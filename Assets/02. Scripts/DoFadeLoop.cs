using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DoFadeLoop : MonoBehaviour
{
    public float duration, startValue, endValue;

    private void OnEnable()
    {
        StartCoroutine(LoopFade());
    }

    private void OnDisable()
    {
        StopCoroutine(LoopFade());
    }

    IEnumerator LoopFade()
    {
        yield return gameObject.GetComponent<Image>().DOFade(startValue, duration).WaitForCompletion();
        yield return gameObject.GetComponent<Image>().DOFade(endValue, duration).WaitForCompletion();

        StartCoroutine(LoopFade());
    }

}
