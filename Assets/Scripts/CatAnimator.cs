using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour {
    [SerializeField] private float stationaryThresholdSpeed = 0.3f;
    
    private Animator animator;
    private Rigidbody rigidbody;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        var velMag = rigidbody.velocity.magnitude;

        if (velMag > stationaryThresholdSpeed) {
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
        
        animator.SetFloat("velocity", rigidbody.velocity.magnitude);
    }
}
