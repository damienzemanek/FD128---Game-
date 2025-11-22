using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Extensions
{
    [Serializable]
    public struct FadeSettings
    {
        public Graphic graphic;
        public float step;
        public float delay;
        public float delayToStartFading;
    }

    public static class FadeEX
    {
        public static void EnableFadeOut(this MonoBehaviour host, FadeSettings fade)
        {
            ResetFade(fade, true);
            host.StartCoroutine(C_FadeToTransparent(fade, () => ResetFade(fade, false)));
        }

        public static void ResetFade(FadeSettings fade, bool _active)
        {
            Color color = fade.graphic.color;
            color.a = 1f;
            fade.graphic.color = color;

            fade.graphic.gameObject.SetActive(_active);
        }

        public static IEnumerator C_FadeToTransparent(FadeSettings fade, Action postHook = null)
        {
            if (fade.delayToStartFading > 0)
                yield return new WaitForSeconds(fade.delayToStartFading);

            fade.graphic.gameObject.SetActive(true);

            float fadeVal = 1f;
            Color currentColor = fade.graphic.color;

            while (fadeVal > 0)
            {
                fadeVal -= fade.step;
                currentColor.a = fadeVal;

                fade.graphic.color = currentColor;
                yield return new WaitForSeconds(fade.delay);
            }

            currentColor.a = 0;

            fade.graphic.color = currentColor;

            postHook?.Invoke();
        }

        public static IEnumerator C_FadeToOpaque(FadeSettings fade, Action postHook = null)
        {
            if(fade.delayToStartFading > 0)
                yield return new WaitForSeconds(fade.delayToStartFading);


            fade.graphic.gameObject.SetActive(true);

            float fadeVal = 0;
            Color currentColor = fade.graphic.color;

            while (fadeVal < 1)
            {
                fadeVal += fade.step;
                currentColor.a = fadeVal;

                fade.graphic.color = currentColor;
                yield return new WaitForSeconds(fade.delay);
            }

            currentColor.a = 1;
            fade.graphic.color = currentColor;

            postHook?.Invoke();
        }
    }

}