using TMPro;
using UnityEngine;

public class UpgradeCosts: MonoBehaviour
{

    public TMP_Text text;
    public GameObject bubbleBase;
    public BubbleType type;
    private BubbleResourceManager resourceManager;
    public void Start()
    {
        resourceManager = bubbleBase.GetComponent<BubbleResourceManager>();

    }

    public void Update()
    {
        float cost = resourceManager.getUpgradeCost(type);
        text.SetText(cost.ToString());
    }





}
