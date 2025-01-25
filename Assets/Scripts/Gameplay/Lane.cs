using System;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {
    public LanePosition lanePosition;

    public List<BubbleUnit> bubbleUnits;

    public int playerBubbleCount = 0;

    public int enemyBubbleCount = 0;

    public GameObject leftPoint;

    public GameObject rightPoint;

    private void Update() {
        CheckIfEndReached();
    }

    public void AddBubble(BubbleUnit bubbleUnit) {
        bubbleUnits.Add(bubbleUnit);
        if (bubbleUnit.isPlayerUnit) {
            playerBubbleCount++;
        } else {
            enemyBubbleCount++;
        }
    }

    public Vector3 GetVectorPoint(bool isPlayer) {
        if (isPlayer) {
            return leftPoint.transform.position;
        } else {
            return rightPoint.transform.position;
        }
    }

    public void QueueDeletion(BubbleUnit unit) {
        if (bubbleUnits.Contains(unit)) {
            bubbleUnits.Remove(unit);
        }
    }

    void CheckIfEndReached() {
        foreach (BubbleUnit bubbleUnit in bubbleUnits) {

            if (bubbleUnit == null) {
                bubbleUnits.Remove(bubbleUnit);
            }

            if (bubbleUnit.currentState == BubbleState.MovingToBase) {
                continue; // Skip units already moving to base
            }

            float distanceToPoint = Mathf.Abs(bubbleUnit.transform.position.x -
                (bubbleUnit.isPlayerUnit ? rightPoint.transform.position.x : leftPoint.transform.position.x));

            if (distanceToPoint <= 2) {
                Vector3 baseTransform = GameManager.Instance.GetEnemyBase(!bubbleUnit.isPlayerUnit)?.transform.position ?? Vector3.zero;

                if (baseTransform != Vector3.zero) {
                    bubbleUnit.baseDirection = baseTransform - bubbleUnit.transform.position;
                    bubbleUnit.SetBubbleState(BubbleState.MovingToBase);
                }
            }
        }
    }

}
