using UnityEngine;

public class AIController : MonoBehaviour {
    private BubbleBase homeBase;

    private const float RESOURCE_SAVE_THRESHOLD = 10f;
    private const float RESOURCE_BURST_THRESHOLD = 50f;
    private const int UNIT_THRESHOLD = 3;
    private float decisionCooldown = 1f;
    private float cooldownTimer = 0f;

    private void Start() {
        homeBase = GetComponent<BubbleBase>();
        homeBase.isPlayerBase = false;
    }
 
    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= decisionCooldown) {
            AnalyzeAndAct();
            cooldownTimer = 0f;
        }
    }

    private void AnalyzeAndAct() {
        float currentResources = homeBase.bubbleResource.GetCurrentBubbles();

        if (Random.value < 0.5f && currentResources < RESOURCE_SAVE_THRESHOLD) {
            if (Random.value < 0.25f && homeBase.bubbleResource.sunflowerBubbles < 3) {
                homeBase.SpawnBubble(BubbleType.Sunflower, LanePosition.Middle);
            }
            return;
        }

        LanePosition bestLane = FindBestLane();
        int playerUnitsInBestLane = LaneManager.Instance.GetUnitsInLane(bestLane, true);
        int aiUnitsInBestLane = LaneManager.Instance.GetUnitsInLane(bestLane, false);

        if (playerUnitsInBestLane > aiUnitsInBestLane + UNIT_THRESHOLD) {
            Debug.Log("AI reinforcing lane: " + bestLane);
            homeBase.SpawnBubble(BubbleType.Warrior, bestLane);
            return;
        }

        if (currentResources >= RESOURCE_BURST_THRESHOLD) {
            Debug.Log("AI launching burst attack in lane: " + bestLane);
            for (int i = 0; i < 5; i++) {
                BubbleType type = (Random.value < 0.5f) ? BubbleType.Archer : BubbleType.Warrior;
                homeBase.SpawnBubble(type, bestLane);
            }
            decisionCooldown = 2f;
            return;
        }

        Debug.Log("AI spawning fallback unit in lane: " + bestLane);
        BubbleType fallbackType = (Random.value < 0.5f) ? BubbleType.Warrior : BubbleType.Floaty;
        homeBase.SpawnBubble(fallbackType, bestLane);
        decisionCooldown = 1f;
    }

    private LanePosition FindBestLane() {
        LanePosition bestLane = LanePosition.Middle;
        int fewestPlayerUnits = int.MaxValue;

        foreach (LanePosition lane in System.Enum.GetValues(typeof(LanePosition))) {
            int playerUnits = LaneManager.Instance.GetUnitsInLane(lane, true);
            int aiUnits = LaneManager.Instance.GetUnitsInLane(lane, false);

            if (playerUnits < fewestPlayerUnits || aiUnits > playerUnits || Random.value > 0.5f) {
                fewestPlayerUnits = playerUnits;
                bestLane = lane;
            }
        }

        return bestLane;
    }
}
