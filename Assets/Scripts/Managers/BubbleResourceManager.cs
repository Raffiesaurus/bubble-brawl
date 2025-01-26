using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BubbleResourceManager : MonoBehaviour {
    private float currentBubbles = 0f;
    private float bubbleGenerationRate = 1f;
    [HideInInspector] public int sunflowerBubbles = 0;
    [HideInInspector] public bool isPlayer = false;

    private void Update() {
        currentBubbles += (bubbleGenerationRate + sunflowerBubbles) * Time.deltaTime;
    }

    public bool CanAffordUnit(BubbleType bubbleType) {
        int cost = GameManager.Instance.GetUnitStats(bubbleType.ToString()).Cost;
        if (currentBubbles >= cost) {
            SpendBubbles(cost);
            return true;
        }
        return false;
    }

    public bool CanAffordUpgrade(BubbleType bubbleType, int currentLevel) {
        UnitStats stats = GameManager.Instance.GetUnitStats(bubbleType.ToString());
        if (currentLevel + 1 < stats.UpgradeCost.Count) {
            int cost = stats.UpgradeCost[currentLevel + 1];
            if (currentBubbles >= cost) {
                SpendBubbles(cost);
                return true;
            }
            return false;
        } else {
            return false;
        }

    }

    public void SpendBubbles(float amount) {
        currentBubbles -= amount;
    }

    public int GetCurrentBubbles() {
        return Mathf.FloorToInt(currentBubbles);
    }

    public void Reset() {
        currentBubbles = 0f;
        bubbleGenerationRate = 1f;
        sunflowerBubbles = 0;
    }
}
