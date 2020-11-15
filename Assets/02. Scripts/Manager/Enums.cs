using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public string[] Item_name_string = { "Emerald_ring" , "Diamond_ring", "Ruby_ring", "Saphire_ring" };
    public string[] Item_Korean_name_string = { "에메랄드 반지", "다이아 반지", "루비 반지", "사파이어 반지" };
    public string[] Meterial_name_string = { "Emerald", "Iron", "Ruby", "Diamond", "Saphire" };
    public string[] Meterial_Korean_name_string = { "에메랄드", "철", "루비", "다이아", "사파이어" };

    static public int Item_Len = Enum.GetNames(typeof(Item_Name_List)).Length;
    static public int Meterial_Len = Enum.GetNames(typeof(Item_Meterial_List)).Length;

    // 아이템 enum에 명시 된 순서대로 저장한다(위의 스트링값도 아이템 enum순서이니까 참조가능)
    public Meterial_countSort[] Item_countSort = new Meterial_countSort[Item_Len];  // 현재 만든 아이템 보관용도
    public int[] Item_Price = new int[Item_Len];  // 현재 만든 아이템 보관용도
    public Sprite[] Item_sprite = new Sprite[Item_Len];
    public string[] Item_explanString = new string[Item_Len];

    /// <summary>
    /// ///////////////////////
    /// </summary>

    public int[] Meterial_countSort = new int[Meterial_Len];  // 현재 만든 아이템 보관용도
    public int[] Meterial_Price = new int[Meterial_Len];  // 현재 만든 아이템 보관용도
    public Sprite[] Meterial_sprite = new Sprite[Meterial_Len];
    public string[] Meterial_explanString = new string[Meterial_Len];

    public string RankToStr_System(double Calculation)
    {
        if (Calculation == 1)
            return "S";
        else if (Calculation >= 0.95 && Calculation <= 0.99)
            return "A";
        else if (Calculation >= 0.8 && Calculation <= 0.94)
            return "B";
        else if (Calculation >= 0.5 && Calculation <= 0.79)
            return "C";
        else if (Calculation >= 0.3 && Calculation <= 0.49)
            return "D";

        return "F"; // 다 안나올경우 최악 F 판정
    }

    public string RankToInt_System(double Calculation)
    {
        if (Calculation == 1)
            return "S";
        else if (Calculation >= 0.95 && Calculation <= 0.99)
            return "A";
        else if (Calculation >= 0.8 && Calculation <= 0.94)
            return "B";
        else if (Calculation >= 0.5 && Calculation <= 0.79)
            return "C";
        else if (Calculation >= 0.3 && Calculation <= 0.49)
            return "D";

        return "F"; // 다 안나올경우 최악 F 판정
    }

    public void Item_countSort_Plus(int index, string ToRank, int Plus = 1)
    {
        int len = Item_countSort[index].Rank.Length;

        Item_countSort[index].Rank = new string[len + Plus];

        for (int i = 0; i < Plus; i++)
        {
            Item_countSort[index].Rank[len] = ToRank;
            len++;
        }
    }
}

[System.Serializable]
public class Meterial_countSort
{
    public string[] Rank;
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

public enum RightAndLeft{
    left,
    right
};

