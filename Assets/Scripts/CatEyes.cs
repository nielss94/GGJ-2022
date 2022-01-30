using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CatAggro))]
public class CatEyes : MonoBehaviour {
    [SerializeField] private Renderer catEyesModel;
    [SerializeField] private Material passiveMaterial;
    [SerializeField] private Material activeMaterial;
    
    private CatAggro aggro;

    private void Awake() {
        this.aggro = GetComponent<CatAggro>();

        aggro.onChaseStarted += ActivateEyes;
        aggro.onChaseEnded += DeactivateEyes;
    }

    private void ActivateEyes() {
        // StartCoroutine(ActivationRoutine());
        catEyesModel.material = activeMaterial;
    }

    private void DeactivateEyes() {
        catEyesModel.material = passiveMaterial;
    }

    // private IEnumerator ActivationRoutine() {
    //     catEyesModel.material = activeMaterial;
    //
    //     // TODO: Flash the eyes on activation
    //     Sequence eyeSequence = DOTween.Sequence();
    //     eyeSequence.Append(DOTween.To(() => catEyesModel.material))
    //
    // }

    private void OnDestroy() {
        aggro.onChaseStarted -= ActivateEyes;
        aggro.onChaseEnded -= DeactivateEyes;
    }
}
