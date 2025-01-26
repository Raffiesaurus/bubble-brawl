using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip[] clips;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(bool loop = true) {
        if (bgmSource != null) {
            bgmSource.clip = clips[(int)AudioClips.BGM];
            bgmSource.loop = loop;
            bgmSource.Play();
        } else {
            Debug.LogWarning("BGM Source is not assigned.");
        }
    }

    public void StopBGM() {
        if (bgmSource != null && bgmSource.isPlaying) {
            bgmSource.Stop();
        }
    }

    public void PlayAudioClip(AudioClips clip) {
        if (sfxSource != null) {
            sfxSource.PlayOneShot(clips[(int)clip]);
        } else {
            Debug.LogWarning("SFX Source is not assigned.");
        }
    }
}