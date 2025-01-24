using System;
using UnityEngine;

public class SpawnPanel : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnUnitSpawnButton(string BubbleTypeString) {
        BubbleType type = (BubbleType)Enum.Parse(typeof(BubbleType), BubbleTypeString);
        if (type == BubbleType.Sunflower) {
            GameManager.Instance.playerBase.SpawnBubble(GameUIManager.Instance.chosenBubble, LanePosition.Middle);
        } else {
            GameUIManager.Instance.chosenBubble = type;
        }
    }

}
