using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime_Manager : MonoBehaviour
{
    // AM 00:00분 부터 시작
    public int h, m;// 시간, 분, 텍스트 업데이트 할 타이밍
    const int textUpdate_Time = 5, checkTime = 60; int i = 0;
    bool AM = true;
    public Text timeText;

    private void OnEnable()
    {
        StartCoroutine(RealTime_Play());
        TimeText_Update();
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
                if(h >= 12)
                {
                    AM = !AM;
                    h = 0;
                }
            }
            TimeText_Update();
        }    

        i++;
        StartCoroutine(RealTime_Play());
    }

    void TimeText_Update()
    {
        if(AM == true)
            timeText.text = "AM " + h.ToString("D2") + " : " + m.ToString("D2");
        else
            timeText.text = "PM " + h.ToString("D2") + " : " + m.ToString("D2");
    }
}
