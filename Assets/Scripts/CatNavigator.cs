using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NavMethod {
    Stationary,
    Patrol,
    Chase,
    Disabled
}

public class CatNavigator : MonoBehaviour {
    [SerializeField] private NavMethod currentNavMethod;
    [SerializeField] private List<NavTarget> patrolTargets;
    [SerializeField] private NavTarget stationaryTarget;

    private NavMeshAgent navAgent;
    private Queue<NavTarget> patrolTargetQueue;
    private NavMethod initialNavMethod;
    private Player playerTarget;
    [SerializeField] private GameObject currentTarget;
    private CatAggro catAggro;
    private Coroutine navigateRoutine;

    private void Awake() {
        this.navAgent = GetComponent<NavMeshAgent>();
        this.initialNavMethod = currentNavMethod;
        
        patrolTargetQueue = new Queue<NavTarget>();
        foreach (var navTarget in patrolTargets) {
            patrolTargetQueue.Enqueue(navTarget);
        }

        playerTarget = FindObjectOfType<Player>();

        if (TryGetComponent(out CatAggro aggro)) {
            this.catAggro = aggro;
            catAggro.onChaseStarted += ActivateChase;
            catAggro.onChaseEnded += EndChase;
            catAggro.onDetection += DisableNavigation;
        }
        
        NavigateNext();
    }

    public void NavigateNext(int waitTime = 0) {
        if (currentNavMethod == NavMethod.Stationary) {
            currentTarget = stationaryTarget.gameObject;
            navAgent.SetDestination(currentTarget.transform.position);
        } else if (currentNavMethod == NavMethod.Patrol) {
            NavigateNextPatrol();
        } else if (currentNavMethod == NavMethod.Chase) {
            navigateRoutine = StartCoroutine(ChaseRoutine());
        }
    }

    public IEnumerator NavigateNextDelay(int waitTime) {
        yield return new WaitForSeconds(waitTime);
        NavigateNext();
    }

    public void NavigateNextPatrol() {
        if (patrolTargetQueue.Count <= 0) {
            foreach (var navTarget in patrolTargets) {
                patrolTargetQueue.Enqueue(navTarget);
            }
        }
        
        currentTarget = patrolTargetQueue.Dequeue().gameObject;
        navAgent.SetDestination(currentTarget.transform.position);
    }

    public IEnumerator ChaseRoutine() {
        while (currentNavMethod == NavMethod.Chase) {
            navAgent.SetDestination(playerTarget.transform.position);
            
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return null;
    }

    public void ActivateChase() {
        if (currentNavMethod == NavMethod.Chase) return;
        currentNavMethod = NavMethod.Chase;
        navAgent.isStopped = false;
        NavigateNext();
    }

    public void DisableNavigation() {
        currentNavMethod = NavMethod.Disabled;
        navAgent.isStopped = true;
    }

    private void EndChase() {
        currentNavMethod = initialNavMethod;
        NavigateNext();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out NavTarget target)) {
            if (target.name.Equals(currentTarget.name)) {
                StartCoroutine(NavigateNextDelay(target.WaitTime));
            }
        }
    }

    private void OnDisable() {
        catAggro.onChaseStarted -= ActivateChase;
        catAggro.onChaseEnded -= EndChase;
        if (navigateRoutine != null) StopCoroutine(navigateRoutine);
    }
}
