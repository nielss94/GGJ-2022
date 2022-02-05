using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    private void OnEnable()
    {
        StartCoroutine(FadeAndLoadSceneCoroutine());
    }

    private IEnumerator FadeAndLoadSceneCoroutine()
    {
        FindObjectOfType<DarthFader>().FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
