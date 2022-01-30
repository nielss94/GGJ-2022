using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public AudioClip clickAudio;
    public void StartNewGame()
    {
        PlayerPrefs.SetInt("DoneTutorial", 0);
        FindObjectOfType<AudioManager>().PlayOneShot(clickAudio, Vector3.zero, 0, .2f);
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
