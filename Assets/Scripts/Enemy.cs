using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject currentObject;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            // play animations
            Die();

            // remove current object from scene
            Destroy(currentObject);
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead...");

        // die animation
        animator.SetBool("IsDead", true);

        // disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
