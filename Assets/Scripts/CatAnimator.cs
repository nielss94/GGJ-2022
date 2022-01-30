using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CatAnimator : MonoBehaviour {
    [SerializeField] private float stationaryThresholdSpeed = 0.3f;
    [SerializeField] private Vector3 jumpscareOffset;
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
        catAggro.onDetection += StartShake;
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

    private void StartShake() {
        animator.SetTrigger("shake");
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
        transform.position = player.transform.Find("JumpscarePos").position;
        transform.position = new Vector3(transform.position.x, 0.08f, transform.position.z);
        transform.LookAt(player.transform.position);

        // Set jumpscare enabled
        animator.SetTrigger("jumpscare");
    }

    private void OnDestroy() {
        catAggro.onChaseStarted -= ActivateChase;
        catAggro.onChaseEnded -= DeactivateChase;
        player.onDeath -= TriggerDeath;
    }
}
