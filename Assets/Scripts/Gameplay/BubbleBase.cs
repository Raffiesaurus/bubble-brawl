using System;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class BubbleBase : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayerBase;
    [HideInInspector] public BubbleResourceManager bubbleResource;
    [HideInInspector] public BubbleSpawner bubbleSpawner;

    public TMP_Text timeText;
    float accumulatedTime;
    bool baseDestroyed;
    private float surviviedTime;
    private UnitLevelManager unitLevelManager;

    void Awake() {
        unitLevelManager = GetComponent<UnitLevelManager>();
        bubbleResource = GetComponent<BubbleResourceManager>();
        bubbleSpawner = GetComponent<BubbleSpawner>();
        currentHealth = maxHealth;
        bubbleResource.isPlayer = isPlayerBase;

    }

    void Start()
    {
        accumulatedTime = 0f;
        baseDestroyed = false;
    }

    void Update()
    {
        if(!baseDestroyed)
        {
            accumulatedTime+= Time.deltaTime;
            DisplayTime();
        }
        else 
        {
            HandleBaseDestroyed();
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            baseDestroyed = true;
        }
    }

    void DisplayTime()
    {
        

        float minutes = Mathf.FloorToInt(accumulatedTime / 60); 
        float seconds = Mathf.FloorToInt(accumulatedTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void HandleBaseDestroyed() {

        surviviedTime = accumulatedTime;

        for(int i = 0; i < 10; i++)
        {
            if(surviviedTime>PlayerPrefs.GetFloat(Convert.ToString(i)))
            {
                PlayerPrefs.SetFloat(Convert.ToString(i),surviviedTime);
                PlayerPrefs.Save();
            }
        }



        SceneManager.LoadScene(3);
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
