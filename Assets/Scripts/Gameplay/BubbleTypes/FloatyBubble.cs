using UnityEngine;

public class FloatyBubble : BubbleUnit {

    public override void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        base.Spawn(bubbleType, lane, unitLevel, isPlayer);
        attackClip = AudioClips.FloatyHit;
    }

    public override void AttackUnit() {
        base.AttackUnit();
    }
}
