using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public abstract class DetectorAI : MonoBehaviour
{
    [SerializeField] protected AgentAI agentAI;

    private void Awake()
    {
        agentAI.Ensure(this);
    }
}

