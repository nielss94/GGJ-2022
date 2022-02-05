using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // public Button newGameButton;
    // public Button continueButton;
	// public Button quitButton;
	public List<Button> buttons;
	
	private int menuIndex;
	
	private void Start() {
		buttons[0].Select();
	}
    
    private void Update()
    {		
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0) 
		{
			if (menuIndex < buttons.Count) menuIndex++;
			buttons[menuIndex].Select();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
			if (menuIndex > 0) menuIndex--;
			buttons[menuIndex].Select();
        }		
    }
}
