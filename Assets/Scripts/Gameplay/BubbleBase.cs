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

    public Transform[] sunflowerSpots;

    private Slider hpSlider;
    public GameObject healthBarPrefab;


    void Awake() {
    }

    private void Start() {
        unitLevelManager = GetComponent<UnitLevelManager>();
        bubbleResource = GetComponent<BubbleResourceManager>();
        bubbleSpawner = GetComponent<BubbleSpawner>();
        currentHealth = maxHealth;
        bubbleResource.isPlayer = isPlayerBase;
        hpSlider = GetComponentInChildren<Slider>();

        hpSlider = Instantiate(healthBarPrefab, transform.position, Quaternion.identity).GetComponentInChildren<Slider>();
        hpSlider.transform.localScale = new Vector3(4, 1, 1);
        hpSlider.gameObject.transform.SetParent(GameUIManager.Instance.hpCanvas.transform, false);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position + Vector3.up * 65.0f);
        hpSlider.gameObject.transform.position = screenPosition;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        hpSlider.value = currentHealth / maxHealth;
        if (currentHealth <= 0) {
            HandleBaseDestroyed();
        }
    }

    private void HandleBaseDestroyed() {
        BubbleUnit[] bubbleChildren = GetComponentsInChildren<BubbleUnit>();
        foreach (BubbleUnit bubbleChild in bubbleChildren) {
            bubbleChild.Pop();
        }
        Destroy(hpSlider.gameObject);
        GameManager.Instance.EndGame(!isPlayerBase);
        Destroy(gameObject);
    }

    public void SpawnBubble(BubbleType bubbleType, LanePosition lane) {

        if (bubbleResource.CanAffordUnit(bubbleType)) {
            int currentLevel = unitLevelManager.unitLevels[(int)bubbleType];

            if (bubbleType != BubbleType.Sunflower) {
                bubbleSpawner.SpawnBubbleUnit(bubbleType, lane, isPlayerBase, currentLevel);
            } else {
                if (bubbleResource.sunflowerBubbles <= 2) {
                    bubbleSpawner.SpawnSunflower(isPlayerBase, currentLevel, sunflowerSpots[bubbleResource.sunflowerBubbles]);
                    bubbleResource.sunflowerBubbles++;
                } else {
                    Debug.Log("Maximum sunflowers reached");
                }
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

    public int GetUpgradeCost(BubbleType bubbleType) {

        UnitStats stats = GameManager.Instance.GetUnitStats(bubbleType.ToString());
        int currentLevel = unitLevelManager.unitLevels[(int)bubbleType];

        if (currentLevel + 1 < stats.UpgradeCost.Count) {
            return stats.UpgradeCost[currentLevel + 1];
        } else {
            return -1;
        }

    }

    public void CleanupUnits() {
        BubbleUnit[] bubbleChildren = GetComponentsInChildren<BubbleUnit>();
        foreach (BubbleUnit bubbleChild in bubbleChildren) {
            bubbleChild.Pop();
        }
    }

    public void Reset() {
        currentHealth = maxHealth;
        bubbleResource.Reset();
        unitLevelManager.Reset();
        hpSlider.value = currentHealth / maxHealth;
    }
}
