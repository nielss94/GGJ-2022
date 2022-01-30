using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DarthFader : MonoBehaviour
{
    public Image image;

    public void FadeIn()
    {
        image.DOFade(0, 2f);
    }
    
    public void FadeOut()
    {
        image.DOFade(1, 2f);
    }
    public void FadeOutAndLoadCutScene()
    {
        image.DOFade(1, 2f).OnComplete(() =>
        {
            SceneManager.LoadScene("EndingScene");
        });
    }
}
