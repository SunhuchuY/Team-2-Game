using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request_List : MonoBehaviour
{
    //public Text itemName_Text, leftDayTime_text, Money_Text; // 아이템 이름, 남은 시간, 사례금

    // 의뢰금은 해당 의뢰를 통해 얻는 총 비용
    // 착수금은 해당 의뢰를 수주할 때 미리 얻는 비용
    // 사례금은 의뢰를 완수하고 받는 착수금을 제외한 비용

    public int keyMoney, requestMoney,retainingMoney, itemIndex; // 사례금, 의뢰금, 착수금, 해당 아이템 인덱스
    public float rand_Request_Magnification;
    public string rank, C_name; // 랭크, 손님이름
    public Sprite C_image;

    public int day, h, m;

    public void OnRequest_Button() // 의뢰를 수락하기 위한 창이 뜨는 버튼
    {
        Sc.c_Manager.C_Request_Button(gameObject.GetComponent<Request_List>());
    }

    public void Start() // 데이터 생성(최악의 경우: 5명까지 올수있다.)
    {
        Request_List tempScript = gameObject.GetComponent<Request_List>(); // 해당 인덱스 참조

        tempScript.itemIndex = Random.Range(0, Enums.Item_Len); // 아이템 랜덤 인덱스
        int tempRand1 = Random.Range(1, 5), tempRand2 = Random.Range(1, 10); //  소수점 1번째자리 랜덤
        tempScript.rand_Request_Magnification = tempRand1 + (tempRand2 * 0.1f); // 의뢰 배율

        int sumPrice = 0, collectionPrice;
        for (int i = 0; i < Sc.enums.itemDatas[tempScript.itemIndex].Item_meterialList.Length; i++)
        {
            sumPrice += Sc.enums.Meterial_Price[(int)Sc.enums.itemDatas[tempScript.itemIndex].Item_meterialList[i]];
        }

        collectionPrice = (int)(sumPrice * tempScript.rand_Request_Magnification);

        tempScript.requestMoney = collectionPrice; // 의뢰금 : 해당 의뢰를 통해 얻는 총 비용
        tempScript.retainingMoney = tempScript.rand_Request_Magnification < 1.8 ? collectionPrice / 2 : 0; // 착수금 : 해당 의뢰를 수주할 때 미리 얻는 비용
        tempScript.keyMoney = tempScript.requestMoney - tempScript.retainingMoney; // 사례금(완수금) : 의뢰를 완수하고 받는 착수금을 제외한 비용

        C_Data temp_C_Data = Sc.enums.Random_C_DataReturn();
        tempScript.C_name = temp_C_Data.C_name;
        tempScript.C_image = temp_C_Data.C_image;

        tempScript.rank = Sc.c_Manager.rankRand_Funtion();

        tempScript.day = Random.Range(1, 6);
        tempScript.h = Sc.realTime_Manager.h + tempScript.day;
        tempScript.m = 0;

    }
}
