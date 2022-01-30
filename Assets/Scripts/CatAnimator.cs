using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CatAnimator : MonoBehaviour {
    [SerializeField] private float stationaryThresholdSpeed = 0.3f;
    [SerializeField] private float jumpscareOffset = 1.5f;
    
    private Animator animator;
    private NavMeshAgent navAgent;
    private CatAggro catAggro;
    private Player player;
    private float velocity;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        catAggro = GetComponent<CatAggro>();
        navAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();

        catAggro.onChaseStarted += ActivateChase;
        catAggro.onChaseEnded += DeactivateChase;
        player.onDeath += TriggerDeath;
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

    private void TriggerDeath() {
        catAggro.enabled = false;
        GetComponent<CatNavigator>().enabled = false;
        navAgent.enabled = false;
        
        // Set cat in front of player
        transform.position = player.transform.position + Vector3.back * jumpscareOffset;

        // Set jumpscare enabled
        animator.SetTrigger("jumpscare");
    }

    private void OnDestroy() {
        catAggro.onChaseStarted -= ActivateChase;
        catAggro.onChaseEnded -= DeactivateChase;
        player.onDeath -= TriggerDeath;
    }
}
