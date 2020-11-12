using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_OnMouse : MonoBehaviour
{
    public int itemIndex;

    public void On()
    {
        Sc.stock_Manager.ItemNameCall_Funtion(gameObject);
    }

    public void Exit()
    {
        Sc.stock_Manager.ItemNameExit_Funtion();
    }
}
