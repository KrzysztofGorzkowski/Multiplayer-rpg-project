using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player
{
    private NetworkObject playerObject;
    
    public Player(Vector2Int startPos)
    {
        playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
        playerObject.transform.position = new Vector3(startPos.x+1.05f, startPos.y+1.25f, -2f);
    }

    public NetworkObject getPlayerObject()
    {
        return playerObject;
    }

    public void mobilitySwitch()
    {
        playerObject.GetComponent<PlayerMovement>().enabled = !playerObject.GetComponent<PlayerMovement>().enabled;
        playerObject.GetComponent<Shooting>().enabled = !playerObject.GetComponent<Shooting>().enabled;
    }
}
