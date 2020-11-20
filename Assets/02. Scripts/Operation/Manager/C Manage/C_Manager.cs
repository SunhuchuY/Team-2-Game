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
    bool[] C_queue_bool = new bool[5];
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
    public Image C_Request_cImage;

    // C_Request_failure_Ob 에서 쓰이는 오브젝트들
    public GameObject C_Request_retaining_buttonOb; // 착수금이 없는 경우 착수금 받는버튼을 없애야 함
    public GameObject C_Request_repick_buttonOb; // 의뢰한 아이템의 랭크보다 큰 랭크의 아이템이 있는경우 다시 고르기 버튼을 활성화 해야 함
    public Text C_Request_retaining_Text;

    // 회수 하러 올때 필요한 데이터 : 편의상 이름을 지을시 C_ReCollection 을 앞에 붙인다.
    int numOfData = 0;
    string request_pickName;
    public GameObject C_ReCollection_itemContants, C_ReCollection_itemPrefab; // 조건에 해당하는 아이템들을 저장해두는 부모 오브젝트, 저장할때 쓰이는 프리팹

    GameObject C_ReCollection_pickOb;
    public GameObject C_ReCollection_itemInformation, C_ReCollection_pickImage; // 밑의 컴포넌트들을 키기 위한 오브젝트, 픽한 아이템을 표시하기 위한 이미지 오브젝트
    public GameObject C_ReCollection_Sugest_buttonOb; // 밑의 컴포넌트들을 키기 위한 오브젝트, 픽한 아이템을 표시하기 위한 이미지 오브젝트
    public Text C_ReCollection_itemName_Text, C_ReCollection_itemInformation_Text; // 아이템 이름, 아이템 설명 텍스트
    public Image C_ReCollection_itemImage; // 아이템 이미지


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
            C_Request_informationText[i] = C_Request_informationText_Contants.transform.GetChild(i).GetChild(0).GetComponent<Text>();
        }
    }

    // 손님 관리 시스템
    IEnumerator C_Join(C_Type c, GameObject temps = null) // 손님이 들어올때의 연출과 자리 배치
    {
        if (C_queue.Count >= 5)
        {
            if(c == C_Type.C_Request_collection || c == C_Type.C_Order) // 회수하러 왔다면
            {
                while(C_queue.Count >= 5)
                {
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            { yield break; }
        }

        GameObject temp = temps;

        switch (c)
        {
            case C_Type.C_Order:
                temp = Instantiate(C_Order_prefab, C_Contants);
                temp.name = $"OC";
                break;
            case C_Type.C_Request:
                temp = Instantiate(C_Request_Prefab, C_Contants);
                temp.name = $"RC{C_queue.Count - 1}";
                break;
        }

        C_queue.Add(temp);

        // 이동 연출
        int index = 0;
        for (int i = 0; i < C_queue_bool.Length; i++)
        {
            if (!C_queue_bool[i])
            {
                if(c != C_Type.C_Request_collection)
                    temp.GetComponent<C_Timer>().C_queue_bool_index = i;

                C_queue_bool[i] = true;
                index = i;
                break;
            }
        }

        temp.transform.position = C_startPosition[index].position;
        yield return temp.transform.DOMove(C_endPosition[index].position, speedC).WaitForCompletion();

        if (c != C_Type.C_Request_collection)
            temp.GetComponent<C_Timer>().StartTimer(30f); // 게임상으론 1시간임.
    }

    public void C_Quit(string obName)
    {
        for (int i = 0; i < C_queue.Count; i++)
        {
            if (C_queue[i].name == obName)
            {
                if(C_queue[i].GetComponent<C_Timer>() != null)
                    C_queue_bool[C_queue[i].GetComponent<C_Timer>().C_queue_bool_index] = false;
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
        // 손님 오브젝트생성 부분
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
            return "F";
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

        return "F";
    }


    Request_List request_List;
    public void C_Request_Button(Request_List tempScript) // 해당 의뢰 버튼을 눌렀을시 시작되는 함수
    {
        C_Request_repick_Button(); C_Request_Magnification_Ob.SetActive(false);

        request_List = tempScript;

        C_Request_Manager_Ob.SetActive(true);
        C_Request_Basic_Ob.SetActive(true);

        C_Request_CnameText.text = tempScript.C_name;
        C_Request_cImage.sprite = tempScript.C_image;
        C_Request_explanText.text = $"{Sc.enums.Item_Korean_name_string[tempScript.itemIndex]}({tempScript.rank}급 이상)을(를) " +
            $"제작 해주셨으면 합니다. {tempScript.day}일 {tempScript.h}시간 뒤에 재방문 합니다. 가능할까요?";

        C_Request_informationText[0].text = $"{Sc.enums.Item_Korean_name_string[tempScript.itemIndex]}({tempScript.rank}급 이상)"; // 제품의 정보
        C_Request_informationText[1].text = $"{tempScript.day}일 {tempScript.h}시간 뒤 재방문"; // 다음 방문 일시
        C_Request_informationText[2].text = $"착수금({tempScript.retainingMoney}G)"; // 착수금
        C_Request_informationText[3].text = $"완수금({tempScript.keyMoney}G)"; // 완수금

        C_Request_slotText.text = $"수락({Sc.request_Manager.isSize()}/3)";
    }

    public void C_Request_accept_Button() // 수락 버튼, 위 함수의 보면 request_List의 정의를 알수있음
    {
        C_Quit(request_List.gameObject.name);
        Sc.request_Manager.ToIndex_Request_Append(request_List);
        Main_Data.Gold += request_List.retainingMoney; // 착수금 - 연산
        C_Request_ExitButton();
    }

    public void C_Request_ExitButton()
    {
        C_Request_Manager_Ob.SetActive(false);
        C_Request_Basic_Ob.SetActive(false);
    }

    public void C_arriveRequest() // 의뢰 문의 오브젝트 대행 함수
    {
        StartCoroutine(C_Join(C_Type.C_Request));
    }

    public void C_Request_Collection_Join(GameObject ob) // 회수오브젝트가 들어오게 할수있는 대행 함수
    {
        StartCoroutine(C_Join(C_Type.C_Request_collection, ob));
    }

    public void C_Request_Collection_Button(GameObject Request_Collection_ToOb) // 회수 버튼을 클릭시에
    {
        C_ReCollection_pickOb = Request_Collection_ToOb;

        int RequesttoIndex = Request_Collection_ToOb.GetComponent<Request_Collection>().Request_Collection_Index,
            itemIndex = Sc.request_Manager.Request_Datas[RequesttoIndex].itemIndex,
            ifrank_Toint = Sc.enums.RankToInt_System(Sc.request_Manager.Request_Datas[RequesttoIndex].rank);

        // 리스트 안에 있는 프리팹 전부 초기화
        for (int i = 0; i < numOfData; i++)
        {
            Destroy(C_ReCollection_itemContants.transform.GetChild(i).GetChild(0).gameObject);
        }

        numOfData = 0;

        // 조건에 맞을때만 오브젝트 생성
        for (int i = 0; i < Sc.enums.Item_countSort[itemIndex].Rank.Length; i++)
        {
            //if (Sc.enums.RankToInt_System(Sc.enums.Item_countSort[itemIndex].Rank[i]) >= ifrank_Toint)
            //{
                GameObject tempOb = Instantiate(C_ReCollection_itemPrefab, C_ReCollection_itemContants.transform.GetChild(numOfData));
                tempOb.GetComponent<Image>().sprite = Sc.enums.Item_sprite[itemIndex];
                tempOb.name = Sc.enums.Item_countSort[itemIndex].Rank[i];

                numOfData++;
            //}
        }

        C_Request_CnameText.text = Sc.request_Manager.Request_Datas[RequesttoIndex].C_name;
        C_Request_explanText.text = $"맡긴 {Sc.request_Manager.Request_Datas[RequesttoIndex].rank}급 이상의 {Sc.enums.Item_Korean_name_string[itemIndex]}을 찾으러 왔습니다";

        Sc.c_Manager.C_Request_Manager_Ob.SetActive(true);
        Sc.c_Manager.C_Request_Magnification_Ob.SetActive(true);
        C_Request_Collection_pickInit();
    }

    public void C_Request_Collection_ExitButton()
    {
        Sc.c_Manager.C_Request_Manager_Ob.SetActive(false);
        Sc.c_Manager.C_Request_Magnification_Ob.SetActive(false);
    }

    public void C_Request_Collection_pickButton(GameObject To) // 해당하는 조건 아이템중에서 선택하는 버튼
    {
        int RequesttoIndex = C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index;
        request_pickName = To.name;

        C_ReCollection_itemImage.sprite = Sc.enums.Item_sprite[Sc.request_Manager.Request_Datas[RequesttoIndex].itemIndex];
        C_ReCollection_itemName_Text.text = $"{Sc.enums.Item_Korean_name_string[Sc.request_Manager.Request_Datas[RequesttoIndex].itemIndex]}({request_pickName}급)";
        C_ReCollection_itemInformation_Text.text = $"{Sc.enums.Item_explanString[Sc.request_Manager.Request_Datas[RequesttoIndex].itemIndex]}";
        C_ReCollection_itemInformation.SetActive(true);

        C_ReCollection_pickImage.transform.position = To.transform.position;
        C_ReCollection_Sugest_buttonOb.SetActive(true);
    }

    void C_Request_Collection_pickInit()
    {
        C_ReCollection_itemInformation.SetActive(false);
        C_ReCollection_pickImage.transform.position = new Vector2(4000, 0);
        C_ReCollection_Sugest_buttonOb.SetActive(false);
    }

    public void C_Request_Collection_suggestButton() // 아이템 제시 버튼
    {
        C_Request_Success_Ob.SetActive(false);
        C_Request_failure_Ob.SetActive(false);

        int RequesttoIndex = C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index,
            ifrank_Toint = Sc.enums.RankToInt_System(Sc.request_Manager.Request_Datas[RequesttoIndex].rank);

        if (ifrank_Toint <= Sc.enums.RankToInt_System(request_pickName))
        {
            // Success 창을 띄운다.
            C_Request_explanText.text = "제작 해주셔서 감사합니다!";
            C_Request_Success_Ob.SetActive(true);
        }
        else
        {
            // failure 창을 띄운다
            C_Request_explanText.text = "이건 제가 원하던게 아니잖아요! 실망입니다!";
            C_Request_repick_buttonOb.SetActive(false);
            C_Request_retaining_buttonOb.SetActive(false);
            C_Request_failure_Ob.SetActive(true);
            C_Request_retaining_Text.text = $"착수금환불({Sc.request_Manager.Request_Datas[RequesttoIndex].retainingMoney}G)";

            if (Sc.request_Manager.Request_Datas[RequesttoIndex].retainingMoney > 0) // 착수금이 있는 경우, 착수금 받기 버튼이 있어야함
                C_Request_retaining_buttonOb.SetActive(true);

            for (int i = 0; i < numOfData; i++)
            {
                if(ifrank_Toint <= Sc.enums.RankToInt_System(C_ReCollection_itemContants.transform.GetChild(i).name))
                {
                    C_Request_repick_buttonOb.SetActive(true);
                    break;
                }
            }
        }

        C_Request_Magnification_Ob.SetActive(false);
    }

    public void C_Request_repick_Button() // 성공 (다시 픽하기 버튼)
    {
        C_Request_Success_Ob.SetActive(false);
        C_Request_failure_Ob.SetActive(false);

        C_Request_Magnification_Ob.SetActive(true);
    }

    public void C_Request_Success_Button() // 성공 버튼(완수금 받기)
    {
        Request_Collection_Exit();

        Main_Data.Gold += Sc.request_Manager.Request_Datas[C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index].keyMoney;
        Main_Data.Shop_repu += Sc.request_Manager.Request_Datas[C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index].rand_Request_Magnification;

        Sc.enums.Item_countSort_Data_Remove(Sc.request_Manager.Request_Datas[C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index].itemIndex
            , request_pickName);
    }

    public void C_Request_retainingMoney_Button() // 실패(착수금 받기 버튼)
    {
        Request_Collection_Exit();

        Main_Data.Gold -= Sc.request_Manager.Request_Datas[C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index].retainingMoney;
        Main_Data.Shop_repu -= 0.4f;
    }

    public void C_Request_Apologize() // 실패(사과하기 버튼)
    {
        Request_Collection_Exit();

        if (C_Request_retaining_buttonOb.activeSelf == true) // 착수금이 있는 상태임. 있는 상태에서 사과하기 버튼을 누를경우
        {
            Main_Data.Shop_repu -= 0.8f;
            return;
        }

        // 착수금 없는 상태
        Main_Data.Shop_repu -= 0.3f;
    }

    void Request_Collection_Exit() // 회수형 종료 함수
    {
        Sc.c_Manager.C_Request_Manager_Ob.SetActive(false);
        Sc.c_Manager.C_Request_Magnification_Ob.SetActive(false);

        Sc.request_Manager.Request_Datas[C_ReCollection_pickOb.GetComponent<Request_Collection>().Request_Collection_Index].isEmpty = 0;
        C_Quit(C_ReCollection_pickOb.name);
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
