using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class FeedTrigger : MonoBehaviour
{
    [SerializeField] public GameObject parent;

    private void Awake()
    {
        parent.Ensure(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;

        Feed.Instance.Display(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        Feed.Instance.StopDisplay();
    }
}
