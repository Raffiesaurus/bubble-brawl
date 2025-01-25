using System;
using UnityEngine;

public class UnitLevelManager : MonoBehaviour {

    [HideInInspector] public int[] unitLevels;

    void Awake() {
        var bubbleTypes = Enum.GetValues(typeof(BubbleType));
        unitLevels = new int[bubbleTypes.Length];

        for (int i = 0; i < bubbleTypes.Length; i++) {
            unitLevels[i] = 0;
        }
    }

}
