using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform lampPos;
    public PlayerLamp playerLamp;

    public bool dead = false;
    private void Start()
    {
        Instantiate(playerLamp, lampPos.position, lampPos.rotation);
    }

    public void Die()
    {
        dead = true;
        StartCoroutine(WaitAndReloadScene());
    }

    private IEnumerator WaitAndReloadScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
