using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelload : MonoBehaviour
{

    public int ileveltoload;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Entering the new level
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Frenski")
        {

            LoadScene();

        }
        #endregion
    }


    void LoadScene()
    {
        #region Load next level
        SceneManager.LoadScene(ileveltoload);
        #endregion
    }

}