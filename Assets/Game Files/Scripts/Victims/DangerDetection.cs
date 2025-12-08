using Extensions;
using UnityEngine;
using Extensions;
using static Extensions.ColliderExtensions;

public class DangerDetection : DetectorAI
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.TagIs("Player")) return;
        GetIsSafe()?.Set(false);
        GetIsInDanger()?.Set(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TagIs("Player")) return;

        GetIsSafe()?.Set(true);
        GetIsInDanger()?.Set(false);
        GetCanSeeHideout()?.Set(false, null);
    }
}
