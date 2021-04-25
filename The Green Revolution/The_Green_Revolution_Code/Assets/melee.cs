using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public GameObject parent;
    float currentTime3 = 0f;
    float startingTime3 = 10f;
    float currentTime3f = 0f;
    float startingTime3f = 0.3f;
    float currentTime3fd = 0f;
    float startingTime3fd = 4f;
    int health = 75;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Nature's Revenge button
        currentTime3 -= 1 * Time.deltaTime;
        currentTime3f -= 1 * Time.deltaTime;

        if (Input.GetKeyDown("3") && currentTime3 <= 0)
        {
            currentTime3 = startingTime3;
            Naturerevenge();
            currentTime3f = startingTime3f;
        }
        if (currentTime3f <= 0)
        {
            parent.transform.GetChild(0).gameObject.SetActive(false);
        }
        #endregion
    }

    #region Health
    public void LoseHealth(int value)
    {
        #region Losing health
        //Do nothing if you are out of health
        if (health <= 0)
            return;
        //Reduce the health
        health -= value;
        //Check if your health is zero or less => Dead
        if (health <= 0)
        {
            Die();
        }
        #endregion
    }

    void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
    #endregion

    void Naturerevenge()
    {
        parent.transform.GetChild(0).gameObject.SetActive(true);
        LoseHealth(74);
        currentTime3fd -= 1 * Time.deltaTime;
        if (currentTime3fd <= 0)
        {

        }
    }
}