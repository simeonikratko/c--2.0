using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilities : MonoBehaviour
{
    //New
    [SerializeField]
    private Frenski playerObj;
    #region Fields
    public GameObject parent;

    float currentTime1 = 0f;
    float startingTime1 = 10f;
    float currentTime1f = 0f;
    float startingTime1f = 1f;

    float currentTime2 = 0f;
    float startingTime2 = 10f;

    float currentTime3 = 0f;
    float startingTime3 = 10f;

    float currentTime4 = 0f;
    float startingTime4 = 10f;
    float currentTime4f = 0f;
    float startingTime4f = 5f;

    float currentTime5 = 0f;
    float startingTime5 = 10f;

    float currentTime6 = 0f;
    float startingTime6 = 10f;

    float currentTime7 = 0f;
    float startingTime7 = 10f;

    float currentTime8 = 0f;
    float startingTime8 = 10f;
    float currentTime8f = 0f;
    float startingTime8f = 5f;

    float currentTime9 = 0f;
    float startingTime9 = 10f;
    #endregion

    void Start()
    {
        
    }

    #region Plant Abilities Buttons
    //Ready
    #region 1.Friendly Bush Button
    public void FriendlyBushButton()
    {
        currentTime1 -= 1 * Time.deltaTime;
        currentTime1f -= 1 * Time.deltaTime;

        if (Input.GetKeyDown("1") && currentTime1 <= 0)
        {
            currentTime1 = startingTime1;
            FriendlyBush();
            currentTime1f = startingTime1f;
        }

        if (currentTime1f <= 0)
        {
            parent.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    #endregion

    #region 2.Spikes Time Button
    public void SpikesTimeButton()
    {

    }
    #endregion

    #region 3.Nature’s Revenge Button
    public void NatureRevengeButton()
    {

    }
    #endregion
    #endregion

    #region Air Abilities Button
    //Ready
    #region 1.Fast as the wind
    public void FastAsTheWindButton()
    {
        currentTime4 -= 1 * Time.deltaTime;
        currentTime4f -= 1 * Time.deltaTime;

        if (Input.GetKeyDown("4") && currentTime4 <= 0)
        {
            currentTime4 = startingTime4;
            playerObj.isFastAsTheWind = true;
            currentTime4f = startingTime4f;
        }

        if (currentTime4f <= 0)
        {
            playerObj.isFastAsTheWind = false;
        }
    }
    #endregion

    #region 2.Mighty Winds Button
    public void MightyWindsButton()
    {

    }
    #endregion

    #region 3.Stormy place Button
    public void StormyPlaceButton()
    {

    }
    #endregion
    #endregion

    #region Water Abilities Buttons
    //Ready
    #region 1.Healing Waters Button
    public void HealingWatersButton()
    {
        currentTime7 -= 1 * Time.deltaTime;
        if (Input.GetKeyDown("7") && currentTime7 <= 0)
        {
            currentTime7 = startingTime7;
            FindObjectOfType<HealthBar>().GainHealth(50);
        }
    }
    #endregion

    #region 2.Frostbite Button
    public void FrostbiteButton()
    {

    }
    #endregion

    #region 3.Tsunami Button
    public void TsunamiButton()
    {

    }
    #endregion
    #endregion





    #region Plant Abilities
    #region 1.Friendly Bush
    public void FriendlyBush()
    {
        Debug.Log("Friendly Bush");
        parent.transform.GetChild(0).gameObject.SetActive(true);
    }
    #endregion

    #region 2.Spikes Time
    public void SpikesTime()
    {

    }
    #endregion

    #region 3.Nature’s Revenge
    public void NatureRevenge()
    {

    }
    #endregion
    #endregion

    #region Air Abilities
    #region 1.Fast as the wind
    public void FastAsTheWind()
    {
        //Animation
    }
    #endregion

    #region 2.Mighty Winds
    public void MightyWinds()
    {

    }
    #endregion

    #region 3.Stormy place
    public void StormyPlace()
    {

    }
    #endregion
    #endregion

    #region Water Abilities
    #region 1.Healing Waters
    public void HealingWaters()
    {
        //Animation
    }
    #endregion

    #region 2.Frostbite
    public void Frostbite()
    {

    }
    #endregion

    #region 3.Tsunami
    public void Tsunami()
    {

    }
    #endregion
    #endregion
}
