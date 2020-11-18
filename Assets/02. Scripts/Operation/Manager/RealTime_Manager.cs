using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime_Manager : MonoBehaviour
{
    // AM 00:00분 부터 시작
    public int h, m, day;// 시간, 분, 몇일
    const int textUpdate_Time = 5, maxDay = 30, checkTime = 60;
    int i = 0;
    float Time_Value;
    public bool AM = true;
    public Text timeText, dayText;

    private void OnEnable()
    {
        StartCoroutine(RealTime_Play());
        Text_Update();
    }

    IEnumerator RealTime_Play()
    {
        // 1초마다 조건검사
        yield return new WaitForSeconds(1f);

        // 리얼타임(실제시간) 5초가 게임의 10분이므로 텍스트 업데이트
        if (i >= textUpdate_Time)
        {
            i = 0;
            m += 10;
            if(m >= 60)
            {
                h++; m = 0;
                C_timeCheck();
                TimeValue_Check();
                if (h >= 12)
                {
                    if (!AM) day++;
                    AM = !AM;
                    h = 0;
                }
            }
            Text_Update();
        }    

        i++;
        StartCoroutine(RealTime_Play());
    }

    void Text_Update()
    {
        if(AM == true)
            timeText.text = "AM " + h.ToString("D2") + " : " + m.ToString("D2");
        else
            timeText.text = "PM " + h.ToString("D2") + " : " + m.ToString("D2");

        dayText.text = day + "/" + maxDay;
    }

    void C_timeCheck()
    {
        if (AM && h == 11) // 주문형 손님 오는시간
        {
            Sc.c_Manager.C_arriveOrder();
        }
        else if (!AM && h == 5 && Sc.pickBox_Manager.goldSum > 0) // 주문형 손님때 구매한 택배 오는시간
        {
            Sc.c_Manager.C_Order_Delivery();
        }
    }

    void TimeValue_Check() // 시간 부가치 변경해야하는지 체크
    {
        int tempTime = AM == true ? h : h + 12; // 24시간 기준으로 기록

        if(tempTime < 6) // 새벽 -0.01
            Time_Value = -0.01f;

        else if(tempTime < 10) // 아침 0
            Time_Value = 0f;

        else if (tempTime < 50) // 0.015
            Time_Value = 0.015f;

        else if (tempTime < 50) // 저녁 0.01
            Time_Value = 0.01f;

        else // 밤 0
            Time_Value = 0;
    }

    void C_Appear_Check() // 1초마다 식을 통해 손님이 올지 결정한다.
    {
        int index;
        float rand = Random.Range(0f, 0.1f);
        float tempAppear = 0.01f + (Main_Data.Shop_repu / 100) + Time_Value;
        
        if(tempAppear >= rand) // 등장
        {
            // 일단은 손님유형이 하나밖에 없으므로 의뢰형만 출연

            index = Sc.request_Manager.isEmpty_ToIndex();
            if(index != -1) // 비었으므로 등장
            {
                print("a");
                Sc.c_Manager.C_arriveRequest();
            }
        }

        return;
    }
}
