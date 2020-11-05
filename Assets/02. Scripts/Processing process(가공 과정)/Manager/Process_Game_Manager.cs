using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class Process_Game_Manager : MonoBehaviour
{
    int LoopOfnum; public int Score; public Text LoopOfnum_text, Count_Text, Score_Text;
    public GameObject TimingBar_Ob;
    public GameObject rest, interval, Temp_Parent; // 쉼표, 음정, 이 둘을 보관할 임시 저장 부모 오브젝트

    private void OnEnable() { StartCoroutine(Enable()); }

    IEnumerator Enable()
    {
        TimingBar_Ob.SetActive(false);

        LoopOfnum = Sc.process_Menu_Manager.rythem_Data.LoopOfNum;
        Score = 0;
        LoopOfnum_Text_Update();

        yield return new WaitForSeconds(Sc.fadeInFadeOut.fade_time);

        for (int i = 0; i < Sc.process_Menu_Manager.rythem_Data.Syllable.Length; i++)
        {
            if(Sc.process_Menu_Manager.rythem_Data.Syllable[i] == 1)
            {
                GameObject Temp = Instantiate(interval, Temp_Parent.transform);
            }
            else
            {
                GameObject Temp = Instantiate(rest, Temp_Parent.transform);
            }
        }

        for (int i = 0; i < 3; i++) // 3초 카운트
        {
            Count_Text.text = "" + (3 - i);
            yield return new WaitForSeconds(1f);
        }
        Count_Text.text = "";

        TimingBar_Ob.SetActive(true);
    }

    public void isNext()
    {
        TimingBar_Ob.SetActive(false);
        LoopOfnum--;
        if (LoopOfnum <= 0) { return; }

        LoopOfnum_Text_Update();
        TimingBar_Ob.SetActive(true);
    }

    public void LoopOfnum_Text_Update() { LoopOfnum_text.text = "x " + LoopOfnum; }

    public void Score_Text_Update() { Score++; Score_Text.text = "score : " + Score; }


}
