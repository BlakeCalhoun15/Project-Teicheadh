using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject currentObject;
    private Rigidbody2D rb;
    
    public int maxHealth = 100;
    private int currentHealth;
    
    public float movementSpeed = 2f;
    public float moveDistanceCheck = 1f;
    public List<Transform> waypoints;
    public int nextPointIndex = 0;
    private int idChangeValue = 1;

    private bool inRange = false;

    ///<Summary>
    /// Start is called before the first frame update
    ///<Summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // check if the player is in range of the enemy
        //    if true -> have enemy follow the player until out of range
        //    if false -> MoveToNextPoint();
        if (!inRange)
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        // get the next point transform
        Transform goalPoint = waypoints[nextPointIndex];
        
        // flip enemy transform
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else transform.localScale = new Vector3(-1,1,1);
       
        // move enemy towards goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, movementSpeed * Time.deltaTime);
        
        // check distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < moveDistanceCheck)
        {
            // check if we are at the end of the line (make change -1)
            if (nextPointIndex == waypoints.Count - 1)
            {
                idChangeValue = -1;
            }
            
            // check if we are at the start of the line (make chnge +1)
            if (nextPointIndex == 0)
            {
                idChangeValue = 1;
            }
            
            // apply the change on the nextPointIndex
            nextPointIndex += idChangeValue;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        
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
            Destroy(currentObject);
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
