using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    private AudioSource aSource;
    [SerializeField] private bool fadeIn = true;
    [SerializeField] private bool fadeOut = true;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.volume = 0f;
        if (fadeIn) StartCoroutine(Fade(true, aSource, 1f, 0.5f));
        if (fadeOut) StartCoroutine(Fade(false, aSource, 1f, 0f));
    }

    private void Update()
    {
        if (!aSource.isPlaying)
        {
            aSource.Play();
            if (fadeIn) StartCoroutine(Fade(true, aSource, 2f, 0.5f));
            if (fadeOut) StartCoroutine(Fade(false, aSource, 2f, 0f));
        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource aSource, float duration, float targetVol) {
        if (!fadeIn) {
            double lengthOfSource = (double)aSource.clip.samples / aSource.clip.frequency;
            yield return new WaitForSeconds((float) (lengthOfSource - duration));
        }

        float time = 0f;
        float startVol = aSource.volume;
        while (time < duration)
        {
            string fadeSituation = fadeIn? "fadeIn" : "fadeOut";
            Debug.Log(fadeSituation);
            time += Time.deltaTime;
            aSource.volume = Mathf.Lerp(startVol, targetVol, time / duration);
            yield return null;
        }
        yield break;

    }
}
