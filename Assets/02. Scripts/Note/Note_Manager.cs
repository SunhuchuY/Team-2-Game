using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Manager : MonoBehaviour
{
    public int bpm = 0; // 비트 펄 미닛의 약자
    double currentTime = 0d;

    public Transform noteApped_tr; // 노트가 생성 될 위치
    public GameObject note_prefeb; // 노트 프리펩

    timing_Manager timing_Manager;

    private void Start()
    {
        timing_Manager = GetComponent<timing_Manager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject temp_note = Instantiate(note_prefeb, noteApped_tr.position, Quaternion.identity);
            temp_note.transform.SetParent(this.transform);
            timing_Manager.boxNodeList.Add(temp_note);
            currentTime -= 60d / bpm; // 시간 손실 줄이기 위해
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Note"))
        {
            timing_Manager.boxNodeList.Remove(col.gameObject);
            Destroy(col.gameObject);
        }
    }
}
