using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Process_Combination_Game_Manager : MonoBehaviour
{
    public Text scoreText, countText;

    public GameObject Meterial_Rydhem_data_prefab, timingBar;

    List<GameObject> Note_List = new List<GameObject>();
    List<Rythem_Data> data = new List<Rythem_Data>();
    public Combination_Game_Data combination_Game_Data;

    int pickIndex, inIndex, outIndex;

    private void OnEnable()
    {
        // 오브젝트 전부 삭제
        data.RemoveRange(0, data.Count);

        for (int i = 0; i < Enums.Meterial_Len; i++)
        {
            if (Sc.process_Menu_Manager.Item_Meterial_Manager.GetComponent<ItemToMeterialStartGame>().Meterial_Contant.transform.GetChild(i).gameObject.activeSelf == true)
            {
                data.Add(Sc.process_Menu_Manager.Item_Meterial_Manager.GetComponent<ItemToMeterialStartGame>().Meterial_Contant.transform.GetChild(i).gameObject.GetComponent<Rythem_Data>());
                string meterialName = Sc.process_Menu_Manager.Item_Meterial_Manager.GetComponent<ItemToMeterialStartGame>().Meterial_Contant.transform.GetChild(i).gameObject.name;

                GameObject temp = Instantiate(Meterial_Rydhem_data_prefab, gameObject.transform);
                temp.name = meterialName;
                Note_List.Add(temp);
                for (int j = 0; j < data[data.Count-1].Syllable.Length; j++)
                {
                    if (data[data.Count-1].Syllable[j] == 1)
                    {
                        Instantiate(Sc.process_Game_Manager.interval, temp.transform);
                    }
                    else
                    {
                        Instantiate(Sc.process_Game_Manager.rest, temp.transform);
                    }
                }
            }
        }

        string use_Item_dataName = Sc.enums.Item_name_string[(int)Sc.process_Menu_Manager.Item_Data.Item_Name];
        inIndex = 0; outIndex = 0;

        for (int i = 0; i < combination_Game_Data.Combination_Data.Length; i++)
        {
            if (use_Item_dataName == Sc.enums.Item_name_string[(int)combination_Game_Data.Combination_Data[i].item_name])
            {
                pickIndex = i;
                break;
            }
        }

        StartCoroutine(Line_Start());
    }

    public void isNext()
    {
        if (combination_Game_Data.Combination_Data[pickIndex].Line[outIndex].LoopOfnum <= ++inIndex)
        {
            outIndex++;
            inIndex = 0;
            StartCoroutine(Line_Start());
        }
        else
        {
            StartCoroutine(InLine_Loop());
        }

    }

    IEnumerator Line_Start()
    {
        for (int i = 0; i < Note_List.Count; i++)
        {
            Note_List[i].SetActive(false);
        }

        for (int i = 0; i < Note_List.Count; i++)
        {
            for (int j = 0; j < combination_Game_Data.Combination_Data[pickIndex].Line[outIndex].Meterial_Combination.Length; j++)
            {
                if (Note_List[i].name == Sc.enums.Meterial_name_string
                [(int)combination_Game_Data.Combination_Data[pickIndex].Line[outIndex].Meterial_Combination[j]])
                {
                    Note_List[i].SetActive(true);
                }
            }
        }

        timingBar.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            countText.text = "" + (3 - i);
            yield return new WaitForSeconds(1f);
        }
        countText.text = "";
        timingBar.SetActive(true);

        yield return null;
    }

    IEnumerator InLine_Loop()
    {
        timingBar.SetActive(false);
        timingBar.SetActive(true);
        yield return null;
    }
}
