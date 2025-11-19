using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
    [SerializeField] float timeLeftUsing;

    public void Use()
    {
        timeLeftUsing -= Time.deltaTime;
        if (timeLeftUsing < 0) Destroy(gameObject);
    }



}
