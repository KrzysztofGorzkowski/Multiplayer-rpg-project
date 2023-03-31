using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Rigidbody2D rigidbody;
    public Animator animator;
    private int _damage;

    public void SetDamage(int damage)      //set bullet damage
    {
        _damage = damage;
    }

    public int GetDamage()      //get damage from bullets
    {
        return _damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("BulletExplosion");
    }

    private void End()
    {
        Destroy(gameObject);
    }

}
