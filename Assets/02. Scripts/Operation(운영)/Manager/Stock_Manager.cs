using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Stock_Manager : MonoBehaviour
{
    const int maxC = 5;
    const float speedC = 0.7f;

    public float Height;

    int numOfData = 0;

    public GameObject prefab;
    public Transform Contants;

    // Mouse On > Item Name Call
    [SerializeField] GameObject ItemName_Ob;
    [SerializeField] Text ItemName_Text;

    // Explan
    [SerializeField] GameObject Explan_Ob;
    [SerializeField] Image Explan_Image;
    [SerializeField] Text Explan_Text;
    [SerializeField] Text Explan_nameText;
    [SerializeField] Text Explan_numText;


    private void OnEnable()
    {
        for (int i = 0; i < numOfData; i++)
        {
            Destroy(Contants.GetChild(i).GetChild(0).gameObject);
        }

        numOfData = 0;

        for (int i = 0; i < Sc.enums.Item_countSort.Length; i++)
        {
            for (int j = 0; j < Sc.enums.Item_countSort[i].Rank.Length; j++)
            {
                Transform tempParent = Contants.GetChild(numOfData);

                GameObject Temp = Instantiate(prefab, tempParent);
                Temp.GetComponent<Image>().sprite = Sc.enums.Item_sprite[i];
                Temp.GetComponent<Item_OnMouse>().itemIndex = i;
                Temp.GetComponent<Item_OnMouse>().rankIndex = j;
                numOfData++;
            }
        }
    }


    // 마우스 올릴시 아이템 이름 띄우기
    public void ItemNameCall_Funtion(GameObject you)
    {
        int temp = you.GetComponent<Item_OnMouse>().itemIndex;

        ItemName_Text.text = Sc.enums.Item_Korean_name_string[temp];

        ItemName_Ob.transform.position = new Vector2(you.transform.position.x, you.transform.position.y + Height);
        ItemName_Ob.SetActive(true);
    }

    public void ItemNameExit_Funtion() { ItemName_Ob.SetActive(false); }



    // 아이템 설명 창 띄우기
    public void ExplanCall_Funtion(GameObject you)
    {
        Item_OnMouse tempScript = you.GetComponent<Item_OnMouse>();

        Explan_Image.sprite = you.GetComponent<Image>().sprite;
        Explan_nameText.text = Sc.enums.Item_Korean_name_string[tempScript.itemIndex];
        Explan_Text.text = Sc.enums.Item_explanString[tempScript.itemIndex];
        Explan_numText.text = $"{Sc.enums.Item_countSort[tempScript.itemIndex].Rank[tempScript.rankIndex]}";

        Explan_Ob.SetActive(true);
    }

    public void ExplanExit_Funtion() { Explan_Ob.SetActive(false); }


}
