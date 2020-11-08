using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Process_Game_Manager : MonoBehaviour
{
    int LoopOfnum; public int Score; public Text LoopOfnum_text, Count_Text, Score_Text;
    public GameObject TimingBar_Ob;
    public GameObject rest, interval, Temp_Parent; // 쉼표, 음정, 이 둘을 보관할 임시 저장 부모 오브젝트
    GameObject[] Temp = new GameObject[0];

    private void OnEnable() { StartCoroutine(Enable()); }

    IEnumerator Enable()
    {
        TimingBar_Ob.SetActive(false);

        LoopOfnum = Sc.process_Menu_Manager.rythem_Data.LoopOfnum;
        Score = 0;
        LoopOfnum_Text_Update();
        Score_Text_Update(0);
        Temp_Remove();

        yield return new WaitForSeconds(Sc.fadeInFadeOut.fade_time);

        Temp = new GameObject[Sc.process_Menu_Manager.rythem_Data.Syllable.Length];

        for (int i = 0; i < Sc.process_Menu_Manager.rythem_Data.Syllable.Length; i++)
        {
            GameObject to;

            if(Sc.process_Menu_Manager.rythem_Data.Syllable[i] == 1) {
                to = Instantiate(interval, Temp_Parent.transform);
            }
            else {
                to = Instantiate(rest, Temp_Parent.transform);
            }

            Temp[i] = to;
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
        if (LoopOfnum <= 0) {
            Sc.fadeInFadeOut.FadeFuntion();
            Sc.Process_Game.SetActive(false);
            Sc.process_Menu_Manager.Item_Meterial_Manager.SetActive(true);
            return; 
        }

        LoopOfnum_Text_Update();
        TimingBar_Ob.SetActive(true);
    }

    public void LoopOfnum_Text_Update() { LoopOfnum_text.text = "x " + LoopOfnum; }

    public void Score_Text_Update(int plus = 1) { Score += plus; Score_Text.text = "score : " + Score; }

    void Temp_Remove()
    {
        for (int i = 0; i < Temp.Length; i++)
        {
            Destroy(Temp[i]);
        }
    }


}
