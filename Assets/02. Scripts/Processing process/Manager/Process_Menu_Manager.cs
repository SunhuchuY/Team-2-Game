using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Process_Menu_Manager : MonoBehaviour
{

    public GameObject Pick_Manager_Ob, Pick_Image;
    public GameObject Item_Meterial_Manager, Item_Contants_Parent, Item_Contant_Prefab;

    public ItemToMerterialList ItemToMerterialList;

    GameObject Temp_Ob;
    public Item_Data Item_Data;
    public Rythem_Data rythem_Data;

    private void Start()
    {
        for (int i = 0; i < Enums.Item_Len; i++)
        {
            Item_Data tempScript = Instantiate(Item_Contant_Prefab, Item_Contants_Parent.transform).GetComponent<Item_Data>();
            tempScript.Item_Name = Sc.enums.itemDatas[i].itemName;
            tempScript.transform.GetChild(0).GetComponent<Text>().text = $"{Sc.enums.Item_Korean_name_string[i]}";
            tempScript.item_Meterial_Lists = new Item_Meterial_List[Sc.enums.itemDatas[i].Item_meterialList.Length];

            for (int j = 0; j < Sc.enums.itemDatas[i].Item_meterialList.Length; j++)
            {
                tempScript.item_Meterial_Lists[j] = Sc.enums.itemDatas[i].Item_meterialList[j];
            }
        }
    }

    public void Pick_Item_Button()
    {
        Temp_Ob = EventSystem.current.currentSelectedGameObject;
        Pick_Image.transform.SetParent(Temp_Ob.transform); Pick_Image.transform.localPosition = Vector2.zero;
        Item_Data = Temp_Ob.GetComponent<Item_Data>();
        Pick_Manager_Ob.SetActive(true);
        ItemToMerterialList.Item_Meterial_();
    }

    public void Item_Make_Button()
    {
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.Process_Menu.SetActive(false);
        Item_Meterial_Manager.GetComponent<ItemToMeterialStartGame>().Setting();
        Item_Meterial_Manager.SetActive(true);
    }

    public void Meterial_Make_Button()
    {
        rythem_Data = EventSystem.current.currentSelectedGameObject.GetComponent<Rythem_Data>();
        Sc.fadeInFadeOut.FadeFuntion();

        Item_Meterial_Manager.SetActive(false);
        Item_Meterial_Manager.GetComponent<ItemToMeterialStartGame>().Combination_Check();

        Sc.Process_Game.SetActive(true);
    }

    public void Back_Screen_Button()
    {
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.Process_Menu.SetActive(false);
        Sc.Operation_Menu.SetActive(true);
    }

}
