using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickBox_Manager : MonoBehaviour
{
    public int goldSum;
    int tempSize; // 픽 된 아이템의 선택 수량, 취소 버튼의 구현을 위한 임시 저장 데이터

    public Text useGold_Text, size_Text, Name_text, goldSum_Text; // 픽 된 아이템의 가격, 수량, 이름, 총 금액
    public GameObject goldSum_Ob;

    C_Oreder_Prefab_D pickScript;

    public void PickBoxOn_Setting(GameObject pick)
    {
        pickScript = pick.GetComponent<C_Oreder_Prefab_D>();

        Name_text.text = pickScript.isItem == true ?
            Sc.enums.Item_Korean_name_string[pickScript.Korean_stringIndex] : Sc.enums.Meterial_Korean_name_string[pickScript.Korean_stringIndex];
        tempSize = pickScript.deliveryNum;
        Text_Update();

        gameObject.SetActive(true);
    }

    public void Order_Submit_Button() // 주문에 대해서 제출
    {
        Main_Data.Gold -= goldSum;

        Sc.c_Manager.C_order_Manager_Ob.SetActive(false);

        Main_Data.Now_C_Quit();
    }

    public void PickData_Submit_Button() // 현재 선택한 아이템 및 재료에 대해서 제출
    {
        if (pickScript.deliveryNum > 0) // 최소 1개 이상 선택 한 경우
            pickScript.GetComponent<Image>().color = Color.blue;
        else
            pickScript.GetComponent<Image>().color = Color.white;

        Gold_Sum_Update();
    }

    public void Cancel_Button() // 초기화 버튼
    {
        if (pickScript.deliveryNum > 0) // 최소 1개 이상 선택 한 경우
            pickScript.GetComponent<Image>().color = Color.blue;
        else
            pickScript.GetComponent<Image>().color = Color.white;

        pickScript.deliveryNum = tempSize;
        Gold_Sum_Update();
    }

    public void SizeUpButton()
    {
        if (pickScript.deliveryNum >= pickScript.maxRand) // 선택 된 값이 최댓 사이즈보다 크다면
            return;

        pickScript.deliveryNum++;
        Text_Update();
    }

    public void SizeDownButton()
    {
        if (pickScript.deliveryNum <= 0) // 선택 된 값이 최댓 사이즈보다 크다면
            return;

        pickScript.deliveryNum--;
        Text_Update();
    }

    void Text_Update() 
    {
        useGold_Text.text = $"{pickScript.rateSale_Price * pickScript.deliveryNum}G";
        size_Text.text = $"{pickScript.deliveryNum}/{pickScript.maxRand}";
    }

    void Gold_Sum_Update()
    {
        pickScript.itemText_Update();
        gameObject.SetActive(false);

        goldSum_Ob.SetActive(true);

        goldSum = 0;
        for (int i = 0; i < Sc.c_Manager.C_Order_itemNumOfData + Sc.c_Manager.C_Order_meterialNumOfData; i++)
        {
            if (Sc.c_Manager.C_Order_Contants.transform.GetChild(i).gameObject.activeSelf == true)
            {
                goldSum += Sc.c_Manager.C_Order_Contants.transform.GetChild(i).GetComponent<C_Oreder_Prefab_D>().Sum;
            }
        }

        goldSum_Ob.GetComponent<Image>().color = Main_Data.Gold > goldSum ? Color.green : Color.red;

        goldSum_Text.text = $"{goldSum}";
    }
}
