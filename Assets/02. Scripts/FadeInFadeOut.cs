using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInFadeOut : MonoBehaviour
{
    public Image image;
    public float fade_time = 1f;

    public void FadeFuntion() { StartCoroutine(Fade()); }

    IEnumerator Fade()
    {
        image.gameObject.SetActive(true);
        yield return image.DOFade(1, 0f).WaitForCompletion();
        yield return image.DOFade(0, fade_time).WaitForCompletion();
        image.gameObject.SetActive(false);
    }
}
