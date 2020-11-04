using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private GameObject currentEnemyObject;
    private Rigidbody2D rb;
    public GameObject player;
    
    public int maxHealth = 100;
    private int currentHealth;
    
    public float movementSpeed = 2f;
    public bool inRangeForPathing = false;
    public bool inRangeForCombat = false;

    ///<Summary>
    /// Start is called before the first frame update
    ///<Summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentEnemyObject = gameObject;

        currentHealth = maxHealth;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        inRangeForPathing = GetComponentInChildren<EnemyPathing>().inRangeForPathing;

        if (inRangeForPathing)
        {
            inRangeForCombat = GetComponentInChildren<EnemyCombat>().inRangeForCombat;
        }
    }

    ///<Summary>
    /// This funciton is called whenever the enemy needs to take damage from the player
    ///<Summary>
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
            Destroy(currentEnemyObject);
        }
    }

    ///<Summary>
    /// This funciton is called once the enemy's health is <= 0
    ///<Summary>
    void Die()
    {
        // die animation
        animator.SetBool("IsDead", true);

        // disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
