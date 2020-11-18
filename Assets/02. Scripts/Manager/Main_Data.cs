using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Data : MonoBehaviour
{

    public Text moneyText;

    static public GameObject Now_C;
    static public int Gold = 50000;
    static public float Shop_repu = 4;
    
    private void Update()
    {
        moneyText.text = $"{Gold}G";
    }

    static public void Now_C_Quit()
    {
        Destroy(Now_C);
    }

    static public void Now_C_Init(GameObject To = null) // 모든 매니저 오브젝트를 살펴 켜진 오브젝트를 현재 손님으로 취급
    {
        if(To == null)
        {
            Now_C = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            return;
        }

        Now_C = To.transform.parent.gameObject;
    }
}
