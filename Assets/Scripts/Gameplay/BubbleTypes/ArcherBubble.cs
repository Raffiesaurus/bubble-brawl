using Unity.VisualScripting;
using UnityEngine;

public class ArcherBubble : BubbleUnit {

    public override void AttackUnit() {
        base.AttackUnit();
    }


    public override void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        base.Spawn(bubbleType, lane, unitLevel, isPlayer);
        attackClip = AudioClips.ArcherHit;
    }
}