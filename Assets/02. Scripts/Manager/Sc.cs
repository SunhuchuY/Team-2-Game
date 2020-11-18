using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc : MonoBehaviour
{
    public GameObject p_Operation_Menu;
    static public GameObject Operation_Menu;

    public GameObject p_Process_Menu;
    static public GameObject Process_Menu;
    public GameObject p_Process_Game;
    static public GameObject Process_Game;
    public GameObject p_Process_Combination_Game_Ob;
    static public GameObject Process_Combination_Game_Ob;

    public FadeInFadeOut p_fadeInFadeOut;
    static public FadeInFadeOut fadeInFadeOut;
    public Process_Game_Manager p_process_Game_Manager;
    static public Process_Game_Manager process_Game_Manager;
    public Process_Menu_Manager p_process_Menu_Manager;
    static public Process_Menu_Manager process_Menu_Manager;
    public SoundManagers p_soundManagers;
    static public SoundManagers soundManagers;
    public Enums p_enums;
    static public Enums enums;
    public Process_Combination_Game_Manager p_process_Combination_Game_Manager;
    static public Process_Combination_Game_Manager process_Combination_Game_Manager;

    public Stock_Manager p_stock_Manager;
    static public Stock_Manager stock_Manager;
    public C_Manager p_c_Manager;
    static public C_Manager c_Manager;
    public PickBox_Manager p_pickBox_Manager;
    static public PickBox_Manager pickBox_Manager;
    public C_Order_Delivery_Manager p_C_Order_Delivery_Manager;
    static public C_Order_Delivery_Manager C_Order_Delivery_Manager;
    public Request_Manager p_request_Manager;
    static public Request_Manager request_Manager;
    public RealTime_Manager p_realTime_Manager;
    static public RealTime_Manager realTime_Manager;


    private void Awake()
    {
        Process_Menu = p_Process_Menu;
        Process_Game = p_Process_Game;
        Operation_Menu = p_Operation_Menu;
        Process_Combination_Game_Ob = p_Process_Combination_Game_Ob;

        fadeInFadeOut = p_fadeInFadeOut;
        process_Game_Manager = p_process_Game_Manager;
        process_Menu_Manager = p_process_Menu_Manager;
        soundManagers = p_soundManagers;
        enums = p_enums;
        process_Combination_Game_Manager = p_process_Combination_Game_Manager;

        stock_Manager = p_stock_Manager;
        c_Manager = p_c_Manager;
        pickBox_Manager = p_pickBox_Manager;
        C_Order_Delivery_Manager = p_C_Order_Delivery_Manager;
        request_Manager = p_request_Manager;
        realTime_Manager = p_realTime_Manager;
    }
}
