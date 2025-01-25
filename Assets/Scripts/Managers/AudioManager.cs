using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    //public AudioClip[] 

    void Start() {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayBGM() {

    }

    public void StopBGM() {

    }

    public void PlayAudioClip(AudioClip clip) {

    }
}
