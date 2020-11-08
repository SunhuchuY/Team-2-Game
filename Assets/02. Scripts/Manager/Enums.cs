
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public string[] Item_name_string = { "Emerald_ring" , "Diamond_ring", "Ruby_ring", "Saphire_ring" };
    public string[] Meterial_name_string = { "Emerald", "Iron", "Ruby", "Diamond", "Saphire" };

    static public int Item_Len = Enum.GetNames(typeof(Item_Name_List)).Length;
    static public int Meterial_Len = Enum.GetNames(typeof(Item_Meterial_List)).Length;
}

enum Timing_State
{
    
};

public enum Sound_List
{
    temp_sound
};

public enum Note_Type
{
    Rest,
    Hit
};

public enum Item_Meterial_List
{
    Emerald = 0,
    Iron = 1,
    Ruby = 2,
    Diamond = 3,
    Saphire = 4
};

public enum Item_Name_List
{
    Emerald_ring = 0,
    Diamond_ring = 1,
    Ruby_ring = 2,
    Saphire_ring = 3
};

