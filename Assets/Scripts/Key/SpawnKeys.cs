using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnKeys : NetworkBehaviour
{
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        if (IsServer)
        {
            this.spawnKeys();
        }
    }


    private void spawnKeys()
    {

        GameObject go = Instantiate(key, new Vector3(2, -2.5f, 0), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
        go = Instantiate(key, new Vector3(3, -2.5f, 0), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
        go = Instantiate(key, new Vector3(4, -2.5f, 0), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
    }

}
