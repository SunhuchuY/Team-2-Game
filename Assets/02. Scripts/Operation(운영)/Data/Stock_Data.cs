using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stock_Data : MonoBehaviour
{
    public GameObject prefab;
    int numOfData = 0;

    private void OnEnable()
    {
        for (int i = 0; i < Sc.enums.Item_countSort.Length; i++)
        {
            for (int j = 0; j < Sc.enums.Item_countSort[i]; j++)
            {
                Transform tempParent = transform.GetChild(numOfData);
                Instantiate(prefab, tempParent).GetComponent<Image>().sprite = Sc.enums.Item_Image_Ob[i];
                numOfData++;
            }
        }
    }
}
