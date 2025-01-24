using UnityEngine;

public class LaneManager : MonoBehaviour {
    public static LaneManager Instance { get; private set; }

    private Lane[] lanes;

    void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        lanes = GetComponentsInChildren<Lane>();
    }

    public Vector3 GetStartingPoint(LanePosition lanePosition, bool isPlayer) {
        foreach (Lane lane in lanes) {
            if (lane.lanePosition == lanePosition) {
                if (isPlayer) {
                    lane.playerBubbleCount++;
                    return lane.leftPoint.transform.position;
                } else {
                    lane.enemyBubbleCount++;
                    return lane.rightPoint.transform.position;
                }
            }
        }
        return Vector3.zero;
    }
}
