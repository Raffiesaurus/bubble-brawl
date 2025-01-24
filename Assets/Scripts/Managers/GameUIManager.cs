using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    public GameUI inGameUI;

    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static void GameOver(bool playerWon) {
        Time.timeScale = 0;
        Instance.gameOverPanel.SetActive(true);
        Instance.gameOverText.text = playerWon ? "Player Wins!" : "AI Wins!";
    }
}
