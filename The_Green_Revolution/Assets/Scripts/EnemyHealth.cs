using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    bool Dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Dead = true;
        Debug.Log("Enemy died!");

        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BlackCloud>().enabled = false;
        this.enabled = false;
    }
}