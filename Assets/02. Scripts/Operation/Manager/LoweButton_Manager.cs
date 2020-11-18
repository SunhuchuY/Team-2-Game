using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoweButton_Manager : MonoBehaviour
{
    public GameObject StockManager_Ob;

    public void MakeButton()
    {
        Sc.fadeInFadeOut.FadeFuntion();
        Sc.Operation_Menu.SetActive(false);
        Sc.Process_Menu.SetActive(true);
    }

    public void StockButton() { StockManager_Ob.SetActive(true); }
    public void StockExitButton() { StockManager_Ob.SetActive(false); }

}
