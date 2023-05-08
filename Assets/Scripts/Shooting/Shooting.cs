using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;


public class Shooting : NetworkBehaviour
{
    private float _bulletForce = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    //public Animator animator;

    public float _fireRate = 0.5F;
    private float _nextFire = 0.0F;

    // Update is called once per frame
    void Update()
    { 
        if (IsOwner && Input.GetButtonDown("Fire1") && Time.time > _nextFire && Time.timeScale != 0f)
        {
            _nextFire = Time.time + _fireRate;
            //animator.SetTrigger("Shot");
            this.ShootServerRpc();
        }
    }
    [ServerRpc]
    public void ShootServerRpc()
    {

        Debug.Log(firePoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  //spawn bulet
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Bullet>().SetDamage(PlayerDatabase.Damage()); //????? czy to dobrze??
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.GetChild(0).GetComponent<Collider2D>());    //ignoring player collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), firePoint.GetComponent<Collider2D>());                //ignoring firePoint collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), NetworkManager.Singleton.LocalClient.PlayerObject.transform.GetChild(0).GetComponent<Collider2D>());    //ignoring player collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), NetworkManager.Singleton.LocalClient.PlayerObject.transform.GetChild(2).GetComponent<Collider2D>());                //ignoring firePoint collisions
        Rigidbody2D rigidbody =  bullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(firePoint.up * _bulletForce, ForceMode2D.Impulse);
    }
}
