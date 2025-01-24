using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    [SerializeField] private GameObject warriorBubblePrefab;
    [SerializeField] private GameObject archerBubblePrefab;
    [SerializeField] private GameObject floatyBubblePrefab;
    [SerializeField] private GameObject sunflowerBubblePrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static GameObject GetPrefabByType(BubbleType type) {
        return type switch {
            BubbleType.Warrior => Instance.warriorBubblePrefab,
            BubbleType.Archer => Instance.archerBubblePrefab,
            BubbleType.Floaty => Instance.floatyBubblePrefab,
            BubbleType.Sunflower => Instance.sunflowerBubblePrefab,
            _ => throw new System.ArgumentException("Invalid bubble type")
        };
    }

}
