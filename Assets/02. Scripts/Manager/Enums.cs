
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
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
    Emerald,
    Iron,
    Ruby,
    Diamond,
    Saphire
};

public enum Item_Name_List
{
    Emerald_ring ,
    Diamond_ring,
    Ruby_ring,
    Saphire_ring
};
