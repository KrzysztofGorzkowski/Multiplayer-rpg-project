using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Tilemaps;
using System.IO;
using Unity.Netcode;

public class Labyrinth
{
    private System.Random _rnd = new System.Random();
    private int[,] _tiles;                              //two-dimensional tiles array
    private int _maxLength;                             //is used to determine the farthest position of the end of the labyrinth
    private Vector2Int _startPos;                       //start position of the labyrinth
    private Vector2Int _finishPos;                      //end position of the labyrinth
    private Vector2Int _size;                           //corresponds to the length of the sides of the labyrinth
    private GameObject _labyrinthPrefab;                        //tilemap of the labyrinth

    private LabyrinthGraph _labyrinthGraph;

    private int _minumumDistanceBetweenEnemies;
    private int _minumumDistanceBetweenKeys;

    public static int numberOfKeys;
    public static int numberOfPickedUpKeys;

    public Labyrinth(int labyrinthSize)
    {
        numberOfKeys = 0;
        numberOfPickedUpKeys = 0;

        _size = new Vector2Int(labyrinthSize, labyrinthSize);           //set labyrrinth size

        _minumumDistanceBetweenEnemies = (int)(_size.x * 0.5f);         //simple formula to calculate the minimum distance between enemies                       
        _minumumDistanceBetweenKeys = (int)(_size.x * 0.7f);            //simple formula to calculate the minimum distance between keys   

        _tiles = new int[labyrinthSize, labyrinthSize];                 //set two-dimensional tiles array
        for (int i = 0; i < _tiles.GetLength(0); ++i)                   //complementing each tile with a wall
        {
            for (int j = 0; j < _tiles.GetLength(1); ++j)
            {
                _tiles[i, j] = (int)LabyrinthObject.Type.WALL;
            }
        }

        do
        {
            _startPos = new Vector2Int(_rnd.Next(1, _size.x), _rnd.Next(1, _size.y));   //set a random starting position of the labyrinth
        } while (_startPos.x % 2 == 0 || _startPos.y % 2 == 0);

        _tiles[_startPos.x, _startPos.y] = (int)LabyrinthObject.Type.PATH;              //set the starting position as PATH
        _finishPos = new Vector2Int();
        _maxLength = int.MinValue;

        int counter = 0;                                                                //helps determine the position furthest from the starting position

        DigTunnel(_startPos, counter);
        MakeLoops();

        _tiles[_startPos.x, _startPos.y] = (int)LabyrinthObject.Type.LABYRINTH_START;   //set the starting position as LABYRINTH_START
        _tiles[_finishPos.x, _finishPos.y] = (int)LabyrinthObject.Type.LABYRINTH_END;   //set the starting position as LABYRINTH_END
        _labyrinthGraph = new LabyrinthGraph(_startPos, _tiles);                        //creating a labyrinth graph

        //_labyrinthGraph.SpawnObject(_minumumDistanceBetweenEnemies, LabyrinthObject.Type.ENEMY);    //spawn enemies
        //_labyrinthGraph.SpawnObject(_minumumDistanceBetweenKeys, LabyrinthObject.Type.KEYS);        //spawn keys

        LoadTileMap();

        CreateFinishCheck();
    }

    private void DigTunnel(Vector2Int currPos, int counter)                             //recursive method of labyrinth generation
    {
        int[] randomDirections = { 1, 2, 3, 4 };
        randomDirections = randomDirections.OrderBy(x => _rnd.Next()).ToArray();        //placing directions in random order
        for (int i = 0; i < randomDirections.Length; ++i)
        {
            switch (randomDirections[i])
            {
                case 1: //down
                    if (currPos.y - 2 < 0)                                              //checking if we do not go beyond the labyrinth
                    {
                        continue;
                    }
                    else if (_tiles[currPos.x, currPos.y - 2] == (int)LabyrinthObject.Type.WALL)
                    {
                        _tiles[currPos.x, currPos.y - 1] = (int)LabyrinthObject.Type.PATH;
                        _tiles[currPos.x, currPos.y - 2] = (int)LabyrinthObject.Type.PATH;
                        DigTunnel(new Vector2Int(currPos.x, currPos.y - 2), counter + 1);
                    }
                    break;
                case 2: //up
                    if (currPos.y + 2 >= _tiles.GetLength(1))   //checking if we do not go beyond the labyrinth
                    {
                        continue;
                    }
                    else if (_tiles[currPos.x, currPos.y + 2] == (int)LabyrinthObject.Type.WALL)
                    {
                        _tiles[currPos.x, currPos.y + 1] = (int)LabyrinthObject.Type.PATH;
                        _tiles[currPos.x, currPos.y + 2] = (int)LabyrinthObject.Type.PATH;
                        DigTunnel(new Vector2Int(currPos.x, currPos.y + 2), counter + 1);
                    }
                    break;
                case 3: //left
                    if (currPos.x - 2 < 0)   //checking if we do not go beyond the labyrinth
                    {
                        continue;
                    }
                    else if (_tiles[currPos.x - 2, currPos.y] == (int)LabyrinthObject.Type.WALL)
                    {
                        _tiles[currPos.x - 1, currPos.y] = (int)LabyrinthObject.Type.PATH;
                        _tiles[currPos.x - 2, currPos.y] = (int)LabyrinthObject.Type.PATH;
                        DigTunnel(new Vector2Int(currPos.x - 2, currPos.y), counter + 1);
                    }
                    break;
                case 4: //right
                    if (currPos.x + 2 >= _tiles.GetLength(0))   //checking if we do not go beyond the labyrinth
                    {
                        continue;
                    }
                    else if (_tiles[currPos.x + 2, currPos.y] == (int)LabyrinthObject.Type.WALL)
                    {
                        _tiles[currPos.x + 1, currPos.y] = (int)LabyrinthObject.Type.PATH;
                        _tiles[currPos.x + 2, currPos.y] = (int)LabyrinthObject.Type.PATH;
                        DigTunnel(new Vector2Int(currPos.x + 2, currPos.y), counter + 1);
                    }
                    break;
            }
        }
        if (counter > _maxLength)
        {
            _maxLength = counter;
            _finishPos = currPos;
        }
    }

    private void MakeLoops()   //method for making loops in a labyrinth
    {
        for (int i = 0; i < Math.Max(1, (int)(_size.x * 0.1f)); ++i)    //Math.Max(1, (int)(_size.x * 0.1f) is the formula for the number of loops in labyrinth
        {
            Vector2Int randomVector;
            while (true)
            {
                randomVector = new Vector2Int(_rnd.Next(1, _size.x - 1), _rnd.Next(1, _size.y - 1));   //set a random position
                if (_tiles[randomVector.x, randomVector.y] == (int)LabyrinthObject.Type.WALL //checking the conditions if we can insert PATH here
                    && (_tiles[randomVector.x + 1, randomVector.y] == (int)LabyrinthObject.Type.WALL && _tiles[randomVector.x - 1, randomVector.y] == (int)LabyrinthObject.Type.WALL
                    && _tiles[randomVector.x, randomVector.y + 1] != (int)LabyrinthObject.Type.WALL && _tiles[randomVector.x, randomVector.y - 1] != (int)LabyrinthObject.Type.WALL
                    || _tiles[randomVector.x, randomVector.y + 1] == (int)LabyrinthObject.Type.WALL && _tiles[randomVector.x, randomVector.y - 1] == (int)LabyrinthObject.Type.WALL
                    && _tiles[randomVector.x + 1, randomVector.y] != (int)LabyrinthObject.Type.WALL && _tiles[randomVector.x - 1, randomVector.y] != (int)LabyrinthObject.Type.WALL))
                {
                    break;
                }
            }
            _tiles[randomVector.x, randomVector.y] = (int)LabyrinthObject.Type.PATH;
        }
    }

    public void LoadTileMap()   //method to load graphics into a tilemap
    {
        _labyrinthPrefab = Resources.Load("Labyrinth") as GameObject;    //load the labyrinth prefab
        _labyrinthPrefab = UnityEngine.Object.Instantiate(_labyrinthPrefab);
        _labyrinthPrefab.name = "Labyrinth";
        

        Vector3Int[] positions = new Vector3Int[_size.x * _size.y];    //array of positions
        Tile[] tileArray = new Tile[positions.Length];         //array of tile base

        //load tile base from resources
        Tile start = Resources.Load("TeleportStartON") as Tile;
        Tile end = Resources.Load("TeleportFinishOFF") as Tile;
        Tile floor = Resources.Load("Floor") as Tile;
        //total wall has 6 tiles to be set randomly
        Tile []wallTotal = {
            Resources.Load("WallTotal") as Tile,
            Resources.Load("WallTotal2") as Tile,
            Resources.Load("WallTotal3") as Tile,
            Resources.Load("WallTotal4") as Tile,
            Resources.Load("WallTotal5") as Tile,
            Resources.Load("WallTotal6") as Tile
        };
        //wall above path has 4 tiles to be set randomly
        Tile[] wallAbovePath = {
            Resources.Load("WallAbovePath") as Tile,
            Resources.Load("WallAbovePath2") as Tile,
            Resources.Load("WallAbovePath3") as Tile,
            Resources.Load("WallAbovePath4") as Tile
        };
        
        System.Random rand = new System.Random();

        for (int i = 0; i < positions.Length; i++)   //going through all the positions
        {
            positions[i] = new Vector3Int(i % _size.x, _size.x - (1 + i / _size.x), 0);
            switch (_tiles[i % _size.x, _size.x - (1 + i / _size.x)])
            {
                case (int)LabyrinthObject.Type.WALL:
                    {
                        if (_size.x - (1 + i / _size.x) - 1 >= 0 && _tiles[i % _size.x, _size.x - (1 + i / _size.x)-1] != (int)LabyrinthObject.Type.WALL)  //checking which TileBase to write to the tileArray array
                        {
                            tileArray[i] = wallAbovePath[rand.Next(0, wallAbovePath.Length)];
                        }
                        else
                        {
                            tileArray[i] = wallTotal[rand.Next(0, wallTotal.Length)];
                        }
                    }
                    break;
                case (int)LabyrinthObject.Type.PATH:
                    {
                        tileArray[i] = floor;
                    }
                    break;
                case (int)LabyrinthObject.Type.LABYRINTH_START:
                    {
                        tileArray[i] = start;
                    }
                    break;
                case (int)LabyrinthObject.Type.LABYRINTH_END:
                    {
                        tileArray[i] = end;
                    }
                    break;
                default:
                    {
                        ;
                    }
                    break;
            }
        }
        
        _labyrinthPrefab.transform.GetChild(0).gameObject.GetComponent<Tilemap>().SetTiles(positions, tileArray);  //sett the appropriate tileBase in the appropriate positions for the tilemap
        //_labyrinthPrefab.GetComponent<NetworkObject>().Spawn(true);
    }

    private void CreateFinishCheck() //creating an object that will check if the player has picked up all the keys after reaching the finish
    {
        GameObject finishCheckyPrefab = Resources.Load("FinishCheck") as GameObject;
        GameObject newObject = GameObject.Instantiate<GameObject>(finishCheckyPrefab);
        newObject.transform.position = new Vector3(_finishPos.x + 1, _finishPos.y + 1, 0);
    }

    public Vector2Int GetStartPos()
    {
        return _startPos;
    }


    public LabyrinthGraph GetLabyrinthGraph()
    {
        return _labyrinthGraph;
    }

    public void SwapTeleportStartTile()
    {
        TileBase startOn = Resources.Load("TeleportStartON") as TileBase;
        TileBase startOff = Resources.Load("TeleportStartOFF") as TileBase;
        _labyrinthPrefab.transform.GetChild(0).gameObject.GetComponent<Tilemap>().SwapTile(startOn, startOff);
    }

    public void SwapTeleportEndTile()
    {
        TileBase endOff = Resources.Load("TeleportFinishOFF") as TileBase;
        TileBase endOn = Resources.Load("TeleportFinishON") as TileBase;
        _labyrinthPrefab.transform.GetChild(0).gameObject.GetComponent<Tilemap>().SwapTile(endOff, endOn);
    }

}
