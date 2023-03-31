using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabase
{

    [Header("Movement")]
    public static float movementSpeed = 2f;

    [Header("Attributes")]
    public static int strength = 1;
    public static int agility = 1;
    public static int intelect = 1;
    public static int stamina = 1;

    [Header("Statistics")]
    public static int maxHp = 100;
    public static int currentHp = 100;
    public static int minDamage = 10;
    public static int maxDamage = 15;

    [Header("LevelParameters")]
    public static int lvl = 1;
    public static int attributesPoints = 1;
    public static int expToNextLvl = 300;
    public static int currentExp = 0;

    public static int Damage()
    {
        return Random.Range(minDamage, maxDamage);
    }

    public static void LevelUp()
    {
        lvl++;
        currentExp -= expToNextLvl;
        expToNextLvl += 100;
        attributesPoints++;
    }

    public static void ResetStats()
    {
        movementSpeed = 2f;
        strength = 1;
        agility = 1;
        intelect = 1;
        stamina = 1;
        maxHp = 100;
        currentHp = 100;
        minDamage = 10;
        maxDamage = 15;
        lvl = 1;
        attributesPoints = 1;
        expToNextLvl = 300;
        currentExp = 0;
    }
}
