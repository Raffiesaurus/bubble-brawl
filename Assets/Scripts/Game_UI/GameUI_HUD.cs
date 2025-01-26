using TMPro;
using UnityEngine;

public class GameUI_HUD : MonoBehaviour {

    [SerializeField] private TMP_Text bubbleResourceText;

    private PausePanel pausePanel;

    private UpgradePanel unitUpgradePanel;

    private SpawnPanel spawnPanel;

    private LanePanel lanePanel;

    void Awake() {
        unitUpgradePanel = GetComponentInChildren<UpgradePanel>();
        spawnPanel = GetComponentInChildren<SpawnPanel>();
        lanePanel = GetComponentInChildren<LanePanel>();
        pausePanel = GetComponentInChildren<PausePanel>();
    }

    public void OnPauseButton() {
        Time.timeScale = 0.0f;
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);
        pausePanel.gameObject.SetActive(true);
    }

    private void Update() {
        UpdateResourceDisplay();
    }

    public void OnUpgradeMenuButton() {
        //unitUpgradePanel.ToggleActive();

        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);
        unitUpgradePanel.gameObject.SetActive(!unitUpgradePanel.gameObject.activeSelf);
    }

    private void UpdateResourceDisplay() {
        bubbleResourceText.text = $"{Mathf.Floor(GameManager.Instance.GetMyBubbleCount())}";
    }
}
