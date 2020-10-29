using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("WeaponAttack"))
        {
            Attack();
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    void Attack()
    {
        // play an attack animation
        animator.SetTrigger("Attack");
        // detect enemies in range of attack
        // damage enemies
    }
}
