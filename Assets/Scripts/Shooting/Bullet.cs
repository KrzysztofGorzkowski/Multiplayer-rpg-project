using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    //public Rigidbody2D rigidbody;
    //public Animator animator;
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
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PlayerWand")) return;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //animator.Play("BulletExplosion");
        DestoryServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestoryServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();   
        //Destroy(gameObject);
    }

}
