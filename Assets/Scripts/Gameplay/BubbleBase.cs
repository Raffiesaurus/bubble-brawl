using System;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleBase : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayerBase;
    [HideInInspector] public BubbleResourceManager bubbleResource;
    [HideInInspector] public BubbleSpawner bubbleSpawner;
    private int[] bubbleLevels = new int[4];
    private int bubbleLevelMAX = 3;//is actually 4 cus array indexing!!

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
            bubbleSpawner.SpawnBubbleUnit(bubbleType, lane, isPlayerBase , bubbleLevels[(int)bubbleType]);
            if (bubbleType == BubbleType.Sunflower) {
                bubbleResource.sunflowerBubbles++;
            }
        }

    }

    public void setBubbleLevelOnType(BubbleType bubbleType, int level) 
    {
        int pos = (int)bubbleType;

        bubbleLevels[pos] += level;
        
        Debug.Log(bubbleType + "level" + bubbleLevels[pos]);

        if (bubbleLevels[pos] > bubbleLevelMAX) 
        {
            bubbleLevels[pos] = bubbleLevelMAX;
        }


    }
    public int getBubbleLevelOnType(BubbleType type) 
    {
        return bubbleLevels[(int)type];
    }

}
