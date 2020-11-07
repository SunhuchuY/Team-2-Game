using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToMerterialList : MonoBehaviour
{
    // 개발 된 아이템 순서로 집어넣는다
    public GameObject[] Meterial_List;
    // 0번 에메랄드 , 1번 철괴
    

    public void Item_Meterial_()
    {
        for (int i = 0; i < Meterial_List.Length; i++)
        {
            Meterial_List[i].SetActive(false);
        }

        for (int i = 0; i < Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists.Length; i++)
        {
            switch (Sc.process_Menu_Manager.Item_Data.item_Meterial_Lists[i])
            {
                case Item_Meterial_List.Emerald:
                    Meterial_List[0].SetActive(true);
                    break;
                case Item_Meterial_List.Iron:
                    Meterial_List[1].SetActive(true);
                    break;
            }
        }
    }
}
