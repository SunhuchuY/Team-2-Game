using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationNote_ColCheck : MonoBehaviour
{
    public Note_Type type;
    public bool isCheck;

    private void OnTriggerEnter2D(Collider2D col)
    {
        isCheck = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (type)
            {
                case Note_Type.Rest:
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) 
                        || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)
                        || Input.GetKeyDown(KeyCode.Space) || transform.parent.GetComponent<CombinationNote_Manager>().isButton == true && !isCheck)
                    {
                        isCheck = true;
                    }
                    break;
                case Note_Type.Hit:
                    if (!isCheck)
                    {
                        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) 
                            || transform.parent.GetComponent<CombinationNote_Manager>().isButton == true)
                        {
                            transform.parent.GetComponent<CombinationNote_Manager>().you = gameObject;
                            transform.parent.GetComponent<CombinationNote_Manager>().isCheck_Left();
                        }

                        if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L) 
                            || transform.parent.GetComponent<CombinationNote_Manager>().isButton == true)
                        {
                            transform.parent.GetComponent<CombinationNote_Manager>().you = gameObject;
                            transform.parent.GetComponent<CombinationNote_Manager>().isCheck_Right();
                        }
                    }
                    break;
            }
        }
        transform.parent.GetComponent<CombinationNote_Manager>().isButton = false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch (type)
        {
            case Note_Type.Rest:
                if (!isCheck)
                {
                    transform.parent.GetComponent<CombinationNote_Manager>().Hit_Update_Funtion();
                }
                break;
        }
    }
}
