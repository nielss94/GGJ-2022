using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAppearAfterSeconds : MonoBehaviour
{
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public float seconds;
    public bool disappearAfter;
    public float disappearAfterSeconds;

    public bool setText = false;
    IEnumerator Start()
    {
        if (setText)
        {
            var amountOfControllers = Input.GetJoystickNames().Length;
            
            if (amountOfControllers > 0)
            {
                text1.text = "A to interact";
                text2.text = "LT to sprint";
                text3.text = "RT to raise lamp";
            }
            else
            {
                text1.text = "F to interact";
                text2.text = "L SHIFT to sprint";
                text3.text = "Right Mouse to raise lamp";
            }
        }

        yield return new WaitForSeconds(seconds - 1.5f);

        Sequence s = DOTween.Sequence();

        s.OnComplete(() =>
        {
            if (disappearAfter)
            {
                StartCoroutine(Disappear());
            }
        });
        foreach (var text in texts)
        {
            s.Join(text.DOFade(1, 3));
        }
        
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearAfterSeconds);
        foreach (var text in texts)
        {
            text.DOFade(0, 3);
        }
    }
}
