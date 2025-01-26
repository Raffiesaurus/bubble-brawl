using Unity.VisualScripting;
using UnityEngine;

public class ArcherBubble : BubbleUnit {
    public override void AttackUnit() {
        base.AttackUnit();
        AudioManager.Instance.PlayAudioClip(AudioClips.ArcherHit, 1);
    }

 
    public override void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        base.Spawn(bubbleType, lane, unitLevel, isPlayer);
       
    }
}