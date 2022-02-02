using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private Sprite _interactControllerSprite;
    [SerializeField] private Sprite _interactKeyboardSprite;
    public Image interactImage;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI imageKeyText;

    private void Start()
    {
        // set interactSprite based on input type
        
        var amountOfControllers = Input.GetJoystickNames().Length;
            
        if (amountOfControllers > 0)
        {
            interactImage.sprite = _interactControllerSprite;
        }
        else
        {
            interactImage.sprite = _interactKeyboardSprite;
        }
        
        Disable();
    }

    public void SetInteractUI(string imageText, string text)
    {
        imageKeyText.text = imageText; 
        interactText.text = text;

        interactImage.gameObject.SetActive(true);
        interactText.gameObject.SetActive(true);
        imageKeyText.gameObject.SetActive(true);
        
    }

    public void Disable()
    {
        interactImage.gameObject.SetActive(false);
        interactText.gameObject.SetActive(false);
        imageKeyText.gameObject.SetActive(false);
    }
}
