using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CatAnimator : MonoBehaviour {
    [SerializeField] private float stationaryThresholdSpeed = 0.3f;
    [SerializeField] private float jumpscareOffset = 1.5f;
    [SerializeField] private GameObject catPrefab;
    
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

    private void TriggerDeath(GameObject killer) {
        if (killer.name != this.name) return;
        
        GetComponent<CatAggro>().enabled = false;
        GetComponent<CatNavigator>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        
        // Set cat in front of player
        transform.position = player.transform.position + Vector3.left * jumpscareOffset;

        // Set jumpscare enabled
        animator.SetTrigger("jumpscare");
    }

    private void OnDestroy() {
        catAggro.onChaseStarted -= ActivateChase;
        catAggro.onChaseEnded -= DeactivateChase;
        player.onDeath -= TriggerDeath;
    }
}
