using UnityEngine;
using UnityEngine.UI;

public class LevelSliderView : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Slider experienceSlider;
    private LevelSystemModel levelSystem; // This belongs to the player
    private const int STARTLEVEL = 0;
    private const float STARTEXP = 0f;

    /// <summary>
    /// Sets the experience bar fill
    /// </summary>
    /// <param name="experienceNormalized">normalized amount to be set</param>
    private void setExperienceBarFill(float experienceNormalized)
    {
        experienceSlider.value = experienceNormalized;
        Debug.Log("setExperienceBarFill was called with " + experienceNormalized + " exp");
    }

    /// <summary>
    /// The player level text to viewed
    /// Note that while the actual levels start at 0, the user sees this as plus one i.e level 0 will be shown as level 1
    /// </summary>
    /// <param name="levelNumberToSet"></param>
    // 
    private void setLevelNumberText(int levelNumberToSet)
    {
        levelText.text = "Level: " + (levelNumberToSet + 1);
    }

    /// <summary>
    /// Set the level system model
    /// </summary>
    /// <param name="levelSystemToSet">the level system model to set</param>
    public void setLevelSystem(LevelSystemModel levelSystemToSet)
    {
        this.levelSystem = levelSystemToSet;
        setLevelNumberText(levelSystem.getCurrentLevel());
        setExperienceBarFill(levelSystem.getExperienceNormalized());
    }

    /// <summary>
    /// Refreshes the view
    /// For example, if the player earned more experience we probably need to refresh the view.
    /// </summary>
    public void refreshView()
    {
        setLevelNumberText(levelSystem.getCurrentLevel());
        setExperienceBarFill(levelSystem.getExperienceNormalized());
    }
}
