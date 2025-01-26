using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradePanel: MonoBehaviour
{
    public GameObject playerBase;
    private BubbleResourceManager resourceManager;
 

    public void Start()
    {
        resourceManager = playerBase.GetComponent<BubbleResourceManager>();
        
    }
    public void OnUnitUpgrade(ButtonType type) 
   {
        BubbleType unitType = type.type;
        if (resourceManager.CanAffordUpgrade(unitType, playerBase.GetComponent<BubbleBase>().getBubbleLevelOnType(unitType))) 
        {
            playerBase.GetComponent<BubbleBase>().setBubbleLevelOnType(unitType, 1);
        }
        else 
        {
            Debug.Log("Cant afford to upgrade unit: " + unitType);
        }




    }

}
