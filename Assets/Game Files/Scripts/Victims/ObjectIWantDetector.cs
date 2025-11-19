using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectIWantDetector : DetectorAI
{
    [SerializeField] string tagImLookingFor;
    [SerializeField] bool colliding = false;
    [SerializeField, ReadOnly] Collider otherCache;

    private void OnTriggerStay(Collider other)
    {
        if (!RetCheck(other, out IsObjectIWantNearby nearby)) return;
        colliding = true;

        otherCache = other;

        nearby.Set(true, other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!RetCheck(other, out IsObjectIWantNearby nearby)) return;
        colliding = false;
        otherCache = null;

        nearby.Set(val: false, other.transform);

    }

    private void FixedUpdate()
    {
        NullCheck();
    }

    void NullCheck()
    {
        if (!colliding || otherCache != null) return;

        colliding = false;
        if (!agentAI.HasBelief(new IsObjectIWantNearby(), out BeliefAI _nearby)) return;
        IsObjectIWantNearby nearby = (IsObjectIWantNearby)_nearby;
        nearby.Set(false, null);
    }

    bool RetCheck(Collider other, out IsObjectIWantNearby nearby)
    {
        nearby = null;

        if (other.tag != tagImLookingFor) return false;

        if (agentAI.HasBelief(new IsObjectIWantNearby(), out BeliefAI _nearby)) nearby = (IsObjectIWantNearby)_nearby;

        if (nearby == null) return false;
        return true;
    }
}
