using TMPro;
using UnityEngine;

public class GameUI_HUD : MonoBehaviour {

    [SerializeField] private TMP_Text bubbleResourceText;

    private UpgradePanel unitUpgradePanel;

    private SpawnPanel spawnPanel;

    private LanePanel lanePanel;

    void Awake() {
        unitUpgradePanel = GetComponentInChildren<UpgradePanel>();
        spawnPanel = GetComponentInChildren<SpawnPanel>();
        lanePanel = GetComponentInChildren<LanePanel>();
    }

    private void Update() {
        UpdateResourceDisplay();
    }

    public void OnUpgradeMenuButton() {
        //unitUpgradePanel.ToggleActive();
        unitUpgradePanel.gameObject.SetActive(!unitUpgradePanel.gameObject.activeSelf);
    }

    private void UpdateResourceDisplay() {
        bubbleResourceText.text = $"Bubbles: {Mathf.Floor(GameManager.Instance.GetMyBubbleCount())}";
    }
}
