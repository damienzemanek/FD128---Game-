using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class HideoutDetector : DetectorAI
{
    private void OnTriggerStay(Collider other)
    {
        if (!IsCollidedWithPlayer(other)) return;

        GetCanSeeHideout()?.Set(val: true, other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsCollidedWithPlayer(other)) return;

        GetCanSeeHideout()?.Set(false, null);
    }
}
