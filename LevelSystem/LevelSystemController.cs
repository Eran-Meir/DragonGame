using UnityEngine;

public class LevelSystemController : MonoBehaviour
{
    [SerializeField] private LevelSliderView levelSliderView;
    LevelSystemModel levelSystemModel = new LevelSystemModel();
    /// <summary>
    /// Initialize the Level System Controller, set the model and the view
    /// </summary>
    private void Awake()
    {
        levelSliderView = GameObject.Find("Slider Experience").GetComponent<LevelSliderView>();
        levelSliderView.setLevelSystem(levelSystemModel);
        levelSliderView.refreshView();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>the level system model</returns>
    public LevelSystemModel getLevelSystemModel()
    {
        return this.levelSystemModel;
    }

    /// <summary>
    /// Adds experience to the player
    /// </summary>
    /// <param name="expToAdd">amount to add</param>
    public void addExperience(float expToAdd)
    {
        this.levelSystemModel.addExperience(expToAdd, levelSliderView);
        Debug.Log("Added " + expToAdd + " Experience from killing a skeleton!");
    }
}
