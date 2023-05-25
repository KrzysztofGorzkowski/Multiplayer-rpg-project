using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUpTriggerScript : MonoBehaviour
{
    public delegate void KeyPickUp();
    public static event KeyPickUp KeyPickedUp;
    public Animator animator;

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
        if (KeyPickedUp != null)
        {
            animator.Play("KeyPicked");
            Debug.Log("KeyPickUpEvent Invoked");
            KeyPickedUp();
        }
        //Labyrinth.numberOfPickedUpKeys++;
        //Debug.Log("Zebrano " + Labyrinth.numberOfPickedUpKeys.ToString() + " z " + Labyrinth.numberOfKeys.ToString() + " kluczy");
    }

    private void End()
    {
        Destroy(transform.gameObject);
    }
}
