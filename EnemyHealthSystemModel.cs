using UnityEngine;

public class EnemyHealthSystemModel : MonoBehaviour
{
    [SerializeField] private MalbersAnimations.Stats enemyMStats;
    private const int STARTLEVEL = 0;
    private int[] healthByLevel = { 20, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200 };
    private int enemyLevel;
    [SerializeField] private float currentHealth;
    private bool isAlive = true;

    /// <summary>
    /// Start is called before the first frame update
    /// Initializes the enemy health system model, along with Malbers Stats
    /// </summary>
    private void Start()
    {
        enemyMStats = gameObject.GetComponent<MalbersAnimations.Stats>();
        enemyLevel = Mathf.Max(STARTLEVEL, Enemy.getPlayerLevel() - 1);
        currentHealth = healthByLevel[enemyLevel];
        enemyMStats.Stat_Pin(1); // Set the health on the list
        enemyMStats.Stat_Pin_SetMaxValue(currentHealth);
        enemyMStats.Stat_Pin_ModifyValue(currentHealth);
        Debug.Log("Skeleton enemyLevel: " + enemyLevel + " currentHealth: " + currentHealth);
    }

    /// <summary>
    /// Updates the current enemy health 
    /// </summary>
    public void enemyUpdateCurrentHealth()
    {
        currentHealth = enemyMStats.Stat_Get(1).value;
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Enemy thisEnemy = transform.GetComponent<Enemy>();
            thisEnemy.enemyDie();
        }
    }

    /// <summary>
    /// Subtracts health and updates the view
    /// </summary>
    /// <param name="healthAmountToSub">The amount of health to subtract</param>
    /// <param name="healthSystemView">The health system view to update</param>
    public void subtractHealth(float healthAmountToSub, EnemyHealthSystemView healthSystemView)
    {
        currentHealth -= healthAmountToSub;
        healthSystemView.refreshView();
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Enemy thisEnemy = transform.GetComponent<Enemy>();
            thisEnemy.enemyDie();
        }
    }


    /// <returns>the normalized enemy health</returns>
    public float getHealthNormalized()
    {
        return (float)currentHealth / healthByLevel[enemyLevel];
    }

    /// <returns>the enemy level</returns>
    public int getCurrentLevel()
    {
        return this.enemyLevel;
    }
}
