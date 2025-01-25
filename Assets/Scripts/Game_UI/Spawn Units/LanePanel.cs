using UnityEngine;

public class LanePanel : MonoBehaviour {

    public void OnLaneButton(int lane) {
        switch (lane) {
            case 0:
                OnTopLaneButton();
                break;
            case 1:
                OnMiddleLaneButton();
                break;
            case 2:
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
