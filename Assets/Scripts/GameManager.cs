using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
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

    public static void LoadLabirynth()
    {
        new LabyrinthDatabase();
        LabyrinthDatabase.ResetStats();
        _labyrinth = new Labyrinth(LabyrinthDatabase.LabrynthSize());
    }
    public static Labyrinth GetLabirynth()
    {
        return _labyrinth;
    }
}
