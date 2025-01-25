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

    public Vector3 GetStartingPoint(LanePosition lanePosition, BubbleUnit bubbleUnit) {
        foreach (Lane lane in lanes) {
            if (lane.lanePosition == lanePosition) {
                lane.AddBubble(bubbleUnit);
                return lane.GetVectorPoint(bubbleUnit.isPlayerUnit);
            }
        }
        return Vector3.zero;
    }

    public void CheckToDelete(BubbleUnit unit) {
        foreach (Lane lane in lanes) {
            lane.QueueDeletion(unit);
        }
    }
}
