using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToMeterialStartGame : MonoBehaviour
{
    Color blue = new Color(0f, 0f, 1f, 0.75f);

    int check;

    public GameObject Combination_Button;
    public GameObject Process_Combination_Game_Ob;

    public GameObject Item_Contant;
    public GameObject Meterial_Contant;

    private void Start()
    {
        Combination_Button.GetComponent<Button>().onClick.AddListener(Combination_Button_Funtion);
    }

    public void Setting()
    {
        Combination_Button.SetActive(false);
        check = 0;

        for (int i = 0; i < Enums.Item_Len; i++) { Item_Contant.transform.GetChild(i).gameObject.SetActive(false); }
        for (int i = 0; i < Enums.Meterial_Len; i++) { Meterial_Contant.transform.GetChild(i).gameObject.SetActive(false); }

        switch (Sc.process_Menu_Manager.Item_Data.Item_Name)
        {
            case Item_Name_List.Emerald_ring:
                Item_find("Emerald_ring");
                break;
            case Item_Name_List.Diamond_ring:
                Item_find("Diamond_ring");
                break;
            case Item_Name_List.Ruby_ring:
                Item_find("Ruby_ring");
                break;
            case Item_Name_List.Saphire_ring:
                Item_find("Saphire_ring");
                break;
        }

        for (int i = 0; i < Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists.Length; i++)
        {
            switch (Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists[i])
            {
                case Item_Meterial_List.Emerald:
                    Meterial_find("Emerald");
                    break;
                case Item_Meterial_List.Iron:
                    Meterial_find("Iron");
                    break;
                case Item_Meterial_List.Ruby:
                    Meterial_find("Ruby");
                    break;
                case Item_Meterial_List.Diamond:
                    Meterial_find("Diamond");
                    break;
                case Item_Meterial_List.Saphire:
                    Meterial_find("Saphire");
                    break;
            }
        }
    }

    void Item_find(string Item_name)
    {
        for (int i = 0; i < Enums.Item_Len; i++)
        {
            if (Item_name == Item_Contant.transform.GetChild(i).gameObject.name)
            {
                Item_Contant.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    void Meterial_find(string Meterial_name)
    {
        for (int i = 0; i < Enums.Meterial_Len; i++)
        {
            if (Meterial_name == Meterial_Contant.transform.GetChild(i).gameObject.name)
            {
                Meterial_Contant.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void Combination_Check()
    {
        Sc.process_Menu_Manager.rythem_Data.gameObject.GetComponent<Image>().color = blue;

        if (Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists.Length <= ++check)
        {
            Combination_Button.SetActive(true);
        }
    }

    void Combination_Button_Funtion()
    {
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.process_Menu_Manager.Item_Meterial_Manager.SetActive(false);
        Process_Combination_Game_Ob.SetActive(true);
    }
}
