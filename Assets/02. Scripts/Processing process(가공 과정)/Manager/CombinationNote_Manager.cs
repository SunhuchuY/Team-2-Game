using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationNote_Manager : MonoBehaviour
{
    public Button Check_Button;
    public bool isButton = false;
    public RightAndLeft type;

    public GameObject you = null;

    public void Setting(GameObject ToButton_Ob)
    {
        Check_Button = ToButton_Ob.GetComponent<Button>();
        Check_Button.onClick.AddListener(Check_Button_Fun);
    }

    public void Check_Button_Fun()
    {
        isButton = true;
    }

    public void isCheck_Right()
    {
        if(type == RightAndLeft.right)
        {
            // 실행
            Hit_Update_Funtion();
            you.GetComponent<CombinationNote_ColCheck>().isCheck = true;
        }
    }

    public void isCheck_Left()
    {
        if (type == RightAndLeft.left)
        {
            // 실행
            Hit_Update_Funtion();
            you.GetComponent<CombinationNote_ColCheck>().isCheck = true;
        }
    }

    public void Hit_Update_Funtion()
    {
        Sc.soundManagers.List = Sound_List.temp_sound;
        Sc.soundManagers.SoundPlay();
        Sc.process_Combination_Game_Manager.Score_Text_Update();
    }

}
