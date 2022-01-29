using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
                        StartChasing();
                    }
                }
            }
        }
    }
}
