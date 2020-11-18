using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class C_Order_Delivery_Manager : MonoBehaviour
{
    public GameObject Contant_Prefab;
    public GameObject Contants;

    int numOfData = 0;

    private void OnEnable()
    {
        for (int i = 0; i < numOfData; i++)
        {
            Destroy(Contants.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Sc.c_Manager.C_Order_itemNumOfData + Sc.c_Manager.C_Order_meterialNumOfData; i++)
        {
            GameObject ifOb = Sc.c_Manager.C_Order_Contants.transform.GetChild(i).gameObject;
            C_Oreder_Prefab_D Script = ifOb.GetComponent<C_Oreder_Prefab_D>();

            if (ifOb.activeSelf == true && Script.deliveryNum > 0)
            {
                for (int j = 0; j < Script.deliveryNum; j++)
                {
                    string koreanStr = Script.isItem == true ?
                    Sc.enums.Item_Korean_name_string[Script.Korean_stringIndex] 
                    : Sc.enums.Meterial_Korean_name_string[Script.Korean_stringIndex];

                    GameObject temp = Instantiate(Contant_Prefab, Contants.transform);

                    temp.transform.GetChild(0).GetComponent<Text>().text = $"{koreanStr} x {Script.deliveryNum}"; // 텍스트위치임

                    if (Script.isItem)
                        Sc.enums.Item_countSort_Plus(Script.Korean_stringIndex, "S", Script.deliveryNum);
                    else
                        Sc.enums.Meterial_countSort[Script.Korean_stringIndex] += Script.deliveryNum;

                    Script.deliveryNum = 0;
                    numOfData++;
                }
            }
        }
    }

    public void Delivery_OpenButton() // 택배를 눌렀을때
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector2(1944, 0);
        gameObject.transform.DOMove(Vector2.zero, 0.8f);
    }

    public void Delivery_receipt() // 수령하기 버튼
    {
        gameObject.SetActive(false);

        const float speed = 0.6f;

        Sc.c_Manager.C_Order_Delivery_Ob.GetComponent<Button>().interactable = false;
        Sc.c_Manager.C_Order_Delivery_Ob.transform.DOMove(new Vector2(Sc.c_Manager.C_Order_Delivery_Ob.transform.position.x, -1100), speed);
        Invoke("DeliveryOb_False", speed);
    }

    public void DeliveryOb_False()
    {
        Sc.c_Manager.C_Order_Delivery_Ob.SetActive(false);
    }
}
