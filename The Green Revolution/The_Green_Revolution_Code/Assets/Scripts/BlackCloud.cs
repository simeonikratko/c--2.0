using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class BlackCloud : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    bool facingRight = false;
    int decayAmount = 10;
    float currentTime = 0f;
    float startingTime = 1f;
    //int health = 75;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTime = startingTime;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0)
            {
                Debug.Log($"{name} Triggered");
                FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
                currentTime = startingTime;
            }
        }
    }

    /*
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
        Debug.Log($"{name} is Dead");
        gameObject.SetActive(false);
    }
    #endregion
    */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}