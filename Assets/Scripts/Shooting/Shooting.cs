using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;


public class Shooting : NetworkBehaviour
{
    private readonly NetworkVariable<float> _bulletForce = new (writePerm: NetworkVariableWritePermission.Owner);
    public Transform firePoint;
    public GameObject bulletPrefab;
    //public Animator animator;

    public float _fireRate = 0.5F;
    private float _nextFire = 0.0F;

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            _bulletForce.Value = 3f;
        }
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (IsOwner && Input.GetButtonDown("Fire1") && Time.time > _nextFire && Time.timeScale != 0f)
        {
            _nextFire = Time.time + _fireRate;
            //animator.SetTrigger("Shot");
            this.ShootServerRpc(firePoint.up);
        }
    }
    [ServerRpc]
    public void ShootServerRpc(Vector2 y)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  //spawn bulet
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Bullet>().SetDamage(PlayerDatabase.Damage()); //????? czy to dobrze??
        Physics2D.IgnoreLayerCollision(7,6);    //ignoring ALL players collisions using LAYERS (player have Layer 6 named "Action" bullet has layer "Bullet" (7)
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.GetChild(0).GetComponent<Collider2D>());    //ignoring player collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), firePoint.GetComponent<Collider2D>());                //ignoring firePoint collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), NetworkManager.Singleton.LocalClient.PlayerObject.transform.GetChild(0).GetComponent<Collider2D>());    //ignoring player collisions
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), NetworkManager.Singleton.LocalClient.PlayerObject.transform.GetChild(2).GetComponent<Collider2D>());                //ignoring firePoint collisions
        Rigidbody2D rigidbody =  bullet.GetComponent<Rigidbody2D>();
        Debug.Log("Y: " + y);
        rigidbody.AddForce(y * _bulletForce.Value, ForceMode2D.Impulse);
    }
}
