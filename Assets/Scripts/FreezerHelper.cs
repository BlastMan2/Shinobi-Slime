using UnityEngine;
using System.Collections;

public class FreezeHelper : MonoBehaviour
{
    public AudioSource audioSource;

    public void FreezeAndPlaySound(AudioClip clip, float freezeDuration)
    {
        StartCoroutine(FreezeCoroutine(clip, freezeDuration));
    }

    private IEnumerator FreezeCoroutine(AudioClip clip, float freezeDuration)
    {
        Time.timeScale = 0f;

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }

        yield return new WaitForSecondsRealtime(freezeDuration);
        Time.timeScale = 1f;
    }
}
