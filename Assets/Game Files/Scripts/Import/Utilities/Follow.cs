using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] bool releaseFromParentOnSpawn;

    private void Start()
    {
        if (releaseFromParentOnSpawn)
        {
            name = name + $" [ Following {transform.parent.name} ]";
            transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        if(follow != null) transform.position = follow.position;
    }
}
