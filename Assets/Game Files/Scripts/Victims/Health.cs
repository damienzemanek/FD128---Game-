using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using static Extensions.AnimEX;
using static Extensions.PhysEX;
using static Effectability;
using static DelayUtility;
using static Entity;
using static Extensions.NavEX;


public class Health : MonoBehaviour, IHittable
{
    [TitleGroup("Parameters")]
    [SerializeField] int maxHp;
    [field: SerializeField] [field: ReadOnly] public int hp { get; set; }
    [SerializeField] Vector2 baseSpeedVariation;
    [SerializeField] float speedUpForXSecondsOnHit = 0.8f;
    [SerializeField] float speedIncreaseOnHit = 2f;
    [field:SerializeField] public Hittable hittable { get; set; }
    public float lastHitTime { get; set; }
    public bool cannotHit { get; set; }
    public bool hurt;

    [TitleGroup("Effects")] 
    [SerializeField] EffectUser[] hitEffect;
    [SerializeField] EffectUser[] hurtEfects;
    [SerializeField] EffectUser dieEffect;
    [SerializeField] EffectObjectRagdoll deathObjectsEffect;
    [SerializeField] Explode explosion;


    [TitleGroup("Refs")]
    [SerializeField] GameObject bodyRef;
    [SerializeField] GameObject gorePileRef;
    [SerializeField] GameObject bloodPoolRef;
    [SerializeField] ConstantLookAt looker;
    [SerializeField] DeadDetector deadDetector;
    [SerializeField] NavMeshAgent agent;

    [TitleGroup("Anims")] 
    [SerializeField] Animatable anims;
    [SerializeField] string hitAnimName;
    [SerializeField] string deathAnimName;
   

    private void Awake()
    {
        agent.Ensure(this);
        gorePileRef.SetActive(false);
    }

    private void OnEnable()
    {
        agent.speed = baseSpeedVariation.Rand();
        hp = maxHp;
    }

    void GorePileSelf()
    {
        bodyRef.SetActive(false);
        gorePileRef.SetActive(true);
        gorePileRef.transform.SetParent(null);
        if (bloodPoolRef.Has(out TP tp)) tp.DoTp();
        gameObject.SetActive(false);

        dieEffect.UseEffect();
        deathObjectsEffect.UseEffect();

        explosion.Blast();
        this.Get<Collider>().enabled = false;
    }


    IEnumerator SpeedUpForATime()
    {
        agent.speed += speedIncreaseOnHit;
        yield return new WaitForSeconds(speedUpForXSecondsOnHit);
        agent.speed -= speedIncreaseOnHit;
    }

    public void OnHit()
    {
        hitEffect.UseEffectsAll();
        anims.Animate(hitAnimName, layer: 1);
        StartCoroutine(SpeedUpForATime());
        ActivateHurtEffects();
    }

    [Button]
    public void OnDie()
    {
        StopAllCoroutines();
        deadDetector.Die();
        anims.Animate(deathAnimName, this, GorePileSelf);
        looker.looking = false;
        StartCoroutine(agent.C_Disable());
    }

    void ActivateHurtEffects()
    {
        if (hurt) return;
        hurtEfects.UseEffectsAll();
    }
}
