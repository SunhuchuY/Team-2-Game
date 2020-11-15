using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class C_Timer : MonoBehaviour
{
    const float fillmountMax = 0.035f, ordeManager_Ob_trueSpeed = 1.5f;
    public Ease ordeManager_Ob_trueEase;

    public Image timerImage;
    public float timer = -1f;

    private void OnEnable()
    {
        if(timer > 0)
        {
            StartCoroutine(Timer()); // 만들기, 손님과 대화중의 경우를 위함.
        }
    }

    public void StartTimer()
    {
        timer = 30f;
        timerImage.fillAmount = (timer * fillmountMax);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        timer--;
        timerImage.fillAmount = (timer * fillmountMax);

        if(timer > 0)
        {
            StartCoroutine(Timer());
        }
        else
        {
            Sc.c_Manager.C_Quit();
        }
    }

    public void Order_Button()
    {
        Sc.c_Manager.C_order_Manager_Ob.transform.position = new Vector2(1940, 0);
        Sc.c_Manager.C_order_Manager_Ob.SetActive(true);
        Sc.c_Manager.C_order_Manager_Ob.transform.DOMove(Vector2.zero, ordeManager_Ob_trueSpeed).SetEase(ordeManager_Ob_trueEase);

        Main_Data.Now_C_Init(gameObject);
    }
}
