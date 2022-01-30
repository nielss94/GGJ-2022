using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public Transform lampPos;
    public PlayerLamp playerLamp;
    public List<AudioClip> death = new List<AudioClip>();
    public bool dead = false;

    public event Action onDeath;

    private DarthFader _darthFader;

    private void Awake()
    {
        _darthFader = FindObjectOfType<DarthFader>();
    }

    private void Start()
    {
        if (_darthFader)
        {
            _darthFader.FadeIn();
        }
        // Instantiate(playerLamp, lampPos.position, lampPos.rotation);
    }

    public void Die()
    {
        dead = true;
        onDeath.Invoke();
        FindObjectOfType<AudioManager>().PlayOneShot(death[Random.Range(0, death.Count)]);
        StartCoroutine(WaitAndReloadScene());
    }

    private IEnumerator WaitAndReloadScene()
    {
        if (_darthFader)
        {
            _darthFader.FadeOut();
        }
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
