using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text bubbleResourceText;

    private BubbleType chosenBubble = BubbleType.NONE;

    private void Update() {
        UpdateResourceDisplay();
    }

    public void OnWarriorSpawnButton() {
        chosenBubble = BubbleType.Warrior;
    }

    public void OnArcherSpawnButton() {
        chosenBubble = BubbleType.Archer;
    }

    public void OnFloatySpawnButton() {
        chosenBubble = BubbleType.Floaty;
    }

    public void OnSunflowerSpawnButton() {
        chosenBubble = BubbleType.Sunflower;
        GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Middle);
        chosenBubble = BubbleType.NONE;
    }

    public void OnTopLaneButton() {
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Top);
        }
        chosenBubble = BubbleType.NONE;
    }

    public void OnMiddleLaneButton() {
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Middle);
        }
        chosenBubble = BubbleType.NONE;
    }

    public void OnBottomLaneButton() {
        if (chosenBubble != BubbleType.NONE) {
            GameManager.Instance.playerBase.SpawnBubble(chosenBubble, LanePosition.Bottom);
        }
        chosenBubble = BubbleType.NONE;
    }

    private void UpdateResourceDisplay() {
        bubbleResourceText.text = $"Bubbles: {Mathf.Floor(GameManager.Instance.GetPlayerBubbles())}";
    }
}
