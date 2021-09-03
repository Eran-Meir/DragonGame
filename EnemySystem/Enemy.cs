using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static string isIdle = "isIdle", isWalking = "isWalking", isAttacking = "isAttacking", isDead = "isDead", isDying = "isDying", isReceivingDamage = "isReceivingDamage";

    /// <returns>the enemy level</returns>
    public abstract int getEnemyLevel();
    
    /// <summary>
    /// Sets the enemy level
    /// </summary>
    /// <param name="level">level to set</param>
    public abstract void setEnemyLevel(int level);
    
    /// <returns>the enemy damage</returns>
    public abstract int getEnemyDamage();

    /// <summary>
    /// Sets the enemy damage
    /// </summary>
    /// <param name="damage">damage to set</param>
    public abstract void setEnemyDamage(int damage);

    /// <returns>the enemy speed</returns>
    public abstract float getEnemySpeed();
    
    /// <summary>
    /// Sets the enemy speed
    /// </summary>
    /// <param name="speed">enemy speed to set</param>
    public abstract void setEnemySpeed(float speed);

    /// <returns>the enemy experience prize</returns>
    public abstract float getEnemyPrize();

    /// <summary>
    /// Sets the enemy expereince prize
    /// </summary>
    /// <param name="expPrize">experience prize for the player</param>
    public abstract void setEnemyPrize(float expPrize);

    /// <summary>
    /// Within this radius the player is detectable to the enemy
    /// </summary>
    /// <returns>the look radius of the enemy</returns>
    public abstract float getLookRadius();

    /// <summary>
    /// Sets the player detection radius for the enemy
    /// </summary>
    /// <param name="radius">the radius to set</param>
    public abstract void setLookRadius(float radius);

    /// <summary>
    /// Invoke death upon enemy
    /// </summary>
    public abstract void enemyDie();
    
    /// <returns>The Player level</returns>
    public static int getPlayerLevel()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        LevelSystemController levelSystemController = player.GetComponent<LevelSystemController>();
        Debug.Log(levelSystemController.ToString());
        if (levelSystemController != null)
            return levelSystemController.getLevelSystemModel().getCurrentLevel();
        else
            throw new System.NullReferenceException("Couldn't get the Level System Controller");
    }
}
