using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class timing_Manager : MonoBehaviour
{
    public List<GameObject> boxNodeList = new List<GameObject>();

    public Transform Center; // 판정 범위의 중심
    public RectTransform[] timingRect; // 판정 범위
    Vector2[] timingBoxs;

    void Start()
    {

        //타이밍 박스 설정(0번째는 가장 좁은 판정.)
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNodeList.Count; i++)
        {
            float t_nodePosX = boxNodeList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if(timingBoxs[x].x <= t_nodePosX && t_nodePosX <= timingBoxs[x].y)
                {
                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }
}
