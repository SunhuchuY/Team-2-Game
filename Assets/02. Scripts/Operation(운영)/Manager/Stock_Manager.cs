using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class Stock_Manager : MonoBehaviour
{
    const int maxC = 5;
    const float speedC = 0.7f;

    // Mouse On > Item Name Call
    [SerializeField] GameObject ItemName_Ob;
    [SerializeField] Text ItemName_Text;

    // Explan
    [SerializeField] GameObject Explan_Ob;
    [SerializeField] Text Explan_Text;
    [SerializeField] Text Explan_itemName_Text;

    // 마우스 올릴시 아이템 이름 띄우기
    public void ItemNameCall_Funtion(GameObject you)
    {
        int temp = you.GetComponent<Item_OnMouse>().itemIndex;

        ItemName_Text.text = Sc.enums.Item_Korean_name_string[temp];

        ItemName_Ob.transform.SetParent(you.transform.parent);
        ItemName_Ob.transform.localPosition = new Vector2(you.transform.parent.position.x < 800 ? 100 : -100, 0);
        ItemName_Ob.SetActive(true);
    }

    public void ItemNameExit_Funtion() { ItemName_Ob.SetActive(false); }



    // 아이템 설명 창 띄우기
    public void ExplanCall_Funtion(GameObject you)
    {
        int temp = you.GetComponent<Item_OnMouse>().itemIndex;

        Explan_itemName_Text.text = Sc.enums.Item_Korean_name_string[temp];
        Explan_Text.text = Sc.enums.Item_explanString[temp];

        // 800보다 작다면 오른쪽으로 띄우고 / 크다면 왼쪽으로
        Explan_Ob.transform.SetParent(you.transform.parent);
        Explan_Ob.transform.localPosition = new Vector2(you.transform.parent.position.x < 800 ? 100 : -100, 0);
        Explan_Ob.SetActive(true);
    }

    public void ExplanExit_Funtion() { Explan_Ob.SetActive(false); }


}
