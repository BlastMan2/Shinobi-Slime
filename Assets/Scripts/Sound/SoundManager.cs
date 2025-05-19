using UnityEngine;
using System;

public enum SoundType
{ 
    JUMP,
    HURT,
    FOOTSTEP,
    SLASH,
    KUNAI,
    COLLECTIBLE,
    BRIDGE_PLACE,
    SHINOBI_SENSE
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList; // Changed type to SoundList[]
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds; // Accessing Sounds from SoundList
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume); // Fixed PlayOneShot call
    }

    public static void PlaySoundSpecificIndex(SoundType sound, float volume = 1, int specificClipIndex = 0)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds; // Accessing Sounds from SoundList
        if (specificClipIndex >= 0 && specificClipIndex < clips.Length)
        {
            AudioClip specificClip = clips[specificClipIndex];
            instance.audioSource.PlayOneShot(specificClip, volume); // Fixed PlayOneShot call
        }
        else
        {
            Debug.LogWarning($"SoundManager: Invalid clip index {specificClipIndex} for sound type {sound}.");
        }
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; } // Correctly exposing the sounds array
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
