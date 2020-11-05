using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc : MonoBehaviour
{
    public GameObject p_Process_Menu;
    static public GameObject Process_Menu;
    public GameObject p_Process_Game;
    static public GameObject Process_Game;

    public FadeInFadeOut p_fadeInFadeOut;
    static public FadeInFadeOut fadeInFadeOut;
    public Process_Game_Manager p_process_Game_Manager;
    static public Process_Game_Manager process_Game_Manager;
    public Process_Menu_Manager p_process_Menu_Manager;
    static public Process_Menu_Manager process_Menu_Manager;
    public SoundManagers p_soundManagers;
    static public SoundManagers soundManagers;


    private void Awake()
    {
        Process_Menu = p_Process_Menu;
        Process_Game = p_Process_Game;

        fadeInFadeOut = p_fadeInFadeOut;
        process_Game_Manager = p_process_Game_Manager;
        process_Menu_Manager = p_process_Menu_Manager;
        soundManagers = p_soundManagers;
    }
}
