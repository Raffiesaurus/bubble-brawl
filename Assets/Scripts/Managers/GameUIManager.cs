using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour {
    public static GameUIManager Instance { get; private set; }

    public BubbleType chosenBubble;

    [SerializeField] private GameUI_HUD inGameUI_HUD;

    [SerializeField] private GameOverPanel gameOverPanel;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static void GameOver(bool playerWon) {
        Time.timeScale = 0;
        Instance.gameOverPanel.gameObject.SetActive(true);
        Instance.gameOverPanel.SetText(playerWon);
    }
}
