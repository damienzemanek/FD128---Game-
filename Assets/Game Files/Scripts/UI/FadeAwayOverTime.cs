using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

public class FadeAwayOverTime : MonoBehaviour
{
    [SerializeField] bool fadeOnEnable = true;
    [SerializeField] float delayToStartFading = 1f;
    [SerializeField] bool isImg;
    [SerializeField] bool isText;

    [SerializeField, ShowIf("isImg")] Image img;
    [SerializeField, ShowIf("isText")] TextMeshProUGUI text;
    [SerializeField] float fadeDecrementAmount = 0.01f;


    private void OnEnable()
    {
        ResetFade(false);
        StopAllCoroutines();
        if (fadeOnEnable) this.DelayedCall(() => FadeAway(() => ResetFade(true)), delayToStartFading);
    }

    void ResetFade(bool setInActive)
    {
        if (isImg)
        {
            Color color = img.color;
            color.a = 1f;
            img.color = color;
        }
        if (isText)
        {
            Color color = text.color;
            color.a = 1f;
            text.color = color;
        }

        StopAllCoroutines();

        if(setInActive)
            gameObject.SetActive(false);
    }

    public void FadeAway(Action postHook) => StartCoroutine(C_FadeAway(postHook));

    IEnumerator C_FadeAway(Action postHook)
    {
        float fadeVal = 1f;
        Color currentColor = Color.white;

        if(isImg)
            currentColor = img.color;
        if (isText)
            currentColor = text.color;
        else
            this.Error("img or text not selected, select one to fade");



        while (fadeVal > 0)
        {
            fadeVal -= fadeDecrementAmount;
            currentColor.a = fadeVal;

            if(isImg)
                  img.color = currentColor;
            if(isText)
                  text.color = currentColor;

            yield return new WaitForSeconds(0.01f);
        }

        currentColor.a = 0;

        if(isImg)
            img.color = currentColor;
        if(isText)
            text.color = currentColor;

        postHook?.Invoke();
    }
}
