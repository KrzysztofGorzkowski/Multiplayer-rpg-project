using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    public EnemyStats _enemyStats;

    public delegate void KilledEnemy();
    public static event KilledEnemy ExpGained;
    public Animator animator;

    public int currentHealth { get; private set; }


    private void Start()
    {
        currentHealth = _enemyStats.hp;
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    public void TakeDamage(int damage)    //function that is called to take damage
    {

        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + "takes " + damage + " damage");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    public void Die()
    {
        PlayerDatabase.currentExp += _enemyStats.givenExp;
        if (ExpGained != null)
        {
            Debug.Log("ExpEvent Invoked");
            ExpGained();
        }
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyBehaviour>().enabled = false;   //set to false so that he no longer follows the player
        animator.Play("GhostDie");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            TakeDamage(collision.transform.GetComponent<Bullet>().GetDamage());    //taking damage from a player's bullet
        }
    }
        
}
