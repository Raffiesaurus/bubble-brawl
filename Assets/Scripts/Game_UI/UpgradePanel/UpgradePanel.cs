using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradePanel : MonoBehaviour {

    [SerializeField] private TMP_Text warriorUpgradeCostText;
    [SerializeField] private TMP_Text archerUpgradeCostText;
    [SerializeField] private TMP_Text floatyUpgradeCostText;
    [SerializeField] private TMP_Text sunflowerUpgradeCostText;

    public GameObject panel; 

    private void Start() {
        SetNewCosts();
        Hide();
    }

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }

    public void ToggleActive() {
        if (panel.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }

    public void OnUnitUpgradeButton(string BubbleTypeString) {
        BubbleType type = (BubbleType)Enum.Parse(typeof(BubbleType), BubbleTypeString);
        GameManager.Instance.playerBase.UpgradeBubbleUnit(type);
        SetNewCosts();
    }

    private void SetNewCosts() {
        BubbleBase playerBase = GameManager.Instance.playerBase;

        // Map each BubbleType to its corresponding UI Text component
        var upgradeCosts = new Dictionary<BubbleType, TMP_Text>() {
        { BubbleType.Warrior, warriorUpgradeCostText },
        { BubbleType.Archer, archerUpgradeCostText },
        { BubbleType.Floaty, floatyUpgradeCostText },
        { BubbleType.Sunflower, sunflowerUpgradeCostText }
    };

        foreach (var pair in upgradeCosts) {
            int cost = playerBase.GetUpgradeCost(pair.Key);
            pair.Value.text = cost > 0 ? cost.ToString() : "MAX";
        }
    }

}
