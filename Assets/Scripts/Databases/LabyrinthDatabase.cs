using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthDatabase
{
    public static int basicSize = 9;
    public static int labyrinthLvl = 1;

    public static int LabrynthSize()
    {
        int x;
        if (labyrinthLvl % 2 == 0)
            x = labyrinthLvl;
        else
            x = labyrinthLvl - 1;
        return basicSize + x;
    }
    public static void ResetStats()
    {
        basicSize = 9;
        labyrinthLvl = 1;
    }
}
