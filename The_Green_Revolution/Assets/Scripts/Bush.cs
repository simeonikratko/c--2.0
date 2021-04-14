using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    int bushDecayAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
            return;

        if (collision.GetComponent<BushAction>())
            collision.GetComponent<BushAction>().Action();
    }
}
