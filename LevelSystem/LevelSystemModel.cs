using UnityEngine;

public class LevelSystemModel : MonoBehaviour
{
    private const int STARTLEVEL = 0;
    private const float STARTEXP = .0f;
    private int currentLevel;
    private float currentExperience;
    private int[] experienceToNextLevel = { 20, 20, 20, 20, 1600, 3200, 6400, 12800, 25600, 51200 };
    /// these will be changed after tweaking the game
    private float[] dragonTransformScale = { 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f, 1.1f };
    private float[] healthByLevel = { 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200 };
    private float[] healthRegenRateByLevel = { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4.5f, 5f, 7.5f, 10f };
    private float[] manaByLevel = { 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200 };
    private float[] manaRegenRateByLevel = { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4.5f, 5f, 7.5f, 10f };
    private float[] fireDamageByLevel = { 5, 10, 20, 30, 40, 50, 60, 70, 100 };
    private float[] fireballDamageByLevel = { 5, 10, 20, 30, 40, 50, 60, 70, 100 };
    private float[] bodyDamageByLevel = { 5, 10, 20, 30, 40, 50, 60, 70, 100 };
    private MalbersAnimations.Stats dragonStats;
    private MalbersAnimations.Stat dragonHealthStat, dragonManaStat;
    // Start is called before the first frame update

    /// <summary>
    /// Constructs the level system model
    /// </summary>
    public LevelSystemModel()
    {
        currentLevel = STARTLEVEL;
        currentExperience = STARTEXP;
    }

    /// <summary>
    /// Adds experience to the user
    /// </summary>
    /// <param name="expAmountToAdd">the amount to add</param>
    /// <param name="levelSliderView">the view to refresh afterwards</param>
    public void addExperience(float expAmountToAdd, LevelSliderView levelSliderView)
    {
        currentExperience += expAmountToAdd;
        while (currentExperience >= experienceToNextLevel[currentLevel])
        {
            currentExperience -= experienceToNextLevel[currentLevel];
            currentLevel++;
            // Scale up
            if (currentLevel < dragonTransformScale.Length)
            {
                playerTransformScaleUp(); // Make the dragon bigger
                playerStatsLevelUp(); // Make the dragon stronger
            }
        }
        levelSliderView.refreshView();
    }

    /// <summary>
    /// Make the Dragon bigger on level up.
    /// </summary>
    private void playerTransformScaleUp()
    {
        GameObject.FindGameObjectWithTag("Player").transform.localScale =
    new Vector3(dragonTransformScale[currentLevel], dragonTransformScale[currentLevel], dragonTransformScale[currentLevel]);
    }

    /// <summary>
    /// Make the Dragon Stronger on level up
    /// </summary>
    private void playerStatsLevelUp()
    {
        dragonStats = GameObject.FindGameObjectWithTag("Player").GetComponent<MalbersAnimations.Stats>();
        dragonHealthStat = dragonStats.Stat_Get(1);
        dragonManaStat = dragonStats.Stat_Get(3);
        if (!dragonStats)
            Debug.LogError("No dragon stats set");
        else
        {
            levelUpHealthStat();
            levelUpManaStat();
            levelUpDamage();
        }
    }

    /// <summary>
    /// Levels up the Malbers Health Stat
    /// </summary>
    private void levelUpHealthStat()
    {
        // Health
        dragonHealthStat.SetRegeneration(true); // Can regenerate
        if (currentLevel < healthRegenRateByLevel.Length)
            dragonHealthStat.SetRegenerationRate(healthRegenRateByLevel[currentLevel]); // regenaration points over time
        if (currentLevel < healthByLevel.Length)
        {
            dragonHealthStat.SetMAX(healthByLevel[currentLevel]); // max health value
            dragonHealthStat.SetValue(healthByLevel[currentLevel]); // change current health value
        }
    }

    /// <summary>
    /// Levels up the Malbers Mana Stat
    /// </summary>
    private void levelUpManaStat()
    {
        // Mana
        dragonManaStat.SetRegeneration(true); // Can regenerate
        if (currentLevel < manaRegenRateByLevel.Length)
            dragonManaStat.SetRegenerationRate(manaRegenRateByLevel[currentLevel]); // regenaration points over time
        if (currentLevel < manaByLevel.Length)
        {
            dragonManaStat.SetMAX(manaByLevel[currentLevel]); // max health value
            dragonManaStat.SetValue(manaByLevel[currentLevel]); // change current health value
        }
    }

    /// <summary>
    /// Levels up the Malbers Damage Stat, this includes fire, fireball, claws and tail damage
    /// </summary>
    private void levelUpDamage()

    {
        // Level up fire

        // Level up fireball 

        // Level up claws

        // Level up Tail
    }

    /// <summary>
    /// Returns the current normalized experience
    /// </summary>
    /// <returns>current normalized experience</returns>
    public float getExperienceNormalized()
    {
        return (float)currentExperience / experienceToNextLevel[currentLevel];
    }

    /// <summary>
    /// Gets the current level
    /// </summary>
    /// <returns>the current player level</returns>
    public int getCurrentLevel()
    {
        return this.currentLevel;
    }
}
