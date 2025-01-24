using System.Resources;
using UnityEngine;

public class BubbleBase : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayerBase;
    [HideInInspector] public BubbleResourceManager bubbleResource;
    [HideInInspector] public BubbleSpawner bubbleSpawner;

    private void Start() {
        currentHealth = maxHealth;
        bubbleResource = GetComponent<BubbleResourceManager>();
        bubbleResource.isPlayer = isPlayerBase;
        bubbleSpawner = GetComponent<BubbleSpawner>();
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            HandleBaseDestroyed();
        }
    }

    private void HandleBaseDestroyed() {
        GameManager.Instance.EndGame(!isPlayerBase);
    }

    public void SpawnBubble(BubbleType bubbleType, LanePosition lane) {

        if (bubbleResource.CanAffordUnit(bubbleType)) {
            bubbleSpawner.SpawnBubbleUnit(bubbleType, lane, isPlayerBase);
            if (bubbleType == BubbleType.Sunflower) {
                bubbleResource.sunflowerBubbles++;
            }
        }

    }
}
