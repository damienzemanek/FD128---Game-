using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using SingularityGroup.HotReload;
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

[Serializable]
public class MoveToObject : ActionAI
{
    [SerializeField, ReadOnly] Transform loc;
    [SerializeField] float closeRange = 1f;
    [ShowInInspector, ReadOnly] bool toClose;
 
    [Button]
    public override void ExecuteImplement()
    {
        if (!loc) return;

        if (ToClose()) return;

        agent.updateRotation = true;
        toClose = false;
        agent.isStopped = false;
        agent.SetDestination(loc.transform.position);
    }

    public void GiveData(Transform _loc)
    {
        loc = _loc;
    }


    bool ToClose()
    {
        float dist = Vector3.Distance(agent.transform.position, loc.position);

        if (dist < agent.stoppingDistance + closeRange)
        {
            toClose = true;
            this.Log("to close");
            agent.isStopped = true;
            agent.updateRotation = false;

            Vector3 lookDir = loc.position - agent.transform.position;
            lookDir.y = 0;


            if (lookDir.sqrMagnitude > 0.01f) agent.transform.rotation = Quaternion.LookRotation(lookDir);
            return true;
        }
        return false;
    }

}

[Serializable]
public class Die : ActionAI
{
    [SerializeField] ConstantLookAt looker;
    public override void ExecuteImplement()
    {
        looker.looking = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.enabled = false;
    }
}
