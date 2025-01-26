using TMPro;
using UnityEngine;

public class TroopCostsUI : MonoBehaviour
{
    public GameObject playerBase;
    private BubbleResourceManager resourceManager;
    public BubbleType type;
    public TMP_Text text;
    public void Start()
    {
        resourceManager = playerBase.GetComponent<BubbleResourceManager>();
    }

    public void Update()
    {
        int cost = resourceManager.getTroopCost(type);
        text.SetText(cost.ToString());
    }

}
