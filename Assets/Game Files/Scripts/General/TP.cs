using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Extensions;
using static Extensions.NavEX;

public class TP : MonoBehaviour
{
    public float rangeToNearest = 100f;
    public void DoTp()
    {
        Vector3 nearestNavMeshPoint = transform.position.ToNearestNavmeshPoint(rangeToNearest);
        NavEX.Teleport(nearestNavMeshPoint, gameObject, out bool telepoerting);
    }
}
