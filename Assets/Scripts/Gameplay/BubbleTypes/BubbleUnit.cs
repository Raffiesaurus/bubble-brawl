using UnityEngine;
using UnityEngine.UI;

public abstract class BubbleUnit : MonoBehaviour {
    [HideInInspector] public BubbleType type;
    [HideInInspector] public LanePosition currentLane;
    [HideInInspector] public int level = 0;

    [HideInInspector] public float maxHealth;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float damage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float cost;
    [HideInInspector] public float upgradeCost;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackSpeed;

    [HideInInspector] public UnitStats baseStats;

    public BubbleState currentState = BubbleState.Moving;
    public bool isPlayerUnit = false;

    public LayerMask enemyLayer;
    public LayerMask baseLayer;

    public Vector3 baseDirection = Vector3.zero;

    private Transform targetEnemy = null;
    private Transform targetBase = null;

    private float attackTimer = 0.0f;

    private Slider hpSlider;

    private void Start() {
        hpSlider = GetComponentInChildren<Slider>();
    }

    public void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
        type = bubbleType;
        currentLane = lane;
        level = unitLevel;
        isPlayerUnit = isPlayer;
        baseStats = GameManager.Instance.GetUnitStats(type.ToString());
        maxHealth = currentHealth = baseStats.Health[level];
        damage = baseStats.Damage[level];
        attackSpeed = baseStats.AttackSpeed[level];
        attackRange = baseStats.AttackRange[level];
        moveSpeed = baseStats.MoveSpeed[level];
        upgradeCost = baseStats.UpgradeCost[level];
        cost = baseStats.Cost;
        gameObject.name = bubbleType.ToString() + " - " + lane.ToString() + " - " + unitLevel.ToString() + " - " + isPlayer.ToString();
    }

    private void Update() {
        switch (currentState) {
            case BubbleState.Moving:
                Move();
                DetectEnemies();
                break;

            case BubbleState.MovingToBase:
                MoveToBase();
                DetectBase();
                break;

            case BubbleState.FightingUnit:
                AttackUnit();
                break;

            case BubbleState.FightingBase:
                AttackBase();
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

    protected virtual void MoveToBase() {
        moveSpeed = baseStats.MoveSpeed[level];
        transform.Translate(baseDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void DetectEnemies() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hits) {
            BubbleUnit enemyUnit = hit.GetComponent<BubbleUnit>();

            if (enemyUnit != null && enemyUnit.isPlayerUnit != isPlayerUnit) {
                targetEnemy = enemyUnit.transform;
                currentState = BubbleState.FightingUnit;

                moveSpeed = 0;
                return;
            }
        }
    }

    private void DetectBase() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, baseLayer);

        foreach (Collider2D hit in hits) {
            BubbleBase enemyBase = hit.GetComponent<BubbleBase>();

            if (enemyBase != null && enemyBase.isPlayerBase != isPlayerUnit) {
                targetBase = enemyBase.transform;
                currentState = BubbleState.FightingBase;

                moveSpeed = 0;
                return;
            }
        }
    }

    public virtual void TakeDamage(float incomingDamage) {
        currentHealth -= incomingDamage;
        hpSlider.value = currentHealth / maxHealth;
        if (currentHealth <= 0) {
            Pop();
        }
    }

    protected virtual void Pop() {
        LaneManager.Instance.CheckToDelete(this);
        Destroy(gameObject);
    }

    public virtual void AttackUnit() {
        if (targetEnemy == null) {
            currentState = BubbleState.Moving;
            moveSpeed = baseStats.MoveSpeed[level];
            return;
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) {
            attackTimer = 1 / attackSpeed;

            BubbleUnit enemyUnit = targetEnemy.GetComponent<BubbleUnit>();
            if (enemyUnit != null) {
                enemyUnit.TakeDamage(damage);

                if (enemyUnit.currentHealth <= 0 || enemyUnit == null) {
                    targetEnemy = null;
                    currentState = BubbleState.Moving;
                    moveSpeed = baseStats.MoveSpeed[level];
                }
            }
        }
    }

    public virtual void AttackBase() {
        if (targetBase == null) {
            return;
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) {
            attackTimer = 1 / attackSpeed;

            BubbleBase enemyBase = targetBase.GetComponent<BubbleBase>();
            if (enemyBase != null) {
                enemyBase.TakeDamage(damage);

                if (enemyBase.currentHealth <= 0 || enemyBase == null) {
                    targetBase = null;
                }

                Pop();
            }
        }
    }

    public void SetBubbleState(BubbleState newState) {
        currentState = newState;
    }
}
