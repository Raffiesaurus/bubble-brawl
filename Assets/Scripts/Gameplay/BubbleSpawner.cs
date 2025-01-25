using System;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour {


    public void SpawnBubbleUnit(BubbleType type, LanePosition lane, bool isPlayer , int level) {
        GameObject unitPrefab = PrefabManager.GetPrefabByType(type);

        GameObject spawnedUnit = Instantiate(unitPrefab, GetSpawnPosition(lane, isPlayer), Quaternion.identity, gameObject.transform);
        spawnedUnit.GetComponent<BubbleUnit>().Spawn(type, lane, level, isPlayer);

        //colour check to make sure levels are different
        switch (level) 
        {
            case 0:
                spawnedUnit.GetComponent<Image>().color = Color.white;
                break;
            case 1:
                spawnedUnit.GetComponent<Image>().color = Color.red;
                break;
            case 2:
                spawnedUnit.GetComponent <Image>().color = Color.green;
                break;
            case 3:
                spawnedUnit.GetComponent<Image>().color = Color.blue;
                break;
        }

    }

    private Vector3 GetSpawnPosition(LanePosition lane, bool isPlayer) {
        return LaneManager.Instance.GetStartingPoint(lane, isPlayer);
    }
}
