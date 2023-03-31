using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private GameObject _enemyObject;

    public Enemy(Vector2Int position)
    {
        GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;
        _enemyObject = GameObject.Instantiate<GameObject>(enemyPrefab);
        _enemyObject.transform.position = new Vector3(position.x+1, position.y+1, 0f);
    }
}
