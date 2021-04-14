using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class BlackCloud : MonoBehaviour
{
    #region It works
    /*
    public float speed;
    public float lineOfSite;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }*/
    #endregion
    #region It maybe works
    #region Fields
    public float speed;
    public float lineOfSite;
    private Transform player;
    bool facingRight = false;
    bool canAttack = false;
    bool isDead = false;
    int decayAmount = 15;
    int health = 75;
    #endregion

    private void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void Start()
    {
        #region Things that are called before the first frame update
        player = GameObject.FindGameObjectWithTag("Player").transform;
        #endregion
    }

    void Update()
    {
        #region Thinks that are called once per frame
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
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
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        if (canAttack)
        {
            RealAttack();
        }
        #region Thisis for the special attacks
        if (Input.GetKeyDown("z"))
        {
            LoseHealth(100);
            transform.localScale = new Vector3(1, 1, 1);
            //Play Animation
        }
        #endregion
        #endregion
    }

    #region Attack?
    void RealAttack()
    {
        //Invoke("OnTriggerEnter2D", 3);
        //Invoke("Attack", 10);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canAttack = true;
            //Debug.Log($"{name} Triggered");
            
            /*transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, player.position.y + 1), -speed * Time.deltaTime * 30);*/
        }
        else
        {
            canAttack = false;
        }
    }

    void Attack()
    {
        FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
    }
    #endregion
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

    private void OnDrawGizmosSelected()
    {
        #region Gizmos
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        #endregion
    }
    #endregion
}
