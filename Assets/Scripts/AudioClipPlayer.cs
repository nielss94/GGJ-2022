using System.Collections;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    
    public void PlayOneShot(AudioClip clip)
    {
        StartCoroutine(PlayAndDestroy(clip));
    }

    private IEnumerator PlayAndDestroy(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 1);
        
        Destroy(gameObject);
    }
}