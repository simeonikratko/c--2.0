using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Angry_cloud : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    bool facingRight = false;
    int decayAmount = 15;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float ditanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (ditanceFromPlayer < lineOfSite)
        {
            if (facingRight && this.transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3(4, 4, 1);
                facingRight = false;
            }
            else if (!facingRight && this.transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3(-4, 4, 1);
                facingRight = true;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, player.position.y + 1), speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log($"{name} Triggered");
            FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
            /*transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, player.position.y + 1), -speed * Time.deltaTime * 30);*/
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
