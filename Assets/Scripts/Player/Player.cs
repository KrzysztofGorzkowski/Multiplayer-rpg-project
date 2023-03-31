using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private GameObject playerObject;
    
    public Player(Vector2Int startPos)
    {
        GameObject playerPrefab = Resources.Load("Player") as GameObject;
        playerObject = GameObject.Instantiate<GameObject>(playerPrefab);
        playerObject.transform.position = new Vector3(startPos.x+1.05f, startPos.y+1.25f, -2f);
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }

    public void mobilitySwitch()
    {
        playerObject.GetComponent<PlayerMovement>().enabled = !playerObject.GetComponent<PlayerMovement>().enabled;
        playerObject.GetComponent<Shooting>().enabled = !playerObject.GetComponent<Shooting>().enabled;
    }
}
