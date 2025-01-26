using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

    public TMP_Text gameOverText;

    void Start() {
        gameObject.SetActive(false);
    }

    public void SetText(bool playerWon) {
        gameOverText.text = playerWon ? "YOU WIN!" : "YOU LOSE!";
    }

    public void OnExitButton() {
        SceneManager.LoadScene(SceneNames.MainMenu.ToString(), LoadSceneMode.Single);
    }

}
