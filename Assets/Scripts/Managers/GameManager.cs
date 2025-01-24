using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public Dictionary<string, UnitStats> unitData;

    public BubbleBase playerBase;
    public BubbleBase aiBase;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        LoadUnitData();
    }

    private void LoadUnitData() {
        TextAsset jsonData = Resources.Load<TextAsset>("UnitData");
        if (jsonData != null) {
            UnitData data = JsonUtility.FromJson<UnitData>(jsonData.text);

            // Convert List<UnitStats> to Dictionary<string, UnitStats>
            unitData = new Dictionary<string, UnitStats>();
            foreach (var unit in data.Units) {
                unitData[unit.Name] = unit;
            }
        } else {
            Debug.LogError("UnitData.json not found in Resources folder!");
        }
    }

    public UnitStats GetUnitStats(string unitName) {
        if (unitData.TryGetValue(unitName, out UnitStats stats)) {
            return stats;
        }
        Debug.LogError($"Unit {unitName} not found in UnitData!");
        return null;
    }

    public void EndGame(bool playerWon) {
        GameUIManager.GameOver(playerWon);
    }

    public int GetPlayerBubbles() {
        return playerBase.bubbleResource.GetCurrentBubbles();
    }
}
