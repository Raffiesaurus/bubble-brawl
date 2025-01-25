using System;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour {


    public void SpawnBubbleUnit(BubbleType type, LanePosition lane, bool isPlayer, int level) {
        GameObject unitPrefab = PrefabManager.GetPrefabByType(type);

        GameObject spawnedUnit = Instantiate(unitPrefab);
        BubbleUnit spawnedUnitScript = spawnedUnit.GetComponent<BubbleUnit>();
        spawnedUnitScript.Spawn(type, lane, level, isPlayer);
        spawnedUnit.transform.rotation = Quaternion.identity;
        Vector3 spawnPos = GetSpawnPosition(lane, spawnedUnitScript);
        spawnPos.y += 3;
        spawnedUnit.transform.position = spawnPos;
        spawnedUnit.GetComponentInChildren<SphereCollider>().gameObject.transform.localScale = Vector3.one * ((level + 1) * 3.0f);
        spawnedUnit.transform.SetParent(gameObject.transform, true);
        //colour check to make sure levels are different
        /*switch (level) 
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
        }*/

    }

    private Vector3 GetSpawnPosition(LanePosition lane, BubbleUnit bubbleUnit) {
        return LaneManager.Instance.GetStartingPoint(lane, bubbleUnit);
    }
}
