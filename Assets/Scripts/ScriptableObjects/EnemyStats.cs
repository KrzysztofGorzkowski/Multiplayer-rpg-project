using UnityEngine;

//Enemy Database
[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemies")]
public class EnemyStats : ScriptableObject
{
    [Header("Statistics")]
    public int hp = 100;
    public int damage = 15; 
    public float speedAttack = 1F;
    public float movementSpeed = 0.5f;
    public float viewDistance = 4f;

    public int givenExp = 100;
}