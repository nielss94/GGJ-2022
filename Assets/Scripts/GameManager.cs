using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Transform tutorialSpawn;
    public Transform afterTutorialSpawn;

    public Room deleteThis;
    private void Start()
    {
        if (PlayerPrefs.GetInt("DoneTutorial") == 1)
        {
            Destroy(deleteThis.gameObject);
            FindObjectOfType<Player>().transform.position = afterTutorialSpawn.position;
        }
        else
        {
            FindObjectOfType<Player>().transform.position = tutorialSpawn.position;
        }
    }

    public void SetTutorialDone()
    {
        PlayerPrefs.SetInt("DoneTutorial", 1);
    }
}
