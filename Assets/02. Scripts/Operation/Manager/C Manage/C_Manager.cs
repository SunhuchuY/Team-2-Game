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
    List<GameObject> C_queue = new List<GameObject>();
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

    // 의뢰형 손님
    public Text C_Request_CnameText, C_Request_explanText, C_Request_slotText; // 손님이름, 아이템 및 등급 설명, 수락(0/3) < 남은 슬롯 파악하기위해서
    Text[] C_Request_informationText = new Text[4]; // 총 4가지 정보 제공

    // 매니저 오브젝트 ,기본, 회수, 성공, 실패
    public GameObject C_Request_Manager_Ob, C_Request_Basic_Ob, C_Request_Magnification_Ob, C_Request_Success_Ob, C_Request_failure_Ob;
    public GameObject C_Request_Prefab, C_Request_informationText_Contants;



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

        for (int i = 0; i < C_Request_informationText.Length; i++)
        {
            C_Request_informationText[i] = C_Request_informationText_Contants.transform.GetChild(i).GetComponent<Text>();
        }
    }

    // 손님 관리 시스템
    IEnumerator C_Join(C_Type c) // 손님이 들어올때의 연출과 자리 배치
    {
        if (C_queue.Count >= 5) yield break;

        GameObject temp = null;

        switch (c)
        {
            case C_Type.C_Order:
                temp = Instantiate(C_Order_prefab, C_Contants);
                break;
            case C_Type.C_Request:
                temp = Instantiate(C_Request_Prefab, C_Contants);
                break;
        }

        C_queue.Add(temp);

        // 이동 연출
        temp.transform.position = C_startPosition[C_queue.Count - 1].position;
        yield return temp.transform.DOMove(C_endPosition[C_queue.Count - 1].position, speedC).WaitForCompletion();

        temp.GetComponent<C_Timer>().StartTimer(30f); // 게임상으론 1시간임.
    }

    public void C_Quit(GameObject ob)
    {
        for (int i = 0; i < C_queue.Count; i++)
        {
            if (C_queue[i].name == ob.name)
            {
                Destroy(C_queue[i]);
                C_queue.RemoveAt(i);
            }
        }
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
        StartCoroutine(C_Join(C_Type.C_Order));
    }

    public void C_Order_Delivery() // 택배 오브젝트가 오는 장면
    {
        C_Order_Delivery_Ob.SetActive(true);
        C_Order_Delivery_Ob.transform.position = new Vector2(1944, 0);

        C_Order_Delivery_Ob.transform.DOMove(C_Order_Delivery_EndPosition.transform.position, speedC);
        C_Order_Delivery_Ob.GetComponent<Button>().interactable = true;
    }

    public void OrderExit_Button() { C_order_Manager_Ob.SetActive(false); }


    // 의뢰형 손님
    public string rankRand_Funtion() // 등급을 먹이기 위한 랜덤 수 돌리기
    {
        int rankRand = Random.Range(1, 101);
        int[] rankRand_toArr = new int[6];

        int Shoprequ = (int)Main_Data.Shop_repu;

        rankRand_toArr[0] = 35 - (Shoprequ*3);

        for (int i = 0; i < rankRand_toArr.Length; i++)
        {
            switch (i)
            {
                case 1:
                    rankRand_toArr[i] = rankRand_toArr[i - 1] + (20 - (Shoprequ * 2));
                    break;
                case 2:
                    rankRand_toArr[i] = rankRand_toArr[i - 1] + (15 - (Shoprequ * 1));
                    break;
                case 3:
                    rankRand_toArr[i] = rankRand_toArr[i - 1] + 10;
                    break;
                case 4:
                    rankRand_toArr[i] = rankRand_toArr[i - 1] + (10 + (Shoprequ * 2));
                    break;
                case 5:
                    rankRand_toArr[i] = rankRand_toArr[i - 1] + (10 + (Shoprequ * 4));
                    break;
            }
        }

        if (rankRand >= 0 && rankRand <= rankRand_toArr[0])
            return "NOT";
        else if (rankRand > rankRand_toArr[0] && rankRand <= rankRand_toArr[1])
            return "D";
        else if (rankRand > rankRand_toArr[1] && rankRand <= rankRand_toArr[2])
            return "C";
        else if (rankRand > rankRand_toArr[2] && rankRand <= rankRand_toArr[3])
            return "B";
        else if (rankRand > rankRand_toArr[3] && rankRand <= rankRand_toArr[4])
            return "A";
        else if (rankRand > rankRand_toArr[4] && rankRand <= rankRand_toArr[5])
            return "S";

        return "무등급";
    }

    public void C_Request_Button(Request_List tempScript) // 해당 의뢰 버튼을 눌렀을시 시작되는 함수
    {
        C_Request_CnameText.text = tempScript.C_name;
        C_Request_explanText.text = $"{Sc.enums.Item_Korean_name_string[tempScript.itemIndex]}({tempScript.rank})을(를) " +
            $"제작 해주셨으면 합니다. {tempScript.day}일 {tempScript.h}시간 뒤에 재방문 합니다. 가능할까요?";

        C_Request_informationText[0].text = $"{Sc.enums.Item_Korean_name_string[tempScript.itemIndex]}({tempScript.rank})"; // 제품의 정보
        C_Request_informationText[1].text = $"{tempScript.day}일 {tempScript.h}시간 뒤 재방문"; // 다음 방문 일시
        C_Request_informationText[2].text = $"{tempScript.retainingMoney}G"; // 착수금
        C_Request_informationText[3].text = $"{tempScript.keyMoney}G"; // 완수금

        C_Request_slotText.text = $"수락({Sc.request_Manager.numOfData}/3)";
    }

    public void C_arriveRequest()
    {
        StartCoroutine(C_Join(C_Type.C_Request));
    }
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
