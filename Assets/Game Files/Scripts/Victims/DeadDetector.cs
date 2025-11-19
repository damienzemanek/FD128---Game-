using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDetector : DetectorAI
{
    public void Die()
    {
        if (!RetCheck(out IsDead dead)) return;

        dead.Set(true);
    }

    bool RetCheck(out IsDead dead)
    {
        dead = null;

        if (agentAI.HasBelief(new IsDead(), out BeliefAI _dead)) dead = (IsDead)_dead;

        if (dead == null) return false;
        return true;
    }
}
