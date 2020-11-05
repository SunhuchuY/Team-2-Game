using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagers : MonoBehaviour
{
    public GameObject[] Sounds;
    public Sound_List List;

    public void SoundPlay()
    {
        switch (List)
        {
            case Sound_List.temp_sound:
                Sounds[0].SetActive(false);
                Sounds[0].SetActive(true);
                break;
        }
    }
}
