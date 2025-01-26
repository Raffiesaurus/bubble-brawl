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
        //spawnedUnit.GetComponentInChildren<SphereCollider>().gameObject.transform.localScale = Vector3.one * ((level + 1) * 3.0f);
        spawnedUnit.transform.SetParent(gameObject.transform, true);
    }

    public void SpawnSunflower(bool isPlayer, int level, Transform spawnPot) {
        GameObject unitPrefab = PrefabManager.GetPrefabByType(BubbleType.Sunflower);

        GameObject spawnedUnit = Instantiate(unitPrefab);
        BubbleUnit spawnedUnitScript = spawnedUnit.GetComponent<BubbleUnit>();

        spawnedUnit.transform.rotation = Quaternion.identity;
        Vector3 spawnPos = spawnPot.position;
        spawnedUnit.transform.position = spawnPos;
        spawnedUnit.transform.SetParent(gameObject.transform, true);
    }

    private Vector3 GetSpawnPosition(LanePosition lane, BubbleUnit bubbleUnit) {
        return LaneManager.Instance.GetStartingPoint(lane, bubbleUnit);
    }
}
