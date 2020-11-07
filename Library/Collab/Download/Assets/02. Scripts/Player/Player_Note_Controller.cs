using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Note_Controller : MonoBehaviour
{
    timing_Manager timing_Manager;

    void Start()
    {
        timing_Manager = FindObjectOfType<timing_Manager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timing_Manager.CheckTiming();
        }
    }
}
