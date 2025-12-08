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
            {
                GetCanSeeHideout()?.Set(val: true, other.transform);
                print("Can see hideout");
            }

            if (hideout.destroyed || hideout.inUse)
            {
                print("removing hideout");
                if(GetCanSeeHideout().AllHideoutsUnusable())
                {
                    OnTriggerExit(other);
                    return;
                }
                GetCanSeeHideout()?.RemoveHideout(other.transform);

            }

            if (hideout.destroyed && hideout.inUse)
                OnTriggerExit(other);

        }
        else
            OnTriggerExit(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TagIs("Hideout")) return;

        GetCanSeeHideout()?.Set(false, other.transform);
    }

    HideLocation GetHideLocation(Collider other) => other.OptionalGet<HideLocation>();
}
