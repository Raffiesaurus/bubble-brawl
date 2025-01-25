using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

    public TMP_Text gameOverText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameObject.SetActive(false);
    }

    public void SetText(bool playerWon) {
        gameOverText.text = playerWon ? "YOU WIN!" : "YOU LOSE!";
    }

}
