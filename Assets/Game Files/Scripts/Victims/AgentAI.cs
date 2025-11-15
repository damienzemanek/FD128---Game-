using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    [SerializeReference] public ActionAI currentAction;
    [SerializeReference, ReadOnly] public ActionAI prevAction;

    [SerializeReference] public List<BeliefAI> beliefs;
    [SerializeReference] public List<DetectorAI> detectors;

    public void FixedUpdate()
    {
        ExecuteNewAction();
    }

    void ExecuteNewAction()
    {
        currentAction = GetHighestPriorityUsableBelief().immediateAction;
        currentAction?.Execute();
        this.Log($"attempting execute : {currentAction?.GetType()}");
        if (prevAction != null && prevAction != currentAction) //New action
        {
            prevAction.inUse = false;
        }
        prevAction = currentAction;
    }

    BeliefAI GetHighestPriorityUsableBelief()
    {
        int highestPriority = 0;
        BeliefAI highestBelief = null;

        foreach(BeliefAI belief in beliefs)
        {
            if (!belief.usable) continue;
            
            if(belief.priority > highestPriority)
            {
                highestPriority = belief.priority;
                highestBelief = belief;
            }
        }

        return highestBelief;
    }

    public bool HasBelief(BeliefAI _belief, out BeliefAI retBeleif)
    {
        retBeleif = null;
        retBeleif = beliefs.FirstOrDefault(b => b.GetType() == _belief.GetType());
        return (retBeleif != null);
    }

}
