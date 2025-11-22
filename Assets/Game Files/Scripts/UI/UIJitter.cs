using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJitter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float duration;
    [SerializeField] bool onMouseOver = true;

    public bool Size;
    [ShowIf("Size")] [SerializeField, ReadOnly] Vector3 initialSize;
    [ShowIf("Size")][SerializeField] Vector3 sizeJitterIncrease;

    public bool Rot;
    [ShowIf("Rot")][SerializeField, ReadOnly] Quaternion initialRot;
    [ShowIf("Rot")][SerializeField] Quaternion rotTo;

    public bool Pos;
    [ShowIf("Pos")][SerializeField, ReadOnly] Vector3 initialPos;
    [ShowIf("Pos")][SerializeField] Vector3 posTo;

    private void Awake()
    {
        initialSize = transform.localScale;
        initialRot = transform.rotation;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(onMouseOver)
            Jitter();
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (onMouseOver)
            ResetJitter();
    }


    public void Jitter()
    {
        StopAllCoroutines();
        if (Size) transform.LerpScale(sizeJitterIncrease, duration, this);
        if (Rot) transform.LerpRot(rotTo, duration, this);
        if (Pos) transform.Lerp(posTo, duration, this);
    }

    public void ResetJitter()
    {
        StopAllCoroutines();
        if (Size) transform.LerpScale(initialSize, duration, this);
        if (Rot) transform.LerpRot(initialRot, duration, this);
        if (Pos) transform.Lerp(initialPos, duration, this);
    }

    [Button]
    public void JitterThenReset()
    {
        bool moving = false;

        void Done()
        {
            if (!moving) return;
            moving = false;      
            ResetJitter();
        }

        if (Size) { moving = true; transform.LerpScale(sizeJitterIncrease, duration, this, Done); }
        if (Rot) { moving = true; transform.LerpRot(rotTo, duration, this, Done); }
        if (Pos) { moving = true; transform.Lerp(posTo, duration, this, Done); }

        if (!moving)
            ResetJitter();
    }

}
