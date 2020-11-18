using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Oreder_Prefab_D : MonoBehaviour
{
    public bool isItem;
    public int deliveryNum = 0, Korean_stringIndex, maxRand, rateSale_Price, Sum = 0;
    public Image itemImage;
    public Text itemText, useGold_Text;

    private void OnEnable()
    {
        itemText_Update();
        useGold_Text.text = $"{rateSale_Price}G";
    }

    public void PickBoxOn_Button()
    {
        Sc.pickBox_Manager.PickBoxOn_Setting(gameObject);
    }

    public void itemText_Update()
    {
        string koreanStr = isItem == true ?
            Sc.enums.Item_Korean_name_string[Korean_stringIndex] : Sc.enums.Meterial_Korean_name_string[Korean_stringIndex];

        Sum = rateSale_Price * deliveryNum;
        itemText.text = $"{koreanStr}({deliveryNum}/{maxRand})";
    }
}
