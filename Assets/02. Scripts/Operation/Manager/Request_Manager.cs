using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request_Manager : MonoBehaviour
{
    public int numOfData = 0;
    public Request_List[] Request_List = new Request_List[3]; // 총 3개까지 가능하니까 3개 선언

    public int isEmpty_ToIndex()
    {
        for (int i = 0; i < Request_List.Length; i++)
        {
            if (Request_List[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    public void ToIndex_Request_Append(int index) // 의뢰창에 저장
    {
        //Request_List[index] = 
    }


}