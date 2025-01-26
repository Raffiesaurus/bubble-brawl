using UnityEngine;

public class LaneManager : MonoBehaviour {
    public static LaneManager Instance { get; private set; }

    private LanePanel lanePanel;
    private Lane[] lanes;
    private Transform selectedObject;

    void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        lanePanel = GetComponent<LanePanel>();
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

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 500.0f)) {

                selectedObject = hit.collider.transform;

                if (selectedObject.parent.TryGetComponent(out Lane lane)) {
                    lanePanel.OnLaneButton(lane.lanePosition);
                }
            }
        }
    }
}
