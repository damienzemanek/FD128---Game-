using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPivot : MonoBehaviour
{
    public Transform newParent;
    public Transform oldParent;

    public GameObject Obj;

    public void ChangePos(bool goToNew)
    {
        Transform parent = newParent;
        if (!goToNew) parent = oldParent;

        Obj.transform.SetParent(parent);
        Obj.transform.Lerp(Vector3.zero, 1f, this, local: true);
        Obj.transform.LerpRot(parent.rotation, 1f, this, local: true);
    }
}
