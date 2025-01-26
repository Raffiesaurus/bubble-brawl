using UnityEngine;

public class WarriorBubble : BubbleUnit {
    public override void AttackUnit() {
        AudioManager.Instance.PlayAudioClip(AudioClips.WarriorHit, 1);

        base.AttackUnit();
    }
}