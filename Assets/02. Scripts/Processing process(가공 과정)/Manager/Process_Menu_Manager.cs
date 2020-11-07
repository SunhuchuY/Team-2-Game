using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Process_Menu_Manager : MonoBehaviour
{

    public GameObject Pick_Manager_Ob, Pick_Image;
    public GameObject Item_Meterial_Manager;

    public ItemToMerterialList ItemToMerterialList;

    GameObject Temp_Ob;
    public Item_Data Item_Data;
    public Rythem_Data rythem_Data;
    
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
        Item_Meterial_Manager.SetActive(true);
    }

    public void Meterial_Make_Button()
    {
        rythem_Data = EventSystem.current.currentSelectedGameObject.GetComponent<Rythem_Data>();
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.process_Menu_Manager.Item_Meterial_Manager.SetActive(false);
        Sc.Process_Game.SetActive(true);
    }

}
