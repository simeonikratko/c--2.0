using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public float health;

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

    private void Update()
    {
        #region Lose health Button
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            LoseHealth(25);
        }*/
        #endregion
    }
}
