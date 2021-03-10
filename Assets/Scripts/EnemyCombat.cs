using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private GameObject player;

    public float attackRange = 0.5f;
    public int attackDamage = 25;
    public float attackRate = 3f;
    float nextAttackTime = 0f;
    public bool inRangeForCombat = false;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (inRangeForCombat && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    void AttackPlayer()
    {
        // play attack animation
        // animator.SetTrigger("Attack");

        isAttacking = true;

        // detect player in range of attack
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position,attackRange,playerLayer);
        
        // damage player
        hitPlayer.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }
    
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRangeForCombat = true;
            //player = collision.gameObject;
        }
    }

    /// <summary>
    /// Sent when another object exits a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRangeForCombat = false;
        }
    }
}
