using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToQuit : MonoBehaviour
{
	public void OnQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
