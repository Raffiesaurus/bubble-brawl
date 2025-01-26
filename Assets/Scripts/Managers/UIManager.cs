using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public void Start()
    {
        AudioManager.Instance.ChangeBGMVolume(0.5f);
        AudioManager.Instance.PlayBGM(true);
    }
    public void OnStartGame() {
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);
        SceneManager.LoadScene(SceneNames.Game.ToString());
    }

    public void OnExitGame() {
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);
        Application.Quit();

    }
}
