using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public Temp_itemData[] itemDatas = new Temp_itemData[Item_Len];

    /// <summary>
    /// ///////////////////////
    /// </summary>

    public int[] Meterial_countSort = new int[Meterial_Len];  // 현재 만든 아이템 보관용도
    public int[] Meterial_Price = new int[Meterial_Len];  // 현재 만든 아이템 보관용도
    public Sprite[] Meterial_sprite = new Sprite[Meterial_Len];
    public string[] Meterial_explanString = new string[Meterial_Len];

    public C_Data[] C_Datas;

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

    public int RankToInt_System(string Rank)
    {
        if (Rank == "S")
            return 6;
        else if (Rank == "A")
            return 5;
        else if (Rank == "B")
            return 4;
        else if (Rank == "C")
            return 3;
        else if (Rank == "D")
            return 2;

        return 1; // 다 안나올경우 최악 F 판정
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

    // 해당 아이템에 랭크가 있을때만 사용을 권함
    public void Item_countSort_Data_Remove(int itemIndex, string RemoveRank) // 해당 아이템의 특정 랭크를 가진 데이터를 삭제
    {
        // 조금 비효율적이지만 옆으로 한칸씩 이동하는 방식을 씀, 지워야 할 데이터 조건 탐색 후 한칸씩 옆으로 옮긴후 배열 길이를 줄임

        /*for (int i = 0; i < Item_countSort[itemIndex].Rank.Length; i++)
        {
            if (Item_countSort[itemIndex].Rank[i] == RemoveRank)
            {
                for (int j = i + 1; j < Item_countSort[itemIndex].Rank.Length; j++)
                {
                    string tempRank = Item_countSort[itemIndex].Rank[j];
                    Item_countSort[itemIndex].Rank[j - 1] = tempRank;
                    print($"{tempRank} {Item_countSort[itemIndex].Rank[j - 1]}");
                }
                break;
            }
        }*/


        // 데이터를 복사 후 옮김
        Stack<string> tempRank = new Stack<string>();

        for (int i = 0; i < Item_countSort[itemIndex].Rank.Length; i++)
        {
            if (Item_countSort[itemIndex].Rank[i] == RemoveRank)
            {
                for (int j = 0; j < Item_countSort[itemIndex].Rank.Length; j++)
                {
                    if (j != i)
                    {
                        tempRank.Push(Item_countSort[itemIndex].Rank[j]);
                    }
                }
                break;
            }
        }

        Item_countSort[itemIndex].Rank = new string[Item_countSort[itemIndex].Rank.Length - 1];

        for (int i = 0; i <= tempRank.Count; i++)
        {
            Item_countSort[itemIndex].Rank[i] = tempRank.Pop();
        }
    }

    // 랜덤 이름을 정해주는 함수.
    public C_Data Random_C_DataReturn()
    {
        return C_Datas[Random.Range(0, C_Datas.Length)];
    }

}

[System.Serializable]
public class Meterial_countSort
{
    public string[] Rank;
}

[System.Serializable]
public class Temp_itemData
{
    public int Meterial_sumPrice;
    public Item_Name_List itemName;
    public Item_Meterial_List[] Item_meterialList;
}

[System.Serializable]

public class C_Data
{
    public string C_name;
    public Sprite C_image;
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

public enum C_Type
{
    C_Order, // 주문형
    C_Request, // 의뢰형
    C_Request_collection // 의뢰 - 회수형
};

