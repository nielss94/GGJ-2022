using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAnimator : MonoBehaviour {
    [SerializeField] private float stationaryThresholdSpeed = 0.3f;
    
    private Animator animator;
    private NavMeshAgent navAgent;
    private CatAggro catAggro;
    private float velocity;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        catAggro = GetComponent<CatAggro>();
        navAgent = GetComponent<NavMeshAgent>();

        catAggro.onChaseStarted += ActivateChase;
        catAggro.onChaseEnded += DeactivateChase;
    }

    private void Update() {
        velocity = navAgent.velocity.magnitude;

        if (velocity > stationaryThresholdSpeed) {
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
        
        animator.SetFloat("velocity", navAgent.velocity.magnitude);
    }

    private void ActivateChase() {
        animator.SetBool("chasing", true);
    }

    private void DeactivateChase() {
        animator.SetBool("chasing" , false);
    }

    private void OnDestroy() {
        catAggro.onChaseStarted -= ActivateChase;
        catAggro.onChaseEnded -= DeactivateChase;
    }
}
