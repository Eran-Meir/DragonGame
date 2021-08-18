using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystemView : MonoBehaviour
{
    [SerializeField] public Text levelText;
    [SerializeField] public Slider healthSlider;
    private EnemyHealthSystemModel healthSystemModel; // This will be set by the controller "parent" who constructs the model and the view 

    /// <summary>
    /// Initializes the view on Awake
    /// Sets the enemy level text and the health view slider
    /// </summary>
    private void Awake()
    {
        levelText = this.transform.parent.GetComponentInChildren<Text>();
        healthSlider = this.GetComponent<Slider>();
    }
    /// <summary>
    /// Refreshes the view on Start
    /// </summary>
    private void Start()
    {
        this.refreshView();
    }

    /// <summary>
    /// Set the enemy health bar fill
    /// </summary>
    /// <param name="healthNormalized">The normalized amount to be filled</param>
    private void setHealthBarFill(float healthNormalized)
    {
        healthSlider.value = healthNormalized;
    }

    /// <summary>
    /// Sets the enemy level number text
    /// Note that while the actual levels start at 0, the user sees this as plus one i.e level 0 will be shown as level 1
    /// </summary>
    private void setEnemyLevelNumberText(int levelNumberToSet)
    {
        levelText.text = "Level: " + levelNumberToSet;
    }

    /// <summary>
    /// Sets the enemy health system model
    /// </summary>
    /// <param name="enemyHealthSystemModelToSet">the enemy health system model</param>
    public void setEnemyHealthSystemModel(EnemyHealthSystemModel enemyHealthSystemModelToSet)
    {
        this.healthSystemModel = enemyHealthSystemModelToSet;
        this.refreshView();
    }

    /// <summary>
    /// Refreshes the view
    /// </summary>
    public void refreshView()
    {
        if (healthSystemModel != null)
        {
            setEnemyLevelNumberText(healthSystemModel.getCurrentLevel());
            setHealthBarFill(healthSystemModel.getHealthNormalized());
        }
        else
        {
            Debug.LogError("Cannot refreshView of enemy");
        }
    }
}
