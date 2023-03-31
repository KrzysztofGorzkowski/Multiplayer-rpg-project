using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && GameManager.isFinishActive)
        {
            LabyrinthDatabase.labyrinthLvl++; //increase lvl 
            GameManager.GetPlayer().mobilitySwitch();
            Animator animator = GameManager.GetPlayer().getPlayerObject().transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            GameManager.GetPlayer().getPlayerObject().transform.position = new Vector3(transform.position.x+0.02f, transform.position.y+0.17f, -2f);
            animator.Play("Disappear");
        }
    }
}
