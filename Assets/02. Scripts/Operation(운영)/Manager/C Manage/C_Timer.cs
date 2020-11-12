using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Timer : MonoBehaviour
{
    const float fillmountMax = 0.035f;

    public Image timerImage;
    public float timer = -1f;

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
        Sc.c_Manager.C_order_Manager_Ob.SetActive(true);
    }
}
