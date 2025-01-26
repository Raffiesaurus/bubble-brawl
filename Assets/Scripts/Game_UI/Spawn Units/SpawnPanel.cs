using System;
using TMPro;
using UnityEngine;

public class SpawnPanel : MonoBehaviour {

    public TMP_Text warriorCost;
    public TMP_Text archerCost;
    public TMP_Text floatyCost;
    public TMP_Text sunflowerCost;

    public void OnUnitSpawnButton(string BubbleTypeString) {
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);

        BubbleType type = (BubbleType)Enum.Parse(typeof(BubbleType), BubbleTypeString);
        if (type == BubbleType.Sunflower) {
            GameManager.Instance.playerBase.SpawnBubble(BubbleType.Sunflower, LanePosition.Middle);
        } else {
            GameUIManager.Instance.chosenBubble = type;
        }
    }

    private void Start() {
        warriorCost.text = GameManager.Instance.GetCurrentUnitCost(BubbleType.Warrior);
        archerCost.text = GameManager.Instance.GetCurrentUnitCost(BubbleType.Archer);
        floatyCost.text = GameManager.Instance.GetCurrentUnitCost(BubbleType.Floaty);
        sunflowerCost.text = GameManager.Instance.GetCurrentUnitCost(BubbleType.Sunflower);
    }

}
