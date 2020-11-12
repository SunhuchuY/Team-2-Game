using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class C_Manager : MonoBehaviour
{
    const int maxC = 5;
    const float speedC = 0.7f;

    public Transform C_Contants;

    // 손님 관리 시스템
    int C = -1;
    Queue<GameObject> C_queue = new Queue<GameObject>();
    [SerializeField] Transform[] C_endPosition = new Transform[maxC]; // 최대로 올수 있는 손님의 수가 5명임
    [SerializeField] Transform[] C_startPosition = new Transform[maxC]; // 최대로 올수 있는 손님의 수가 5명임

    // 주문형 손님
    public GameObject C_order_Manager_Ob;
    [SerializeField] GameObject C_Order_prefab;


    private void Start()
    {
        
    }

    // 손님 관리 시스템
    IEnumerator C_Join() // 손님이 들어올때의 연출과 자리 배치
    {
        GameObject temp = Instantiate(C_Order_prefab, C_Contants);
        C_queue.Enqueue(temp);
        int index = C_queue.Count - 1;

        temp.transform.position = C_startPosition[index].position;
        yield return temp.transform.DOMove(C_endPosition[index].position, speedC).WaitForCompletion();

        temp.GetComponent<C_Timer>().StartTimer();
    }

    public void C_Quit()
    {
        Destroy(C_queue.Dequeue());
        //you.transform.DOMove(C_startPosition[C].position, speedC);
    }




    // 주문형 손님 : 리얼타임매니저에서 오전 11시가 될시 이 함수를 호출한다.
    public void C_arriveOrder() // 오전 11시가 됬을때 실행 될 함수
    {
        StartCoroutine(C_Join());
    }

    public void OrderExit_Button() { C_order_Manager_Ob.SetActive(false); }

}

public class Map{
    public int ifMin, ifMax;
    public Item_Meterial_List[] Item;
    public Item_Name_List[] Meterial;
}
