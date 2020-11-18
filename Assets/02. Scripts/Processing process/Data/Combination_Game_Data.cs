using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapArray
{
    public Item_Name_List item_name;
    public MapArray2[] Line;
}

[System.Serializable]
public class MapArray2
{
    public int LoopOfnum;
    public Item_Meterial_List[] Meterial_Combination;
}

public class Combination_Game_Data : MonoBehaviour
{
    public MapArray[] Combination_Data;
}
