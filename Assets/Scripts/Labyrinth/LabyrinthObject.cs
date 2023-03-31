using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthObject
{
    public Vector2Int pos = new Vector2Int();     //position
    public Type type = new Type();                //type of object

    public LabyrinthObject(Type type)
    {
        this.pos = new Vector2Int(0, 0);
        this.type = type;
    }

    public enum Type
    {
        OTHER = -1,
        PATH,
        WALL,
        LABYRINTH_START,
        LABYRINTH_END,
        ENEMY,
        KEYS
    }
}
