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

    public void Spawn(BubbleType bubbleType, LanePosition lane, int level) {
        type = bubbleType;
        currentLane = lane;
        baseStats = GameManager.Instance.GetUnitStats(type.ToString());
        maxHealth = health = baseStats.Health[level];
        damage = baseStats.Damage[level];
        attackSpeed = baseStats.AttackSpeed[level];
        attackRange = baseStats.AttackRange[level];
        moveSpeed = baseStats.MoveSpeed[level];
        cost = baseStats.Cost;
        upgradeCost = baseStats.UpgradeCost[level];
    }

    protected virtual void Move() {
    }

    public virtual void TakeDamage(float incomingDamage) {
        health -= incomingDamage;
        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    public abstract void Attack(BubbleUnit targetUnit);

    public virtual void SetLevel(int spawnLevel) {
        level = spawnLevel;
    }

   public virtual float GetUpgradeCost() 
    {
        return upgradeCost;
    }
}
