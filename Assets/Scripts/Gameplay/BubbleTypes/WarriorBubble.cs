using UnityEngine;

public class WarriorBubble : BubbleUnit {

    public override void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        base.Spawn(bubbleType, lane, unitLevel, isPlayer);
        attackClip = AudioClips.WarriorHit;
    }

    public override void AttackUnit() {
        base.AttackUnit();
    }
}