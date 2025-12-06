using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using static Extensions.ColliderExtensions;
using static Entity;
using Sirenix.OdinInspector;

public class HideLocation : MonoBehaviour, IHittable
{

    private void Start()
    {
        destroyed = false;
        inUse = false;
    }

    [ReadOnly] public bool destroyed;
    [ReadOnly] public bool inUse;
    [ShowInInspector, ReadOnly] GameObject hiddenPerson;
    [field: ReadOnly] public float lastHitTime { get; set; }



    [SerializeField] GameObject hideoutObj;
    [field:SerializeField] public int hp { get; set; }
    [field: SerializeField] public Hittable hittable { get; set; }

    
    private void OnTriggerEnter(Collider other)
    {
        CheckForPerson(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckForAttacked(other);
    }

    void CheckForPerson(Collider other)
    {
        if (!other.TagIs("Person")) return;
        if (inUse) return;
        if (destroyed) return;
        Hide(other.gameObject);
    }

    void CheckForAttacked(Collider other)
    {
        if (!other.TagIs("AttackTrigger")) return;
        if (other.Get<AttackTrigger>().IsAttacking())
            this.Hit(1);
    }

    void Hide(GameObject person)
    {
        person.SetActive(false);
        inUse = true;
        hiddenPerson = person;
    }

    public void OnHit()
    {
        this.Log("Hideout hit");
    }

    public void OnDie()
    {
        inUse = false;
        destroyed = true;
        hiddenPerson.SetActive(true);
        hideoutObj.SetActive(false);
    }
}
