using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class CatAggro : MonoBehaviour {
    public event Action onChaseStarted = delegate {  };
    public event Action onChaseEnded = delegate {  };
    public event Action onDetection = delegate {  };

    [SerializeField] private float chaseEnergySeconds = 5;
    [SerializeField] private float chaseCooldownSeconds = 5;
    [SerializeField] private float chaseSpeedMultiplier = 2;
    [SerializeField] private float chaseAngularSpeedMultiplier = 3;
    [SerializeField] private float shakeDurationSeconds;
    
    [SerializeField] private bool isChasing = false;
    [SerializeField] private bool isShaking = false;
    [SerializeField] private bool cooldownActive = false;
    [SerializeField] private float currentChaseEnergy;
    private NavMeshAgent navAgent;
    private float navAgentBaseSpeed;
    private float navAgentBaseAngularSpeed;

    private void Awake() {
        if (TryGetComponent(out NavMeshAgent agent)) {
            navAgent = agent;
        }
        // Test

        navAgentBaseSpeed = navAgent.speed;
        navAgentBaseAngularSpeed = navAgent.angularSpeed;
    }

    public IEnumerator StartChasing() {
        if (isChasing && !isShaking) {
            RefreshChaseEnergy();
            yield break;
        }

        if (!isShaking && !isChasing) {
            onDetection.Invoke();
            isShaking = true;
            yield return new WaitForSeconds(shakeDurationSeconds);
            isShaking = false;
            
            isChasing = true;
            
            navAgent.speed = navAgentBaseSpeed * chaseSpeedMultiplier;
            navAgent.angularSpeed = navAgentBaseAngularSpeed * chaseAngularSpeedMultiplier;

            currentChaseEnergy = chaseEnergySeconds;
            onChaseStarted.Invoke();
            StartCoroutine(ChaseEnergyTimer());
            yield return null;
        } 
        
    }

    public bool IsChasing() {
        return isChasing;
    }

    public bool CanChase() {
        if (!isChasing && !cooldownActive) {
            return true;
        } else {
            return false;
        }
    }

    public void StopChasing() {
        isChasing = false;
        onChaseEnded.Invoke();
        
        navAgent.speed = navAgentBaseSpeed;
        navAgent.angularSpeed = navAgentBaseAngularSpeed;
        
        StartCoroutine(ChaseCooldown());
    }

    private void RefreshChaseEnergy() {
        currentChaseEnergy = chaseEnergySeconds;
    }

    private IEnumerator ChaseEnergyTimer() {
        while (currentChaseEnergy > 0) {
            currentChaseEnergy -= Time.deltaTime;
            yield return null;
        }

        StopChasing();
    }

    private IEnumerator ShakeTimer() {
        isShaking = true;
        yield return new WaitForSeconds(shakeDurationSeconds);
        isShaking = false;
    }

    private IEnumerator ChaseCooldown() {
        cooldownActive = true;
        yield return new WaitForSeconds(chaseCooldownSeconds);
        cooldownActive = false;
    }
    
    private void OnTriggerStay(Collider other) {
        if (other.TryGetComponent(out PlayerLampCollider lampCone)) {
            var player = FindObjectOfType<Player>();
            if (player != null) {
                var playerheight = player.GetComponent<Collider>().bounds.size.y;
                var catHeight = GetComponent<Collider>().bounds.size.y;
                var direction = (player.transform.position + ( Vector3.up * playerheight )) - 
                                ( transform.position + ( Vector3.up * catHeight ) );
                if (Physics.Raycast(transform.position + Vector3.up * catHeight, direction, out RaycastHit hit)) {
                    if (hit.transform.CompareTag("Player")) {
                        StartCoroutine(StartChasing());
                    }
                }
            }
        }
    }
}
