using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LabyrinthGraph
{
    /*private class Node
    { 
        public LabyrinthObject labyrinthObject;
        public Node[] adj;   //table of connected nodes
        public bool isAddedToGraph = false;  //tells if a node has been added to the graph
        public Node()
        {
            adj = new Node[4];
            for(int i = 0; i < 4; ++i)
            {
                adj[i] = null;
            }
        }
    }

    public enum Direction
    {
        UP = 0, DOWN, LEFT, RIGHT, NONE
    }



    private Node[,] _labyrinthObjectArray;     //two-dimensional array of nodes
    private Node _graphHead;
    private Vector2Int[] _directions;    // used for creation of the graph
    private List<Node> _pathNodes = new List<Node>();  //list of nodes that have labyrinthObject as PATH
    private List<Node> _tempPathNodes = new List<Node>();  //temporary list of nodes that have labyrinthObject as PATH
    public LabyrinthGraph(Vector2Int startPos, int[,] tiles)
    {
        _directions = new Vector2Int[4];
        _directions[0] = new Vector2Int(0, -1);
        _directions[1] = new Vector2Int(0, 1);
        _directions[2] = new Vector2Int(-1, 0);
        _directions[3] = new Vector2Int(1, 0);

        _labyrinthObjectArray = new Node[tiles.GetLength(0), tiles.GetLength(1)];

        for(int i=0; i< _labyrinthObjectArray.GetLength(0); ++i)
        {
            for(int j = 0; j < _labyrinthObjectArray.GetLength(1); ++j)
            {
                _labyrinthObjectArray[i, j] = new Node();
                _labyrinthObjectArray[i, j].labyrinthObject = new LabyrinthObject((LabyrinthObject.Type)tiles[i, j]);
                _labyrinthObjectArray[i, j].labyrinthObject.pos = new Vector2Int(i, j);
            }
        }
        _graphHead = _labyrinthObjectArray[startPos.x, startPos.y];
        AddNodesToGraph(_graphHead);
    }


    private void AddNodesToGraph(Node currNode)  //a recursive method for adding nodes to a graph
    {
        _pathNodes.Add(currNode);
        Vector2Int currPos = currNode.labyrinthObject.pos;      //current position
        Vector2Int nextPos;                                     //next position

        for(int i = 0; i < 4; ++i)
        {
            nextPos = currPos + _directions[i];     //getting the next node position
            if (_labyrinthObjectArray[nextPos.x, nextPos.y].labyrinthObject.type != LabyrinthObject.Type.WALL   //checking if we can connect a given node
                && _labyrinthObjectArray[nextPos.x, nextPos.y].labyrinthObject.type != LabyrinthObject.Type.OTHER
                && !_labyrinthObjectArray[nextPos.x, nextPos.y].isAddedToGraph)
            {
                Node newNode = _labyrinthObjectArray[nextPos.x, nextPos.y];
                ConnectNodeInDirection((Direction)i, currNode, newNode);   //connect the nodes
                currNode.isAddedToGraph = true;
                AddNodesToGraph(newNode);  //recursive method call
            }
        }
    }


    private void ConnectNodeInDirection(Direction direction, Node currNode, Node newNode)  //a method that connects the nodes in the right directions
    {
        switch (direction)
        {
            case Direction.UP:
                
                currNode.adj[(int)Direction.DOWN] = newNode;
                newNode.adj[(int)Direction.UP] = currNode;
                break;
            case Direction.DOWN:
                currNode.adj[(int)Direction.UP] = newNode;
                newNode.adj[(int)Direction.DOWN] = currNode;
                break;
            case Direction.LEFT:
                currNode.adj[(int)Direction.LEFT] = newNode;
                newNode.adj[(int)Direction.RIGHT] = currNode;
                break;
            case Direction.RIGHT:
                currNode.adj[(int)Direction.RIGHT] = newNode;
                newNode.adj[(int)Direction.LEFT] = currNode;
                break;
        }
    }

    public void SpawnObject(int minumumDistanceBetweenObjects, LabyrinthObject.Type type)  //method of spawning ENEMY or KEYS objects
    {
        for(int i = 0; i < _pathNodes.Count; i++)
        {
            _tempPathNodes.Add(_pathNodes[i]);   //filling in the temporary list of nodes. The list helps us determine if there are any possible positions in the graph for spawn objects
        }

        LabyrinthObject newLabyrinthObject = null;
        newLabyrinthObject = GetRandomFreeLabyrinthObject(minumumDistanceBetweenObjects, type);    //get a random free labyrinth object
        while (newLabyrinthObject != null)
        {
            GameObject newObject = null;
            if (type == LabyrinthObject.Type.ENEMY)    //for type ENEMY
            {
                GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;   //get enemy prefab
                newObject = GameObject.Instantiate<GameObject>(enemyPrefab);
                newObject.transform.position = new Vector3(newLabyrinthObject.pos.x + 1, newLabyrinthObject.pos.y + 1, -1f);
            }
            else if (type == LabyrinthObject.Type.KEYS)    //for type KEYS
            {
                GameObject keyPrefab = Resources.Load("Key") as GameObject;      //get keys prefab
                newObject = GameObject.Instantiate<GameObject>(keyPrefab);
                Labyrinth.numberOfKeys++;
                newObject.transform.position = new Vector3(newLabyrinthObject.pos.x + 1, newLabyrinthObject.pos.y + 1, 0f);
            }
            newLabyrinthObject = GetRandomFreeLabyrinthObject(minumumDistanceBetweenObjects, type);    //get a random free labyrinth object
        }
    }

    private LabyrinthObject GetRandomFreeLabyrinthObject(int minumumDistanceBetweenObjects, LabyrinthObject.Type type)   //a method of determining a random free position in a labyrinth
    {
        System.Random rnd = new System.Random();
        LabyrinthObject newLabyrinthObject = null;
        while (newLabyrinthObject == null && _tempPathNodes.Count != 0)    //looking for a free node in the labyrinth
        {
            Node tempNode = _tempPathNodes[rnd.Next(0, _tempPathNodes.Count)];   //get a random node from _tempPathNodes list
            newLabyrinthObject = tempNode.labyrinthObject;
            _tempPathNodes.Remove(tempNode);
            if (CheckSurroundingsClear(tempNode, minumumDistanceBetweenObjects, Direction.NONE, type))   //checking if the surroundings are clear
            {
                newLabyrinthObject.type = type;
            }
            else
            {
                newLabyrinthObject = null;
            }
        }
        return newLabyrinthObject;

    }

    private bool CheckSurroundingsClear(Node node, int distance, Direction cameFrom, LabyrinthObject.Type type) //recursive method that checks a surroundings clear
    {
        if (   node.labyrinthObject.type == type                                    //checking if the node we are currently on is not free
            || node.labyrinthObject.type == LabyrinthObject.Type.LABYRINTH_START 
            || node.labyrinthObject.type == LabyrinthObject.Type.LABYRINTH_END)
        {
            return false;
        }

        if (distance <= 0)   //checking the distance from the node where we want to place the object
        {
            return true;
        }

        bool surroundingsIsClear = true;

        for(int i = 0; i < 4; i++)
        {
            if (i == (int)cameFrom) //to avoid reversing
            {
                continue;
            }

            if (node.adj[i] != null)
            {
                surroundingsIsClear = CheckSurroundingsClear(node.adj[i], distance - 1, GetOppositeDirection((Direction)i), type);  //recursive method call
                if (!surroundingsIsClear)
                {
                    return false;
                }
            }
        }
        return surroundingsIsClear;
    }

    private Direction GetOppositeDirection(Direction direction)   //function that returns the opposite direction
    {
        switch (direction)
        {
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
        }
        return Direction.NONE;
    }*/

}
