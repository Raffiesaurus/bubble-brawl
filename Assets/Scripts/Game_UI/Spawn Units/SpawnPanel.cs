using System;
using UnityEngine;

public class SpawnPanel : MonoBehaviour {

    public void OnUnitSpawnButton(string BubbleTypeString) {
        AudioManager.Instance.PlayAudioClip(AudioClips.ButtonClicked, 1);

        BubbleType type = (BubbleType)Enum.Parse(typeof(BubbleType), BubbleTypeString);
        if (type == BubbleType.Sunflower) {
            GameManager.Instance.playerBase.SpawnBubble(BubbleType.Sunflower, LanePosition.Middle);
        } else {
            GameUIManager.Instance.chosenBubble = type;
        }
    }

}
