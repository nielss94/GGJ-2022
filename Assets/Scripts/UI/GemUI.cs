using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    public TextMeshProUGUI gemText;
    public int gems = 0;

    public void AddGem()
    {
        gems++;
        SetGemText();
    }

    private void SetGemText()
    {
        gemText.text = $"{gems.ToString()} / 6 gems";
    }
}
