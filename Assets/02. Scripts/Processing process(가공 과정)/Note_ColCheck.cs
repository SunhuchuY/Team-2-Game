using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_ColCheck : MonoBehaviour
{
    public Note_Type type;
    bool isCheck;

    private void OnTriggerEnter2D(Collider2D col)
    {
        isCheck = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            switch (type)
            {
                case Note_Type.Rest:
                    if (Input.GetKeyDown(KeyCode.Space) && !isCheck)
                    {
                        isCheck = true;
                    }
                    break;
                case Note_Type.Hit:
                    if (Input.GetKeyDown(KeyCode.Space) && !isCheck)
                    {
                        isCheck = true;
                        Sc.soundManagers.List = Sound_List.temp_sound;
                        Sc.soundManagers.SoundPlay();
                        Sc.process_Game_Manager.Score_Text_Update();
                    }
                    break;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch (type)
        {
            case Note_Type.Rest:
                if (!isCheck)
                {
                    Sc.soundManagers.List = Sound_List.temp_sound;
                    Sc.soundManagers.SoundPlay();
                    Sc.process_Game_Manager.Score_Text_Update();
                }
                break;
        }
    }
}
