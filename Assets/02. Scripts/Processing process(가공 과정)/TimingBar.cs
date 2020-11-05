using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingBar : MonoBehaviour
{
    public float speed = 400;
    public Transform Start_Position;

    Timing_State state;

    bool Hit = false, Rest = true;

    private void OnEnable() { transform.position = Start_Position.position; }

    private void Update() { transform.localPosition += Vector3.right * speed * Time.deltaTime; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EndLine") { Sc.process_Game_Manager.isNext(); }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Hit" && !Hit)
        {
            Hit = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Hit = true;
            }
        }
        else if (col.tag == "Rest" && Rest)
        {
            Rest = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rest = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Hit" && Hit)
        {
            Sc.soundManagers.List = Sound_List.temp_sound;
            SoundPlay();
            Sc.process_Game_Manager.Score_Text_Update();
        }

        if (col.tag == "Rest" && Rest)
        {
            Sc.soundManagers.List = Sound_List.temp_sound;
            Sc.process_Game_Manager.Score_Text_Update();
            SoundPlay();
        }

        Hit = false;
        Rest = true;

    }

    void SoundPlay()
    {
        Sc.soundManagers.SoundPlay();
    }
}
