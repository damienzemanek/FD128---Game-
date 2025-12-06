using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using static Extensions.ColliderExtensions;
using static Entity;
using static Extensions.AnimEX;
using static Extensions.NavEX;
using static Effectability;
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
    public Animatable anims;
    public string hitAnimName = "hit";
    public EffectUser destroyEffect;

    [SerializeField] GameObject hideoutObj;
    [field:SerializeField] public int hp { get; set; }
    [field: SerializeField] public Hittable hittable { get; set; }


    private void OnTriggerEnter(Collider other)
    {
        CheckForPerson(other);
    }

    void CheckForPerson(Collider other)
    {
        if (!other.TagIs("Person")) return;
        if (inUse) return;
        if (destroyed) return;
        Hide(other.gameObject);
    }

    void Hide(GameObject person)
    {
        person.SetActive(false);
        inUse = true;
        hiddenPerson = person;
    }

    public void OnHit()
    {
        anims.Animate(hitAnimName);
        this.Log("Hideout hit");
    }

    public void OnDie()
    {
        inUse = false;
        destroyed = true;
        hiddenPerson.SetActive(true);
        hideoutObj.SetActive(false);
        destroyEffect.UseEffect();

        float myX = GetComponentInParent<Transform>().position.x;
        float myY = GetComponentInParent<Transform>().position.y;
        float personZ = hiddenPerson.transform.position.z;
        Vector3 pos = new Vector3(myX, myY, personZ);

        Teleport(pos, hiddenPerson, out bool TPing);
    }
}
