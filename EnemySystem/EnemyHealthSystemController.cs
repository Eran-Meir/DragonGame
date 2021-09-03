using UnityEngine;

public class EnemyHealthSystemController : MonoBehaviour
{
    [SerializeField] private EnemyHealthSystemView healthSystemView; // This will be attached to the main health slider
    [SerializeField] private EnemyHealthSystemModel enemyHealthSystemModel;

    private int playerLevel;

    /// <summary>
    /// Initialize the enemy health system controller
    /// creates the model and gets the view
    /// </summary>
    private void Start()
    {
        playerLevel = Enemy.getPlayerLevel();
        enemyHealthSystemModel = gameObject.GetComponent<EnemyHealthSystemModel>();
        healthSystemView.setEnemyHealthSystemModel(enemyHealthSystemModel);
        healthSystemView = GetComponentInChildren<EnemyHealthSystemView>();
        healthSystemView.refreshView();
    }
    /// <summary>
    /// On Update
    /// </summary>
    private void Update()
    {
        // This is for debugging and will be removed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float healthToSub = 50f;
            enemyHealthSystemModel.subtractHealth(healthToSub, healthSystemView);
            Debug.Log("Subtracted " + healthToSub + " HP");
        }
    }

    /// <returns>returns the health system model</returns>
    public EnemyHealthSystemModel getEnemyHealthSystemModel()
    {
        return this.enemyHealthSystemModel;
    }

    /// <summary>
    /// Subtracts health from the enemy
    /// </summary>
    /// <param name="healthToSub">the amount of health to sub</param>
    public void enemySubtractHealth(float healthToSub)
    {
        enemyHealthSystemModel.subtractHealth(healthToSub, healthSystemView);
        Debug.Log("Subtracted " + healthToSub + " HP");
    }
}
