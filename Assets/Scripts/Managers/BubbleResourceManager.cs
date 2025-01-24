using Unity.VisualScripting;
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

    public bool CanAffordUpgrade(BubbleType bubbleType, int level) 
    {
        int cost = GameManager.Instance.GetUnitStats(bubbleType.ToString()).UpgradeCost[level];
        if(currentBubbles >= cost) 
        {
            SpendBubbles(cost);
            return true;
        }
        return false;
    }


    public void SpendBubbles(float amount) {
        currentBubbles -= amount;
    }


    public int getUpgradeCost(BubbleType type , int level) 
    {
        return GameManager.Instance.GetUnitStats(type.ToString()).UpgradeCost[level];
    }

    public int GetCurrentBubbles() {
        return Mathf.FloorToInt(currentBubbles);
    }
}
