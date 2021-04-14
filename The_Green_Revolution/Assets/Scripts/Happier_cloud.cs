using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happier_cloud : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    bool facingRight = false;
    bool facingUp = false;
    private Animator anim;

    /*Random rd = new Random();

    int rand_num = rd.Next(100, 200);*/

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
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
            if (facingUp && this.transform.position.y > player.position.y)
            {
                anim.SetBool("IsFlyingUp", false);
                anim.SetBool("IsFlyingDown", true);
                facingUp = false;
            }
            else if (!facingUp && this.transform.position.y < player.position.y)
            {
                anim.SetBool("IsFlyingDown", false);
                anim.SetBool("IsFlyingUp", true);
                facingUp = true;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, player.position.y + 1), speed * Time.deltaTime);
            anim.SetBool("IsFlyingHorizontal", true);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            anim.SetBool("IsFlyingUp", false);
            anim.SetBool("IsFlyingDown", false);
            anim.SetBool("IsFlyingHorizontal", false);
            if (FindObjectOfType<HealthBar>().health != 100)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }

        }
        else
        {
            anim.SetBool("IsFlyingUp", false);
            anim.SetBool("IsFlyingDown", false);
            anim.SetBool("IsFlyingHorizontal", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}