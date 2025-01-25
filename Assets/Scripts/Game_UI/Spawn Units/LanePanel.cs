using UnityEngine;

public class LanePanel : MonoBehaviour {

    public void OnLaneButton(LanePosition lanePosition) {
        switch (lanePosition) {
            case LanePosition.Top:
                OnTopLaneButton();
                break;
            case LanePosition.Middle:
                OnMiddleLaneButton();
                break;
            case LanePosition.Bottom:
                OnBottomLaneButton();
                break;
        }
        GameUIManager.Instance.chosenBubble = BubbleType.NONE;
    }

    private void OnTopLaneButton() {
        BubbleType chosenBubble = GameUIManager.Instance.chosenBubble;
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Top);
        }
    }

    private void OnMiddleLaneButton() {
        BubbleType chosenBubble = GameUIManager.Instance.chosenBubble;
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Middle);
        }
    }

    private void OnBottomLaneButton() {
        BubbleType chosenBubble = GameUIManager.Instance.chosenBubble;
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Bottom);
        }
    }
}
