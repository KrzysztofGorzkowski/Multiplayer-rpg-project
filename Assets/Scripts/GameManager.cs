using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    //private const int LABYRINTH_SIZE = 9;  //the size of the labyrinth corresponding to the length of the side of the square labyrinth
    private static Labyrinth _labyrinth;
    private static Player _player;
    private static GameObject _camera;

    public static bool isFinishActive; //info is the finish active (needed to swap the finish tile)

    // Start is called before the first frame update
    void Start()
    {
        isFinishActive = false;
        new LabyrinthDatabase();
        LabyrinthDatabase.ResetStats();
        _labyrinth = new Labyrinth(LabyrinthDatabase.LabrynthSize()); //cutting the labyrinth and creating a labyrinth graph
        _player = new Player(_labyrinth.GetStartPos());             //creating a player
        LoadCamera();                                               //creating a camera to track the player

    }

    void Update()
    {
        if ((!isFinishActive)&&(Labyrinth.numberOfKeys==Labyrinth.numberOfPickedUpKeys))
        {
            isFinishActive = true;
            _labyrinth.SwapTeleportEndTile();
        }
    }


    private void LoadCamera() //the function of creating a camera to track the player
    {
        GameObject cameraPrefab = Resources.Load("Camera") as GameObject;
        _camera = GameObject.Instantiate<GameObject>(cameraPrefab);
        _camera.GetComponent<PlayerCam>().SetTarget(_player.getPlayerObject());
    } 

    public static GameObject GetCamera()
    {
        return _camera;
    }

    public static Player GetPlayer()
    {
        return _player;
    }

    public static Labyrinth GetLabirynth()
    {
        return _labyrinth;
    }
}
