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
    public int C_Order_itemNumOfData = 0, C_Order_meterialNumOfData = 0;
    public GameObject C_Order_Delivery_Ob, C_Order_Delivery_EndPosition; // 택배 오브젝트
    public GameObject C_order_Manager_Ob;
    public GameObject C_Order_prefab; // 주문형 손님 프리팹
    public GameObject C_Order_itemPrefab; // 모두 생성
    public GameObject C_Order_Contants; // 아이템 및 재료들 모두가 생성 되어 있는 부모 오브젝트
    public C_Order_Input[] C_Order_Input; // 커스텀 할 수 있는 클래스를 배열로 만든 것

    private void Start()
    {

        // 재료모두 생성
        for (int i = 0; i < Enums.Meterial_Len; i++)
        {
            GameObject temp = Instantiate(C_Order_itemPrefab, C_Order_Contants.transform);
            // 추후 조건검사에서 오브젝트 이름을 활용한 조건검사를 실행하므로...
            temp.name = Sc.enums.Meterial_name_string[i];
            C_Oreder_Prefab_D tempPrefab = temp.GetComponent<C_Oreder_Prefab_D>();

            tempPrefab.itemImage.sprite = Sc.enums.Meterial_sprite[i];
            // text는 추후에 다른 변동사항과 변경 될수있으므로 Start()에서 실행되지 않는다

            C_Order_meterialNumOfData++;
        }

        // 아이템 미리 모두 생성
        for (int i = 0; i < Enums.Item_Len; i++)
        {
            GameObject temp = Instantiate(C_Order_itemPrefab, C_Order_Contants.transform);
            // 추후 조건검사에서 오브젝트 이름을 활용한 조건검사를 실행하므로...
            temp.name = Sc.enums.Item_name_string[i];
            C_Oreder_Prefab_D tempPrefab = temp.GetComponent<C_Oreder_Prefab_D>();

            tempPrefab.itemImage.sprite = Sc.enums.Item_sprite[i];
            // text는 추후에 다른 변동사항과 변경 될수있으므로 Start()에서 실행되지 않는다

            C_Order_itemNumOfData++;
        }
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
    int pickIndex;

    void C_Order_inputIndex_Find()
    {
        for (int i = 0; i < C_Order_Input.Length; i++)
        {
            if (Main_Data.Shop_repu >= C_Order_Input[i].ifMin && Main_Data.Shop_repu <= C_Order_Input[i].ifMax)
            {
                pickIndex = i;
                break;
            }
        }
    }

    public void C_arriveOrder() // 오전 11시가 됬을때 실행 될 함수
    {
        // 아이템 오브젝트 모두 끄는(초기화) 함수
        for (int k = 0; k < C_Order_itemNumOfData + C_Order_meterialNumOfData; k++) { C_Order_Contants.transform.GetChild(k).gameObject.SetActive(false); }

        // 재료가 위, 아이템이 아래로 정렬 > 따라서 재료부터 먼저 활성화
        // 인풋에 있는 모두를 해당 가게평판에 맞는지 조건 체크 후 그 조건에 만족하는 구간에 있는 해당 아이템,재료만 오브젝트를 활성화 시킨다.
        C_Order_inputIndex_Find();

        for (int j = 0; j < C_Order_Input[pickIndex].Meterial.Length; j++)
        {
            string check = Sc.enums.Meterial_name_string[(int)C_Order_Input[pickIndex].Meterial[j].Meterial];

            for (int k = 0; k < C_Order_meterialNumOfData; k++)
            {
                if (check == C_Order_Contants.transform.GetChild(k).name)
                {
                    int index = k;
                    C_Oreder_Prefab_D temp = C_Order_Contants.transform.GetChild(index).GetComponent<C_Oreder_Prefab_D>();

                    temp.isItem = false;
                    temp.Korean_stringIndex = k;
                    temp.itemImage.color = Color.white;
                    temp.maxRand = Random.Range(1, C_Order_Input[pickIndex].Meterial[j].maxRand == 0 ? 5 : C_Order_Input[pickIndex].Meterial[j].maxRand);
                    temp.rateSale_Price = Sc.enums.Meterial_Price[k] - (int)((double)Sc.enums.Meterial_Price[k] * C_Order_Input[pickIndex].rateSale);

                    C_Order_Contants.transform.GetChild(index).gameObject.SetActive(true);
                }
            }
        }


        for (int j = 0; j < C_Order_Input[pickIndex].Item.Length; j++)
        {
            string check = Sc.enums.Item_name_string[(int)C_Order_Input[pickIndex].Item[j].Item];

            for (int k = 0; k < C_Order_itemNumOfData; k++)
            {
                if (check == C_Order_Contants.transform.GetChild(k + C_Order_itemNumOfData).name)
                {
                    int index = k + C_Order_meterialNumOfData;
                    C_Oreder_Prefab_D temp = C_Order_Contants.transform.GetChild(index).GetComponent<C_Oreder_Prefab_D>();

                    temp.isItem = true;
                    temp.Korean_stringIndex = k;
                    temp.itemImage.color = Color.white;
                    temp.maxRand = Random.Range(1, C_Order_Input[pickIndex].Item[j].maxRand == 0 ? 5 : C_Order_Input[pickIndex].Item[j].maxRand);
                    temp.rateSale_Price = Sc.enums.Item_Price[k] - (int)((double)Sc.enums.Item_Price[k] * C_Order_Input[pickIndex].rateSale);

                    C_Order_Contants.transform.GetChild(index).gameObject.SetActive(true);
                }
            }
        }
        StartCoroutine(C_Join());
    }

    public void C_Order_Delivery() // 택배 오브젝트가 오는 장면
    {
        C_Order_Delivery_Ob.SetActive(true);
        C_Order_Delivery_Ob.transform.position = new Vector2(1944, 0);

        C_Order_Delivery_Ob.transform.DOMove(C_Order_Delivery_EndPosition.transform.position, speedC);
        C_Order_Delivery_Ob.GetComponent<Button>().interactable = true;
    }

    public void OrderExit_Button() { C_order_Manager_Ob.SetActive(false); }

}

[System.Serializable]
public class C_Order_Input{
    [Range(0, 8)] public int ifMin, ifMax; // 조건 최소, 최대
    [Range(0,1)] public double rateSale; // 1이 100% 할인율임 , 0.1이 10% 할인율
    public C_Order_itemInput[] Item; // 아이템 뭐뭐 들어갈껀지
    public C_Order_meterialInput[] Meterial; // 재료는 뭐뭐 들어갈껀지
}

[System.Serializable]
public class C_Order_itemInput
{
    [Range(0, 50)] public int maxRand = 0; // 0일시 5까지만 랜덤 돌리는거로 간주
    public Item_Name_List Item; // 아이템 뭐뭐 들어갈껀지
}

[System.Serializable]
public class C_Order_meterialInput
{
    [Range(0, 50)] public int maxRand = 0; // 0일시 5까지만 랜덤 돌리는거로 간주
    public Item_Meterial_List Meterial; // 재료는 뭐뭐 들어갈껀지
}
