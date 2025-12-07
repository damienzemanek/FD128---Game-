using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using SingularityGroup.HotReload;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using static Extensions.AnimEX;
using static Extensions.NavEX;

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
    public string animName;
    public Animatable anims;
    public override void ExecuteImplement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        if (!anims.Equals(default(Animatable)))
            anims.Animate(animName);

    }
}

[Serializable]
public class Patrol : ActionAI
{
    public AgentAI ai;
    public Transform body;
    public Deviatable speed;
    [ReadOnly] public ConstantLookAt constantLookAt;
    public bool flipY = false;
    public Animatable anims;
    public string patrolMoveAnim;

    public int currentPoint;
    public Transform[] points;
    public override void ExecuteImplement()
    {
        if (HasError()) { this.Error("Patrol has invalid setup"); return; }

        if(agent.Has(out constantLookAt)) constantLookAt.looking = false;
        if (flipY) body.rotation = body.rotation.WithEuler(y: 0);
        agent.isStopped = false;
        anims.Animate(patrolMoveAnim);
        ai.StartCoroutine(MoveToPoint(currentPoint));
    }

    IEnumerator MoveToPoint(int indx)
    {
        Vector3 pos = points[indx].position.ToNearestNavmeshPoint(5);

        agent.speed = speed.value;
        agent.SetDestination(pos);
        yield return new WaitUntil(agent.Reached(30));
        
        currentPoint++;
        if (currentPoint >= points.Length)
            currentPoint = 0;

        if (ai.currentAction.GetType() == this.GetType())
            ExecuteImplement();
    }

    bool HasError()
    {
        if (agent == null) return true;
        if (ai == null) return true;
        if (points == null || points.Length == 0) return true;

        return false;
    }
}

[Serializable]
public class RunAway : ActionAI
{
    public string animName;
    [SerializeField] Vector2 speed;
    [SerializeField] float dist;
    public Animatable anims;


    [Button]
    public override void ExecuteImplement()
    {

        Vector3 newLoc = agent.transform.position + (-agent.transform.forward * dist);

        agent.isStopped = false;
        agent.speed = speed.Rand();
        agent.SetDestination(newLoc);
        anims.Animate(animName);
    }


}

[Serializable]
public class RunToHideout : ActionAI
{
    public string animName;
    [ReadOnly] public Transform hideoutLoc;
    [SerializeField] Vector2 speed;
    public Animatable anims;


    [Button]
    public override void ExecuteImplement()
    {
        agent.isStopped = false;
        agent.speed = speed.Rand();
        agent.SetDestination(hideoutLoc.position);
        anims.Animate(animName);
    }


}

[Serializable]
public class MoveToObject : ActionAI
{
    [SerializeField, ReadOnly] Transform loc;
    [SerializeField] float closeRange = 1f;
    [SerializeField] float rotSpeed = 10f;
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


            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation,
                                                            targetRot, 
                                                            rotSpeed * Time.deltaTime);
            }

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
