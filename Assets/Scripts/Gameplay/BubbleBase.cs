using System.Resources;
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

    private void Start() {
        currentHealth = maxHealth;
        bubbleResource = GetComponent<BubbleResourceManager>();
        bubbleResource.isPlayer = isPlayerBase;
        bubbleSpawner = GetComponent<BubbleSpawner>();

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
            bubbleSpawner.SpawnBubbleUnit(bubbleType, lane, isPlayerBase);
            if (bubbleType == BubbleType.Sunflower) {
                bubbleResource.sunflowerBubbles++;
            }
        }

    }
}
