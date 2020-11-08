using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToMerterialList : MonoBehaviour
{
    public GameObject Meterial_Content;

    public void Item_Meterial_()
    {
        for (int i = 0; i < Enums.Meterial_Len; i++)
        {
            Meterial_Content.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists.Length; i++)
        {
            switch (Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists[i])
            {
                case Item_Meterial_List.Emerald:
                    Check("Emerald");
                    break;
                case Item_Meterial_List.Iron:
                    Check("Iron");
                    break;
                case Item_Meterial_List.Diamond:
                    Check("Diamond");
                    break;
                case Item_Meterial_List.Ruby:
                    Check("Ruby");
                    break;
                case Item_Meterial_List.Saphire:
                    Check("Saphire");
                    break;
            }
        }
    }

    void Check(string meterialName)
    {
        for (int j = 0; j < Enums.Meterial_Len; j++)
        {
            if (Meterial_Content.transform.GetChild(j).name == meterialName) { Meterial_Content.transform.GetChild(j).gameObject.SetActive(true); }
        }
    }
}
