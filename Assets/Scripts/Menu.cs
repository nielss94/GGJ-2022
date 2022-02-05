using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            continueButton.Select();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            newGameButton.Select();
        }
    }
}
