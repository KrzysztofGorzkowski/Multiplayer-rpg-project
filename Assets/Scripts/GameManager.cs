using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Labyrinth _labyrinth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerRpc]
    public static void LoadLabirynthServerRpc()
    {
        /*new LabyrinthDatabase();
        LabyrinthDatabase.ResetStats();
        _labyrinth = new Labyrinth(LabyrinthDatabase.LabrynthSize());*/
        /*
        GameObject objectToSpawn1 = GameObject.Find("Labyrinth");
        GameObject objectToSpawn2 = GameObject.Find("Tilemap");
        objectToSpawn1.GetComponent<NetworkObject>().Spawn(true);
        
        objectToSpawn2.AddComponent<NetworkObject>();
        NetworkManager.Singleton.AddNetworkPrefab(objectToSpawn2);
        objectToSpawn2.GetComponent<NetworkObject>().Spawn(true);
        objectToSpawn2.transform.parent = objectToSpawn1.transform;
        */
    }
    public static Labyrinth GetLabirynth()
    {
        return _labyrinth;
    }
}
