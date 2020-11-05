using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Process_Menu_Manager : MonoBehaviour
{

    public GameObject Pick_Manager_Ob, Pick_Image;

    GameObject Temp_Ob;
    public Rythem_Data rythem_Data;
    
    public void Pick_Item_Button()
    {
        Temp_Ob = EventSystem.current.currentSelectedGameObject;
        Pick_Image.transform.SetParent(Temp_Ob.transform); Pick_Image.transform.localPosition = Vector2.zero;
        rythem_Data = Temp_Ob.GetComponent<Rythem_Data>();
        Pick_Manager_Ob.SetActive(true);
    }

    public void ItemMake_Button()
    {
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.Process_Menu.SetActive(false);
        Sc.Process_Game.SetActive(true);
    }

}
