using UnityEngine;

public class AIController : MonoBehaviour {
    private BubbleBase homeBase;

    private float aiDecisionTimer = 0f;
    private const float DECISION_INTERVAL = 5f;

    private void Start() {
        homeBase = GetComponent<BubbleBase>();
        homeBase.isPlayerBase = false;
    }

    private void Update() {
        aiDecisionTimer += Time.deltaTime;
        if (aiDecisionTimer >= DECISION_INTERVAL) {
            MakeAIDecision();
            aiDecisionTimer = 0f;
        }
    }

    private void MakeAIDecision() {
        // He a lil stupid

        BubbleType randomType = (BubbleType)Random.Range(0, 3);
        LanePosition randomLane = (LanePosition)Random.Range(0, 3);

        Debug.Log("I am a genius. I summon " + randomType + " in " + randomLane);

        homeBase.SpawnBubble(randomType, randomLane);

        if (Random.value < 0.2f) {
            homeBase.SpawnBubble(BubbleType.Sunflower, LanePosition.Middle);
        }
    }
}