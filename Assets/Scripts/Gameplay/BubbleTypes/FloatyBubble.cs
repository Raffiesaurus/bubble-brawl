using UnityEngine;

public class FloatyBubble : BubbleUnit {

    public override void AttackUnit() {
        AudioManager.Instance.PlayAudioClip(AudioClips.FloatyHit, 1);
        base.AttackUnit();

    }
}
