using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private void Start() {
        gameObject.SetActive(false);
    }

    public void OnResumeButton() {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void OnExitButton() {
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }

}
