using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Happy_cloud_bullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    bool bad = false;
    private Animator anim;
    int decayAmount = 15;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        bad = FindObjectOfType<Happier_cloud>().bad;
        Debug.Log($"{name} {bad}");

        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (bad)
            {
                Debug.Log($"{name} Triggered");
                FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"{name} Triggered");
                FindObjectOfType<HealthBar>().GainHealth(decayAmount);
                Destroy(gameObject);
            }
        }
        if (collision.tag == "Tilemap")
        {
           Destroy(gameObject);
        }
    }
}