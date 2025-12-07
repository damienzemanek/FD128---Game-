using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Extensions;
using static Extensions.NavEX;

public class TP : MonoBehaviour
{
    public void DoTp()
    {
        Vector3 nearestNavMeshPoint = transform.position.ToNearestNavmeshPoint(10);
        NavEX.Teleport(nearestNavMeshPoint, gameObject, out bool telepoerting);
    }
}
