using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;

            sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    private void Start() {
        Play("Theme");
    }

    public void Play(string sound) {
        Sound sound = Array.Find(sounds, (Predicate<Sound>)(item => item.name == sound));
        if (sound == null) {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        sound.source.volume = sound.volume * (1.0f + UnityEngine.Random.Range(-sound.volumeVariance * 0.5f, sound.volumeVariance * 0.5f));
        sound.source.pitch = sound.pitch * (1.0f + UnityEngine.Random.Range(-sound.pitchVariance * 0.5f, sound.pitchVariance * 0.5f));

        sound.source.Play();
    }

}
