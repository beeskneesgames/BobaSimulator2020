using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume = 0.75f;
    [Range(0.0f, 1.0f)]
    public float volumeVariance = 0.1f;

    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;
    [Range(0.0f, 1.0f)]
    public float pitchVariance = 0.1f;

    public bool loop = false;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;

}
