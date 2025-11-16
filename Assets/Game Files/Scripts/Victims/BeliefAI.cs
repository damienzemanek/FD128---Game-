using System;
using System.Collections;
using System.Collections.Generic;
using RetroArsenal;
using Sirenix.OdinInspector;
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

[Serializable]
public class IsDead : BeliefAI
{
    [SerializeField] public bool isDead = false;
    [field: SerializeReference] public override ActionAI immediateAction { get; set; }

    public void Set(bool val)
    {
        isDead = val;
        if (isDead) usable = true;
        else usable = false;
    }


    public ActionAI GetAction()
    {
        if (isDead) return immediateAction;
        else return null;
    }
}

[Serializable]
public class IsObjectIWantNearby : BeliefAI
{
    [SerializeField] public bool isObjectIWantNearby = false;
    [SerializeField, ReadOnly] Transform location;
    [field: SerializeReference] public override ActionAI immediateAction { get; set; }

    public void Set(bool val, Transform _location)
    {
        isObjectIWantNearby = val;
        if (isObjectIWantNearby)
        {
            usable = true;
            location = _location;

            if(immediateAction is MoveToObject moveToObj) moveToObj.GiveData(_loc: location);
        }
        else
        {
            location = null;
            usable = false;

            if (immediateAction is MoveToObject moveToObj) moveToObj.GiveData(null);

            Debug.Log("ate");

        }
    }


    public ActionAI GetAction()
    {
        if (!isObjectIWantNearby) return null;


        if (immediateAction is MoveToObject moveToObj)
        {
            moveToObj.GiveData(location);
            return moveToObj;
        }
        else
            return immediateAction;
    }
}

