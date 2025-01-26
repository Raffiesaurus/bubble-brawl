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

    private Animator animator;

    public BubbleState currentState = BubbleState.Moving;
    public bool isPlayerUnit = false;

    public LayerMask enemyLayer;
    public LayerMask baseLayer;

    public Vector3 baseDirection = Vector3.zero;

    private Transform targetEnemy = null;
    private Transform targetBase = null;

    private float attackTimer = 0.0f;

    private Slider hpSlider;
    public GameObject healthBarPrefab;

    public GameObject rotatorJoint;

    private void Start() {

        hpSlider = Instantiate(healthBarPrefab, transform.position, Quaternion.identity).GetComponentInChildren<Slider>();
        hpSlider.gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);

        animator = GetComponentInChildren<Animator>();
        if (!isPlayerUnit) {
            rotatorJoint.transform.eulerAngles = new Vector3(rotatorJoint.transform.eulerAngles.x, rotatorJoint.transform.eulerAngles.y + 180, rotatorJoint.transform.eulerAngles.z);
        }
    }

    public virtual void Spawn(BubbleType bubbleType, LanePosition lane, int unitLevel, bool isPlayer) {
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

        UpdateHealthBarPosition();

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
        Vector3 direction = isPlayerUnit ? Vector3.right : Vector3.left;
        transform.Translate(moveSpeed * Time.deltaTime * direction);
        animator.SetTrigger(AnimatorTriggers.StartMove.ToString());
    }

    protected virtual void MoveToBase() {
        moveSpeed = baseStats.MoveSpeed[level];
        rotatorJoint.transform.eulerAngles = baseDirection.normalized;
        transform.Translate(moveSpeed * 1.5f * Time.deltaTime * baseDirection.normalized);
        animator.SetTrigger(AnimatorTriggers.StartMove.ToString());
    }

    private void DetectEnemies() {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider hit in hits) {
            BubbleUnit enemyUnit = hit.GetComponentInParent<BubbleUnit>();

            if (enemyUnit != null && enemyUnit.isPlayerUnit != isPlayerUnit && currentLane == enemyUnit.currentLane) {
                targetEnemy = enemyUnit.transform;
                currentState = BubbleState.FightingUnit;

                moveSpeed = 0;
                return;
            }
        }
    }

    private void DetectBase() {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, baseLayer);

        foreach (Collider hit in hits) {
            BubbleBase enemyBase = hit.GetComponentInParent<BubbleBase>();

            if (enemyBase != null && enemyBase.isPlayerBase != isPlayerUnit) {
                targetBase = enemyBase.transform;
                currentState = BubbleState.FightingBase;
                AttackBase();
                moveSpeed = 0;
                break;
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

    public virtual void Pop() {
        if (hpSlider != null) {
            Destroy(hpSlider.gameObject);
        }
        LaneManager.Instance.CheckToDelete(this);
        Destroy(gameObject);
    }

    private void UpdateHealthBarPosition() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position + Vector3.up * 15.5f);
        hpSlider.gameObject.transform.position = screenPosition;
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
            animator.SetTrigger(AnimatorTriggers.StartFight.ToString());
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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
