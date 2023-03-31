using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsManager : MonoBehaviour
{
    public Stat armor; //do dodania w bazie danych
    public Animator animator;

    private void Start()
    {
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    public void TakeDamage(int damage)       ////function that is called to take damage
    {
        animator.SetTrigger("Damage");
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        PlayerDatabase.currentHp -= damage;
        Debug.Log(transform.name + "takes " + damage + " damege");
        
        if (PlayerDatabase.currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.Play("Die");
    }

}


