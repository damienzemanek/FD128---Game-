using UnityEngine;

public class DangerDetection : DetectorAI
{
    private void OnTriggerStay(Collider other)
    {
        if (!RetCheck(other, out IsSafe safe, danger: out IsInDanger danger)) return;


        safe.Set(false);
        danger.Set(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!RetCheck(other, out IsSafe safe, danger: out IsInDanger danger)) return;

        safe.Set(true);
        danger.Set(false);
    }

    bool RetCheck(Collider other, out IsSafe safe, out IsInDanger danger)
    {
        safe = null;
        danger = null;

        if (other.tag != "Player") return false;

        if (agentAI.HasBelief(new IsSafe(), out BeliefAI _safe)) safe = (IsSafe)_safe;
        if (agentAI.HasBelief(_belief: new IsInDanger(), out BeliefAI _danger)) danger = (IsInDanger)_danger;

        if (safe == null || danger == null) return false;
        return true;
    }
}
