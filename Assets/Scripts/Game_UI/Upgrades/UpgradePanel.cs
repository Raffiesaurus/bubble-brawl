using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour {

    [SerializeField] private TMP_Text warriorUpgradeCostText;
    [SerializeField] private TMP_Text archerUpgradeCostText;
    [SerializeField] private TMP_Text floatyUpgradeCostText;
    [SerializeField] private TMP_Text sunflowerUpgradeCostText;

    private void Start() {
        SetNewCosts();
        Hide();
    }

    public void Show() {
        gameObject.SetActive(false);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ToggleActive() {
        if (gameObject.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }

    public void OnUnitUpgradeButton(string BubbleTypeString) {
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);
        BubbleType type = (BubbleType)Enum.Parse(typeof(BubbleType), BubbleTypeString);
        GameManager.Instance.playerBase.UpgradeBubbleUnit(type);
        SetNewCosts();
    }

    private void SetNewCosts() {
        BubbleBase playerBase = GameManager.Instance.playerBase;

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
