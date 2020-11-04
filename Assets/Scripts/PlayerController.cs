using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;

    public float speed;
    private float moveInput;
    public bool isGrounded;
    private bool facingRight = true;
    public float jumpHeight;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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
        Jump();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // determing the player movement input
        moveInput = Input.GetAxis("Horizontal") * speed;
        
        // setting the animator var Speed to play animations
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // If the input is moving the player right and the player is facing left...
        if (moveInput > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveInput < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // moving the player based on moveInput
        rb.velocity = new Vector2(moveInput, rb.velocity.y);
    }

    ///<Summary>
    /// This funciton is called whenever the player needs to take damage from the enemy
    ///<Summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // play hurt animation
        //animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            // play animations
            //Die();

            // remove current object from scene
            //Destroy(currentEnemyObject);
        }
    }

    /// <summary>
    /// This function is called to determine if the player can jump.
    /// </summary>
    void Jump()
    {
        // if the Jump button is pressed && they are on the ground,play the animation and add the force to the y of the players rigidbody
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpHeight), ForceMode2D.Impulse);
        }
        else animator.SetBool("IsJumping", false);
    }

    /// <summary>
    /// This function is called to determine what direction the player needs to be facing.
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
