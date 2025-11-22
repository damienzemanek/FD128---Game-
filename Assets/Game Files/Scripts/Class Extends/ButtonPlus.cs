using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using static Extensions.FadeEX;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class ButtonPlus : Button
{
    [SerializeField] public FadeSettings fade;
    [SerializeField] UnityEvent finishFade;
    public void FadeToTransparent()
        => StartCoroutine(C_FadeToTransparent(fade, () => finishFade?.Invoke()));
    public void FadeToOpaque() 
        => StartCoroutine(C_FadeToOpaque(fade, () => finishFade?.Invoke()));
}
