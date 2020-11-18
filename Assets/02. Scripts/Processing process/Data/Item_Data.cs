using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Data : MonoBehaviour
{
    public Item_Name_List Item_Name;
    public Item_Meterial_List[] item_Meterial_Lists;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Button);
    }

    void Button()
    {
        Sc.process_Menu_Manager.Pick_Item_Button();
    }
}

