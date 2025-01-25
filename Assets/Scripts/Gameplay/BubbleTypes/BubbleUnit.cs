using UnityEngine;

public abstract class BubbleUnit : MonoBehaviour {

    [HideInInspector] public BubbleType type;
    [HideInInspector] public LanePosition currentLane;

    [HideInInspector] public int level = 0;

    [HideInInspector] public float maxHealth;
    [HideInInspector] public float health;
    [HideInInspector] public float damage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float cost;
    [HideInInspector] public float upgradeCost;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackSpeed;

    [HideInInspector] public UnitStats baseStats;

    private BubbleState currentState;

    private bool isPlayerUnit = false;

    public void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        type = bubbleType;
        currentLane = lane;
        level = unitLevel;
        baseStats = GameManager.Instance.GetUnitStats(type.ToString());
        maxHealth = health = baseStats.Health[level];
        damage = baseStats.Damage[level];
        attackSpeed = baseStats.AttackSpeed[level];
        attackRange = baseStats.AttackRange[level];
        moveSpeed = baseStats.MoveSpeed[level];
        upgradeCost = baseStats.UpgradeCost[level];
        cost = baseStats.Cost;
    }

    private void Update() {
        switch (currentState) {
            case BubbleState.Moving:
                Move();
                DetectEnemies();
                break;
            case BubbleState.Fighting:
                Attack();
                break;
        }
    }

    protected virtual void Move() {
        if (isPlayerUnit) {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
        } else {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
        }

    }

    private void DetectEnemies() {

    }

    public virtual void TakeDamage(float incomingDamage) {
        health -= incomingDamage;
        if (health <= 0) {
            Pop();
        }
    }

    protected virtual void Pop() {
        Destroy(gameObject);
    }

    public abstract void Attack();

    public virtual float GetUpgradeCost() {
        return upgradeCost;
    }
}
