using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KeyPickUpTriggerScript : NetworkBehaviour
{
    public delegate void KeyPickUp();
    public static event KeyPickUp KeyPickedUp;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KeysCollector")
        {
            PickUpKey();
        }
    }

    private void PickUpKey()  //function that is called to pick up the key
    {
        transform.GetComponent<Collider2D>().enabled = false;
        DespawnServerRpc();
    }


    [ServerRpc (RequireOwnership = false)]
    private void DespawnServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
