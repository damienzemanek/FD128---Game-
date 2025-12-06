using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class HideoutDetector : DetectorAI
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.TagIs("Hideout")) return;

        if(GetIsInDanger().isInDanger)
        {
            HideLocation hideout = GetHideLocation(other);

            if(!hideout.inUse && !hideout.destroyed)
                GetCanSeeHideout()?.Set(val: true, other.transform);
            else
                OnTriggerExit(other);
        }
        else
            OnTriggerExit(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TagIs("Hideout")) return;

        GetCanSeeHideout()?.Set(false, null);
    }

    HideLocation GetHideLocation(Collider other) => other.OptionalGet<HideLocation>();
}
