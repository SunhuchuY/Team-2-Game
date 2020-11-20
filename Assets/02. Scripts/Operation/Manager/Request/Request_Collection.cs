using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request_Collection : MonoBehaviour
{
    public int Request_Collection_Index;

    public void Request_Collection_Button() // 회수형 손님의 버튼
    {
        Sc.c_Manager.C_Request_Collection_Button(gameObject);
    }

    public void pickButton()
    {
        Sc.c_Manager.C_Request_Collection_pickButton(gameObject);
    }


    void Request_Collection_Exit() // 회수형 종료 함수
    {
        Sc.c_Manager.C_Request_Manager_Ob.SetActive(false);
        Sc.c_Manager.C_Request_Magnification_Ob.SetActive(false);
    }
}
