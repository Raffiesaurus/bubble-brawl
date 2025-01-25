using System;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBase : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayerBase;
    [HideInInspector] public BubbleResourceManager bubbleResource;
    [HideInInspector] public BubbleSpawner bubbleSpawner;
    private UnitLevelManager unitLevelManager;

    private Slider hpSlider;
    public GameObject healthBarPrefab;
    private Transform canvasTransform;
    private RectTransform hpSliderRect;

    void Awake() {
        unitLevelManager = GetComponent<UnitLevelManager>();
        bubbleResource = GetComponent<BubbleResourceManager>();
        bubbleSpawner = GetComponent<BubbleSpawner>();
        currentHealth = maxHealth;
        bubbleResource.isPlayer = isPlayerBase;
        hpSlider = GetComponentInChildren<Slider>();
    }

    private void Start() {
        hpSlider = GetComponentInChildren<Slider>();

        canvasTransform = GameUIManager.Instance.hpCanvas.transform;

        if (canvasTransform != null && healthBarPrefab != null) {
            GameObject healthBar = Instantiate(healthBarPrefab, canvasTransform);
            hpSlider = healthBar.GetComponent<Slider>();
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;

            hpSliderRect = healthBar.GetComponent<RectTransform>();
        }

        if (hpSliderRect != null) {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 20.5f);
            hpSliderRect.position = screenPosition;
            //hpSliderRect.
        }

    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        hpSlider.value = currentHealth / maxHealth;
        if (currentHealth <= 0) {
            HandleBaseDestroyed();
        }
    }

    private void HandleBaseDestroyed() {
        GameManager.Instance.EndGame(!isPlayerBase);
        Destroy(gameObject);
    }

    public void SpawnBubble(BubbleType bubbleType, LanePosition lane) {

        if (bubbleResource.CanAffordUnit(bubbleType)) {
            int currentLevel = unitLevelManager.unitLevels[(int)bubbleType];

            bubbleSpawner.SpawnBubbleUnit(bubbleType, lane, isPlayerBase, currentLevel);
            if (bubbleType == BubbleType.Sunflower) {
                bubbleResource.sunflowerBubbles++;
            }
        } else {
            Debug.Log("Cannot afford unit.");
        }

    }

    public void UpgradeBubbleUnit(BubbleType bubbleType) {
        int currentLevel = unitLevelManager.unitLevels[(int)bubbleType];
        if (bubbleResource.CanAffordUpgrade(bubbleType, currentLevel)) {
            unitLevelManager.unitLevels[(int)bubbleType]++;
        } else {
            Debug.Log("Cannot afford the upgrade.");
        }
    }

    private void UpdateHealthBarPosition() {
        
    }

    public int GetUpgradeCost(BubbleType bubbleType) {

        UnitStats stats = GameManager.Instance.GetUnitStats(bubbleType.ToString());
        int currentLevel = unitLevelManager.unitLevels[(int)bubbleType];

        if (currentLevel + 1 < stats.UpgradeCost.Count) {
            return stats.UpgradeCost[currentLevel + 1];
        } else {
            return -1;
        }

    }
}
