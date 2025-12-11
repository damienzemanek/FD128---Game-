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
using static SignalUtility;
using static Extensions.DelegateEX;
using static UnityEditor.PlayerSettings;

[Serializable]
public abstract class ActionAI
{
    [TabGroup("Action Parameters")][SerializeField] public NavMeshAgent agent;
    [TabGroup("Action Parameters")][SerializeField] public bool actionComplete;
    [TabGroup("Action Parameters")][SerializeField] public bool repeatedAction = false;
    [TabGroup("Action Parameters")][SerializeField] public Reactable<bool> inUse;
    public void Execute(bool overrideInUse = false)
    {
        inUse.Sub(NotInUseAnymore);

        if (!repeatedAction)
        {
            if (inUse.value && !overrideInUse) return;
            inUse.value = true;
        }

        ExecuteImplement();
    }

    public abstract void ExecuteImplement();
    public void NotInUseAnymore(bool val)
    {
        if (val == true) return;
        NotInUseAnymoreImplement();
    }

    public virtual void NotInUseAnymoreImplement() { }
}

[Serializable]
public class Idle : ActionAI
{
    [TabGroup("Idle")] public string animName;
    [TabGroup("Idle")] public Animatable anims;
    public override void ExecuteImplement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        if (!anims.Equals(default(Animatable)))
            anims.CrossFade(animName);
        this.Log("Idling");

    }
}

[Serializable]
public class Patrol : ActionAI
{
    [TabGroup("Patrol")] public AgentAI ai;
    [TabGroup("Patrol")] public Deviatable speed;
    [TabGroup("Patrol")] public Animatable anims;
    [TabGroup("Patrol")] public string patrolMoveAnim;

    [TabGroup("Patrol")] public int currentPoint;
    [TabGroup("Patrol")] public bool randomPoints;
    [TabGroup("Patrol"), ShowIf("randomPoints")] public float randomPointRange;
    [TabGroup("Patrol"), ShowIf("@!randomPoints")] public Transform[] points;
    public override void ExecuteImplement()
    {
        if (HasError()) { this.Error("Patrol has invalid setup"); return; }

        if(agent.Has(out ConstantLookAt c)) c.looking = false;
        anims.animator.transform.SetLocalEuler(y: 0);
        agent.isStopped = false;
        anims.Animate(patrolMoveAnim);
        ai.StartCoroutine(routine: MoveToPoint(currentPoint));
    }

    IEnumerator MoveToPoint(int indx)
    {
        Vector3 pos = GetPoint(indx);

        agent.speed = speed.value;
        agent.SetDestination(pos);
        yield return new WaitUntil(agent.Reached(30));
        
        currentPoint++;
        if (currentPoint >= points.Length)
            currentPoint = 0;

        if (ai.currentAction.GetType() == this.GetType())
            ai.StartCoroutine(routine: MoveToPoint(currentPoint));
    }

    Vector3 GetPoint(int indx)
    {
        if (!randomPoints)
            return points[indx].position.ToNearestNavmeshPoint(5);
        else
            return agent.transform.RandomNavMeshPoint(randomPointRange);
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
    [TabGroup("Run Away")] public string animName;
    [TabGroup("Run Away")][SerializeField] Deviatable speed;
    [TabGroup("Run Away")][SerializeField] float dist;
    [TabGroup("Run Away")] public Animatable anims;


    public override void ExecuteImplement()
    {

        Vector3 newLoc = agent.transform.position + (-agent.transform.forward * dist);

        if (agent.Has(out ConstantLookAt c)) c.looking = true;

        anims.animator.transform.SetLocalEuler(y: 180);
        agent.isStopped = false;
        agent.speed = speed.value;
        agent.SetDestination(newLoc);
        anims.Animate(animName);
    }


}

[Serializable]
public class RunToHideout : ActionAI
{
    [TabGroup("Run To Hideout")] public string animName;
    [TabGroup("Run To Hideout")][ReadOnly] public List<Transform> hideoutLocs;
    [TabGroup("Run To Hideout")][SerializeField] Deviatable speed;
    [TabGroup("Run To Hideout")] public Animatable anims;

    [Button]
    public override void ExecuteImplement()
    {
        agent.isStopped = false;
        agent.speed = speed.value;
        if (agent.Has(out ConstantLookAt c)) c.looking = false;
        anims.animator.transform.SetLocalEuler(y: 0);
        if (hideoutLocs.Count > 0)
            agent.SetDestination(agent.transform.GetClosest(hideoutLocs).position);
        anims.Animate(animName);
    }


}

[Serializable]
public class MoveToObject : ActionAI
{
    [TabGroup("Move to Object")][SerializeField, ReadOnly] Transform loc;
    [TabGroup("Move to Object")][SerializeField] float closeRange = 1f;
    [TabGroup("Move to Object")][SerializeField] float rotSpeed = 10f;
    [TabGroup("Move to Object")][ShowInInspector, ReadOnly] bool toClose;
    [TabGroup("Move to Object")][SerializeField] Animatable moveAnims;
    [TabGroup("Move to Object")][SerializeField] BunnyEat eat;
    [TabGroup("Move to Object")][SerializeField] string anim_move = "move";
    [TabGroup("Move to Object")][SerializeField] string anim_idle = "idle";


    public override void ExecuteImplement()
    {
        if (!loc) return;

        if (ToClose())
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            return;
        }

        agent.updateRotation = true;
        if (!eat.eating)
        {
            toClose = false;
            agent.isStopped = false;
            agent.SetDestination(loc.transform.position);
            if(!moveAnims.animator.IsPlaying(anim_move)) moveAnims.CrossFade(anim_move);
        }
        else
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

    }

    public void GiveData(Transform _loc) => loc = _loc;


    bool ToClose()
    {
        float dist = Vector3.Distance(agent.transform.position, loc.position);

        if (dist < agent.stoppingDistance + closeRange)
        {
            toClose = true;
            this.Log("to close");
            agent.isStopped = true;
            agent.updateRotation = false;
            
            if(!eat.eating && !moveAnims.animator.IsPlaying(anim_move))
            {
                moveAnims.CrossFade(anim_idle);
                this.Log("idling");
            }


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
    [TabGroup("Die")][SerializeField] ConstantLookAt looker;
    public override void ExecuteImplement()
    {
        looker.looking = false;
        if(agent.isOnNavMesh)
            agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.enabled = false;
    }
}
