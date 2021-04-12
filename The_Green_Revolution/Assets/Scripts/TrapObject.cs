using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TrapObject : MonoBehaviour
{
    int decayAmount = 25;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Check if the layer touch it 
        if (collision.tag == "Player")
        {
            Debug.Log($"{name} Triggered");
            FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
        }
        #endregion
    }
}
