using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy
{
    private const float DEFAULT_ANIMATION_SPEED = 1f;
    private const float DAMAGE_ANIMATION_SPEED = 0.7f;
    [SerializeField] private int enemyLevel, enemyDamage, playerLevel;
    [SerializeField] private float lookRadius, enemySpeed, enemyPrize;
    [SerializeField] private static Transform enemyTarget = GameObject.FindWithTag("Player").transform; // Always the player
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private EnemyAttackWeapon enemyAttackWeapon; // Set the cylinder transform here attached to the skeleton sword prop
    private EnemyHealthSystemController skeletonHealthSystemController;
    private LevelSystemController levelSystemController;
    private Animator enemyAnimator;

    /// <summary>
    /// Constructs the skeleton and sets it level
    /// </summary>
    /// <param name="level">the skeleton level to set</param
    public Skeleton(int level)
    {
        setEnemyLevel(level);
    }

    // Start is called before the first frame update and initializes the skeleton
    void Start()
    {
        playerLevel = getPlayerLevel();
        enemyLevel = Mathf.Max(1, playerLevel - 1);
        setEnemyLevel(enemyLevel); // Set Level
        setHealthSystemController(playerLevel);
        meshAgent = this.GetComponent<NavMeshAgent>();
        enemyAnimator = this.GetComponent<Animator>();
        enemyAttackWeapon = this.GetComponentInChildren<EnemyAttackWeapon>();
        setLookRadius(20f);
        setEnemyPrize(10 * getEnemyLevel());
        setEnemyDamage(10 * getEnemyLevel());
        setLevelSystemController();
    }

    /// <summary>
    /// Invoke death upon enemy
    /// </summary>
    public override void enemyDie()
    {
        enemyAnimator.SetBool(Enemy.isIdle, false);
        enemyAnimator.SetBool(Enemy.isAttacking, false);
        enemyAnimator.SetBool(Enemy.isWalking, false);
        enemyAnimator.SetBool(Enemy.isReceivingDamage, false);
        enemyAnimator.SetBool(Enemy.isDying, true);
        levelSystemController.addExperience(this.getEnemyPrize());
        meshAgent.isStopped = true; // Stop moving
        // Note that Destroy(gameObject); is implemented on the Animator after the death animation is finished
    }

    /// <summary>
    /// sets the player level system controller so the skeleton could interact with it
    /// </summary>
    private void setLevelSystemController()
    {
        levelSystemController = GameObject.FindWithTag("Player").GetComponent<LevelSystemController>();
    }

    /// <summary>
    /// Sets the enemy health system controller of the skeleton according the the player level
    /// With stronger player, we want stronger enemies.
    /// </summary>
    /// <param name="playerLevel">the player level</param>
    private void setHealthSystemController(int playerLevel)
    {
        skeletonHealthSystemController = new EnemyHealthSystemController();
    }

    /// <summary>
    /// Chases the player
    /// </summary>
    void Update()
    {
        if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReceivingDamage"))
            chasePlayer();
    }

    /// <summary>
    /// If the player is close enough to the enemy then the enemy chases the player 
    /// until he reaches a certain point. Then starts attacking the player
    /// This includes animations for the Skeleton
    /// </summary>
    private void chasePlayer()
    {
        float distanceFromPlayer = Vector3.Distance(enemyTarget.position, transform.position);
        if ((distanceFromPlayer < getLookRadius() || !meshAgent.velocity.Equals(Vector3.zero))
            && !enemyAnimator.GetBool(isDying)
            && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReceivingDamage"))
        {
            if ((distanceFromPlayer > meshAgent.stoppingDistance))
            {
                // Walk
                enemyAnimator.SetBool(Enemy.isIdle, false);
                enemyAnimator.SetBool(Enemy.isReceivingDamage, false);
                enemyAnimator.SetBool(Enemy.isAttacking, false);
                enemyAnimator.SetBool(Enemy.isWalking, true);
                meshAgent.SetDestination(enemyTarget.position);
            }
            else
            {
                // Stop
                meshAgent.velocity = Vector3.zero;
                FaceTarget();
                Attack();
            }
        }
        else
        {
            // Idle
            enemyAnimator.SetBool(Enemy.isWalking, false);
            enemyAnimator.SetBool(Enemy.isAttacking, false);
            enemyAnimator.SetBool(Enemy.isIdle, true);
        }
    }
    private void Attack()
    {
        // Attack
        enemyAnimator.SetBool(Enemy.isAttacking, true);
        enemyAnimator.SetBool(Enemy.isWalking, false);
        enemyAnimator.SetBool(Enemy.isIdle, false);
        enemyAnimator.SetBool(Enemy.isReceivingDamage, false);
    }

    /// <summary>
    /// Set the animator to receive damage
    /// </summary>
    public void playAnimationReceiveDamage()
    {
        if (!enemyAnimator.GetBool(isDying) && (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReceivingDamage")))
        {
            enemyAnimator.speed = DAMAGE_ANIMATION_SPEED;
            enemyAnimator.Play("ReceivingDamage");
            enemyAnimator.speed = DEFAULT_ANIMATION_SPEED;
            enemyAnimator.SetBool(Enemy.isIdle, false);
            enemyAnimator.SetBool(Enemy.isWalking, false);
            enemyAnimator.SetBool(Enemy.isAttacking, false);
        }
    }

    /// <summary>
    /// Faces the player
    /// </summary>
    public void FaceTarget()
    {
        Vector3 lookPos = enemyTarget.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    /// <returns>the enemy level</returns>
    public override int getEnemyLevel()
    {
        return enemyLevel;
    }

    /// <summary>
    /// Sets the enemy level
    /// </summary>
    /// <param name="level">level to set</param>
    public override void setEnemyLevel(int level)
    {
        enemyLevel = level;
    }

    /// <returns>the enemy damage</returns>
    public override int getEnemyDamage()
    {
        return enemyDamage;
    }

    /// <summary>
    /// Sets the enemy damage
    /// </summary>
    /// <param name="damage">damage to set</param>
    public override void setEnemyDamage(int damage)
    {
        enemyDamage = damage;
    }

    /// <returns>the enemy speed</returns>
    public override float getEnemySpeed()
    {
        return enemySpeed;
    }

    /// <summary>
    /// Sets the enemy speed
    /// </summary>
    /// <param name="speed">enemy speed to set</param>
    public override void setEnemySpeed(float speed)
    {
        enemySpeed = speed;
    }

    /// <returns>the enemy experience prize</returns>
    public override float getEnemyPrize()
    {
        return enemyPrize;
    }

    /// <summary>
    /// Sets the enemy expereince prize
    /// </summary>
    /// <param name="expPrize">experience prize for the player</param>
    public override void setEnemyPrize(float expPrize)
    {
        enemyPrize = expPrize;
    }

    /// <summary>
    /// Within this radius the player is detectable to the enemy
    /// </summary>
    /// <returns>the look radius of the enemy</returns>
    public override float getLookRadius()
    {
        return lookRadius;
    }

    /// <summary>
    /// Sets the player detection radius for the enemy
    /// </summary>
    /// <param name="radius">the radius to set</param>
    public override void setLookRadius(float radius)
    {
        lookRadius = radius;
    }
}
