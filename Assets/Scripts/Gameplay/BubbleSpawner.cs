using System;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour {


    public void SpawnBubbleUnit(BubbleType type, LanePosition lane, bool isPlayer) {
        GameObject unitPrefab = PrefabManager.GetPrefabByType(type);
        BubbleUnit unit = unitPrefab.GetComponent<BubbleUnit>();

        GameObject spawnedUnit = Instantiate(unitPrefab, GetSpawnPosition(lane, isPlayer), Quaternion.identity, gameObject.transform);
        spawnedUnit.GetComponent<BubbleUnit>().Spawn(type, lane);
    }

    public void SpawnSunflowerBubble() {
        // Spawn sunflower bubble to increase resource generation
        Instantiate(PrefabManager.GetPrefabByType(BubbleType.Sunflower), transform.position, Quaternion.identity);
    }

    private Vector3 GetSpawnPosition(LanePosition lane, bool isPlayer) {
        return LaneManager.Instance.GetStartingPoint(lane, isPlayer);
    }
}
