using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TMP_Input : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_InputField input;

    public void OnPointerClick(PointerEventData eventData)
    {
        input.interactable = true;
        input.ActivateInputField();
        input.Select();
    }
}
