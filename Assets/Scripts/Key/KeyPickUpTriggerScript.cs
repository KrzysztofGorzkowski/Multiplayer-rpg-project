using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KeyPickUpTriggerScript : NetworkBehaviour
{
    public delegate void KeyPickUp();
    public static event KeyPickUp KeyPickedUp;
    //public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dupa");
        if (collision.tag == "KeysCollector")
        {
            PickUpKey();
        }
    }

    private void PickUpKey()  //function that is called to pick up the key
    {
        transform.GetComponent<Collider2D>().enabled = false;
        DespawnServerRpc();
        if (KeyPickedUp != null)
        {
            //animator.Play("KeyPicked");
            Debug.Log("KeyPickUpEvent Invoked");
            KeyPickedUp();
            GetComponent<NetworkObject>().Despawn();
        }
        //Labyrinth.numberOfPickedUpKeys++;
        //Debug.Log("Zebrano " + Labyrinth.numberOfPickedUpKeys.ToString() + " z " + Labyrinth.numberOfKeys.ToString() + " kluczy");
    }


    [ServerRpc (RequireOwnership = false)]
    private void DespawnServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
