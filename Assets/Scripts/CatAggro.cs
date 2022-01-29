using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAggro : MonoBehaviour {
    public event Action onChaseStarted = delegate {  };
    public event Action onChaseEnded = delegate {  };

    [SerializeField] private float chaseEnergySeconds;
    [SerializeField] private float chaseCooldownSeconds;
    
    [SerializeField] private bool isChasing = false;
    [SerializeField] private bool cooldownActive = false;

    public void StartChasing() {
        if (!CanChase()) return;
        isChasing = true;
        onChaseStarted.Invoke();
        StartCoroutine(ChaseEnergyTimer());
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
        StartCoroutine(ChaseCooldown());
    }

    private IEnumerator ChaseEnergyTimer() {
        yield return new WaitForSeconds(chaseEnergySeconds);
        StopChasing();
    }

    private IEnumerator ChaseCooldown() {
        cooldownActive = true;
        yield return new WaitForSeconds(chaseCooldownSeconds);
        cooldownActive = false;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerLampCollider lampCone)) {
            StartChasing();
        }
    }
}
