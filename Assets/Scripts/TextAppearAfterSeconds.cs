using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAppearAfterSeconds : MonoBehaviour
{
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    public float seconds;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(seconds - 1.5f);

        foreach (var text in texts)
        {
            text.DOFade(1, 3);
        }
    }

}
