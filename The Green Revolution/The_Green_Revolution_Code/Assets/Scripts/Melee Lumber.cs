using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLumber : MonoBehaviour
{
    public GameObject parent;
    float currentTime3 = 0f;
    float startingTime3 = 10f;
    float currentTime3f = 0f;
    float startingTime3f = 1f;

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

    void Naturerevenge()
    {
        parent.transform.GetChild(0).gameObject.SetActive(true);
    }
}
