using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NavMethod {
    Stationary,
    Patrol,
    Chase
}

public class CatNavigator : MonoBehaviour {
    [SerializeField] private NavMethod currentNavMethod;
    [SerializeField] private List<NavTarget> patrolTargets;
    [SerializeField] private NavTarget stationaryTarget;
    [SerializeField] private float chaseEnergySeconds;

    private NavMeshAgent navAgent;
    private Queue<NavTarget> patrolTargetQueue;
    private NavMethod initialNavMethod;
    private GameObject playerTarget; // Implement player here
    private GameObject currentTarget;
    private float remainingChaseEnergySec;

    private void Awake() {
        this.navAgent = GetComponent<NavMeshAgent>();
        this.initialNavMethod = currentNavMethod;
        
        patrolTargetQueue = new Queue<NavTarget>();
        foreach (var navTarget in patrolTargets) {
            patrolTargetQueue.Enqueue(navTarget);
        }
        
        StartCoroutine(NavigateNext());
    }

    public IEnumerator NavigateNext(int waitTime = 0) {
        yield return new WaitForSeconds(waitTime);

        if (currentNavMethod == NavMethod.Stationary) {
            currentTarget = stationaryTarget.gameObject;
            navAgent.SetDestination(currentTarget.transform.position);
        } else if (currentNavMethod == NavMethod.Patrol) {
            NavigateNextPatrol();
        } else if (currentNavMethod == NavMethod.Chase) {
            StartCoroutine(ChaseRoutine());
        }
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
            remainingChaseEnergySec -= Time.deltaTime;

            navAgent.SetDestination(playerTarget.transform.position);
            
            if (remainingChaseEnergySec <= 0) {
                currentNavMethod = initialNavMethod;
            }
            
            yield return null;
        }
        
        yield return null;
    }

    public void ActivateChase(GameObject player) { // Change parameter?
        remainingChaseEnergySec = chaseEnergySeconds;
        currentNavMethod = NavMethod.Chase;
        StartCoroutine(NavigateNext());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out NavTarget target)) {
            Debug.Log(target.name.Equals(currentTarget.name));
            if (target.name.Equals(currentTarget.name)) {
                StartCoroutine(NavigateNext(target.WaitTime));
            }
        }
    }
}
