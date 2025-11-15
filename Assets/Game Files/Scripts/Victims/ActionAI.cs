using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class ActionAI
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public bool actionComplete;
    [SerializeField] public bool repeatedAction = false;
    [SerializeField] public bool inUse;
    public void Execute()
    {
        if (!repeatedAction)
        {
            if (inUse) return;
            inUse = true;
        }

        this.Log($"Succesfully executing {this.GetType()}");
        ExecuteImplement();
    }

    public abstract void ExecuteImplement();
}

[Serializable]
public class Idle : ActionAI
{
    public override void ExecuteImplement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}

[Serializable]
public class RunAway : ActionAI
{
    [SerializeField] float dist;

    [Button]
    public override void ExecuteImplement()
    {

        Vector3 newLoc = agent.transform.position + (-agent.transform.forward * dist);

        agent.isStopped = false;
        agent.SetDestination(newLoc);
    }


}

