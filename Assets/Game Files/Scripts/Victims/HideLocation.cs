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

    [TabGroup("Readonly")] [ReadOnly] public bool destroyed;
    [TabGroup("Readonly")][ReadOnly] public bool inUse;
    [field: TabGroup("Readonly")][field: ShowInInspector][field: ReadOnly] public bool cannotHit { get; set; }
    [TabGroup("Readonly"), ShowInInspector, ReadOnly] GameObject hiddenPerson;
    
    [field: TabGroup("Readonly")][field: ReadOnly] public float lastHitTime { get; set; }
    [TabGroup("Animation")] public Animatable anims;
    [TabGroup("Animation")] public string hitAnimName = "hit";
    [TabGroup("Effects")] public EffectUser destroyEffect;

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
        hideoutObj.SetActive(false);
        destroyEffect.UseEffect();

        if (hiddenPerson == null) return;

        hiddenPerson.SetActive(true);
        float myX = GetComponentInParent<Transform>().position.x;
        float myY = GetComponentInParent<Transform>().position.y;
        float personZ = hiddenPerson.transform.position.z;
        Vector3 pos = new Vector3(myX, myY, personZ);
        Teleport(pos, hiddenPerson, out bool TPing);
        hiddenPerson.Get<AgentAI>().ReExecuteCurrent(true);
    }
}
