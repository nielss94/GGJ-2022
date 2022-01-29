using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    public AudioClip clickAudio;
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().PlayOneShot(clickAudio, Vector3.zero, 0, .2f);
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Niels");
    }
}
