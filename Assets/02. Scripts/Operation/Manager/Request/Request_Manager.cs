using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request_Manager : MonoBehaviour
{
    public GameObject Request_Information_Ob;
    public Image Box_C_Image;
    public Text left_Time, Rataining_Money,Request_Money, ItemName;

    public GameObject C_Request_collection_Prefab, C_Request_collection_Parent;

    public Request_Data[] Request_Datas = new Request_Data[3]; // 총 3개까지 가능하니까 3개 선언
    public Box_Data[] box_Datas = new Box_Data[3];

    public void Request_Manager_On_Button()
    {
        gameObject.SetActive(true);
        Request_Information_Ob.SetActive(false);

        for (int i = 0; i < Request_Datas.Length; i++)
        {
            if (Request_Datas[i].isEmpty != 0)
            {
                box_Datas[i].Parent_Ob.SetActive(true);
                box_Datas[i].Box_C_Image.sprite = Request_Datas[i].C_image;
                box_Datas[i].ItemName.text = $"{Sc.enums.Item_Korean_name_string[Request_Datas[i].itemIndex]}({Request_Datas[i].rank}급 이상)";
                box_Datas[i].left_Time.text = $"{Request_Datas[i].day}일 {Request_Datas[i].h}시 {Request_Datas[i].m}분 남음";
                box_Datas[i].Rataining_Money.text = $"사례금({Request_Datas[i].keyMoney}G)";
            }
            else
            {
                box_Datas[i].Parent_Ob.SetActive(false);
            }
        }
    }

    public void Request_Manager_Exit_Button()
    {
        gameObject.SetActive(false);
    }

    public void Index_To_Information(int i)
    {
        Request_Information_Ob.SetActive(true);

        Box_C_Image.sprite = Sc.enums.Item_sprite[Request_Datas[i].itemIndex];
        ItemName.text = box_Datas[i].left_Time.text;
        left_Time.text = box_Datas[i].ItemName.text;
        Rataining_Money.text = box_Datas[i].Rataining_Money.text;
        Request_Money.text = $"의뢰금({Request_Datas[i].requestMoney}G)";
    }

    public int isEmpty_ToIndex()
    {
        for (int i = 0; i < Request_Datas.Length; i++)
        {
            if (Request_Datas[i].isEmpty == 0)
            {
                return i;
            }
        }

        return -1;
    }

    public int isSize()
    {
        int size = 0;

        for (int i = 0; i < Request_Datas.Length; i++)
        {
            if (Request_Datas[i].isEmpty == 1)
            {
                size++;
            }
        }

        return size;
    }

    public void ToIndex_Request_Append(Request_List tempScript) // 의뢰창에 저장
    {
        int index = isEmpty_ToIndex();
        Request_Datas[index].isEmpty = 1;
        Request_Datas[index].day = tempScript.day;
        Request_Datas[index].h = tempScript.h;
        Request_Datas[index].m = tempScript.m;

        Request_Datas[index].rank = tempScript.rank;

        Request_Datas[index].rand_Request_Magnification = tempScript.rand_Request_Magnification;

        Request_Datas[index].keyMoney = tempScript.keyMoney;
        Request_Datas[index].requestMoney = tempScript.requestMoney;
        Request_Datas[index].retainingMoney = tempScript.retainingMoney;

        Request_Datas[index].itemIndex = tempScript.itemIndex;

        Request_Datas[index].C_name = tempScript.C_name;
        Request_Datas[index].C_image = tempScript.C_image;
    }

    public void leftTime_Check() // 5초마다 한번씩 실행
    {
        for (int i = 0; i < Request_Datas.Length; i++)
        {
            if (Request_Datas[i].isEmpty == 1)
            {
                if (Request_Datas[i].day <= 0 && Request_Datas[i].h <= 0 && Request_Datas[i].m <= 0)
                {
                    GameObject tempOb = Instantiate(C_Request_collection_Prefab, C_Request_collection_Parent.transform);
                    tempOb.GetComponent<Request_Collection>().Request_Collection_Index = i;
                    tempOb.transform.position = new Vector2(3000, 0);
                    tempOb.name = $"Collection{i}";
                    Sc.c_Manager.C_Request_Collection_Join(tempOb);
                    Request_Datas[i].isEmpty = 2;

                    continue;
                }

                if (Request_Datas[i].m <= 0)
                {
                    Request_Datas[i].h--;
                    Request_Datas[i].m = 60;

                    if (Request_Datas[i].h <= 0 && Request_Datas[i].day > 0)
                    {
                        Request_Datas[i].day--;
                        Request_Datas[i].h = 23;
                    }
                }

                Request_Datas[i].m -= 10;
                box_Datas[i].left_Time.text = $"{Request_Datas[i].day}일 {Request_Datas[i].h}시 {Request_Datas[i].m}분 남음";
            }
            else if(Request_Datas[i].isEmpty == 2)
            {
                box_Datas[i].left_Time.text = $"현재 방문중입니다.";
            }
        }
    }
}

[System.Serializable]
public class Request_Data
{
    public int isEmpty; // 0일시 비었다는 뜻, 1일시 채워있는데 시간은 흘러간다는 뜻, 2일시 방문중이라는 뜻.
    public int day, h, m;
    public string rank, C_name;
    public int keyMoney, requestMoney,retainingMoney, itemIndex;
    public float rand_Request_Magnification; // 배율
    public Sprite C_image;
}

[System.Serializable]
public class Box_Data
{
    public GameObject Parent_Ob;
    public Image Box_C_Image;
    public Text left_Time, Rataining_Money, ItemName;

}