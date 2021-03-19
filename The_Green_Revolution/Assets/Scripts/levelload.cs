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

        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Frenski")
        {

            LoadScene();

        }

    }


    void LoadScene()
    {

        SceneManager.LoadScene(ileveltoload);

    }

}