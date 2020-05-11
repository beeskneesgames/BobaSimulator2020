using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance;
    public static AudioManager Instance {
        get {
            return instance;
        }
    }

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;
    private int bobaClipCount;
    private int iceClipCount;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        bobaClipCount = 8;
        iceClipCount = 9;

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

    public void PlayBoba() {
        int clipNumber = UnityEngine.Random.Range(1, bobaClipCount);

        // Choose a random clip
        Play($"Boba{clipNumber.ToString()}");
    }

    public void PlayIce() {
        int clipNumber = UnityEngine.Random.Range(1, iceClipCount);

        // Choose a random clip
        Play($"Ice{clipNumber.ToString()}");
    }

    private void Play(string soundName) {
        Sound sound = Array.Find(sounds, (Predicate<Sound>)(item => item.name == soundName));
        if (sound == null) {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        sound.source.volume = sound.volume * (1.0f + UnityEngine.Random.Range(-sound.volumeVariance * 0.5f, sound.volumeVariance * 0.5f));
        sound.source.pitch = sound.pitch * (1.0f + UnityEngine.Random.Range(-sound.pitchVariance * 0.5f, sound.pitchVariance * 0.5f));

        sound.source.Play();
    }
}
