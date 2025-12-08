using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public abstract class DetectorAI : MonoBehaviour
{
    [SerializeField] protected AgentAI agentAI;

    private void Awake()
    {
        agentAI.Ensure(this);
    }
    protected virtual bool CheckTag(Collider other, string tag) => (other.tag == tag);

    protected IsSafe GetIsSafe()
    {
        IsSafe safe = null;
        if (agentAI.HasBelief(new IsSafe(), out BeliefAI _safe)) safe = (IsSafe)_safe;
        return safe;
    }

    protected IsInDanger GetIsInDanger()
    {
        IsInDanger danger = null;
        if (agentAI.HasBelief(_belief: new IsInDanger(), out BeliefAI _danger)) danger = (IsInDanger)_danger;
        return danger;
    }

    protected CanSeeHideout GetCanSeeHideout()
    {
        CanSeeHideout hide = null;
        if (agentAI.HasBelief(_belief: new CanSeeHideout(), out BeliefAI _hide)) hide = (CanSeeHideout)_hide;
        return hide;
    }
}

