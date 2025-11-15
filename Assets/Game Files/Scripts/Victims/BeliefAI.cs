using System;
using System.Collections;
using System.Collections.Generic;
using RetroArsenal;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public abstract class BeliefAI 
{
    [SerializeField] public bool usable;
    [SerializeField] public int priority;

    public abstract ActionAI immediateAction { get; set; }
}

[Serializable]
public class IsSafe : BeliefAI
{
    [SerializeField] public bool isSafe = true;
    [field: SerializeReference] public override ActionAI immediateAction { get; set; }

    public void Set(bool val)
    {
        isSafe = val;
        if (isSafe) usable = true;
        else usable = false;
    }

    public ActionAI GetAction()
    {
        if (isSafe) return immediateAction;
        else return null;
    }
}

[Serializable]
public class IsInDanger : BeliefAI
{
    [SerializeField] public bool isInDanger = false;
    [field: SerializeReference] public override ActionAI immediateAction { get; set; }

    public void Set(bool val)
    {
        isInDanger = val;
        if (isInDanger) usable = true;
        else usable = false;
    }


    public ActionAI GetAction()
    {
        if (isInDanger) return immediateAction;
        else return null;
    }
}
