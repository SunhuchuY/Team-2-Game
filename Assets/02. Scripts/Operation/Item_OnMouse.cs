using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_OnMouse : MonoBehaviour
{
    public int itemIndex;
    public int rankIndex;

    public void Explan_Button()
    {
        Sc.stock_Manager.ExplanCall_Funtion(gameObject);
    }

    public void On()
    {
        Sc.stock_Manager.ItemNameCall_Funtion(gameObject);
    }

    public void Exit()
    {
        Sc.stock_Manager.ItemNameExit_Funtion();
    }
}
