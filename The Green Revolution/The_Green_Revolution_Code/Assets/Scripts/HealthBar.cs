using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public float health;
    float currentTime7 = 0f;
    float startingTime7 = 10f;

    public void LoseHealth(int value)
    {
        #region Losing health
        //Do nothing if you are out of health
        if (health <= 0)
            return;
        //Reduce the health
        health -= value;
        //Refresh the UI fillBar
        fillBar.fillAmount = health / 100;
        //Check if your health is zero or less => Dead
        if(health <= 0)
        {
            FindObjectOfType<Frenski>().Die();
        }
        #endregion
    }

    public void GainHealth(int value)
    {
        #region Heal
        //Do nothing if you are out of health
        if (health <= 0)
            return;
        //Increase the health
        health += value;
        //Refresh the UI fillBar
        fillBar.fillAmount = health / 100;
        if (health > 100)
        {
            health = 100;
        }
        #endregion
    }
}
