using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 400;

    private void Update()
    {
        transform.localPosition += Vector3.right * speed * Time.deltaTime;
    }
}
