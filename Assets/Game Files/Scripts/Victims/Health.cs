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


public class Health : MonoBehaviour, IHittable
{
    [TitleGroup("Parameters")]
    [SerializeField] int maxHp;
    [field: SerializeField] [field: ReadOnly] public int hp { get; set; }
    [SerializeField] float speedUpForXSecondsOnHit = 0.8f;
    [SerializeField] float speedIncreaseOnHit = 2f;
    float baseSpeed;
    [field:SerializeField] public Hittable hittable { get; set; }
    public float lastHitTime { get; set; }
    public bool cannotHit { get; set; }

    [TitleGroup("Effects")] 
    [SerializeField] EffectUser hitEffect;
    [SerializeField] EffectUser dieEffect;
    [SerializeField] EffectObjectRagdoll deathObjectsEffect;
    [SerializeField] Explode explosion;


    [TitleGroup("Refs")]
    [SerializeField] GameObject bodyRef;
    [SerializeField] GameObject gorePileRef;
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
        baseSpeed = agent.speed;
    }

    private void OnEnable()
    {
        agent.speed = baseSpeed;
        hp = maxHp;
    }

    void GorePileSelf()
    {
        bodyRef.SetActive(false);
        gorePileRef.SetActive(true);
        gorePileRef.transform.SetParent(null);
        gameObject.SetActive(false);

        dieEffect.UseEffect();
        deathObjectsEffect.UseEffect();

        explosion.Blast();
    }


    IEnumerator SpeedUpForATime()
    {
        agent.speed += speedIncreaseOnHit;
        yield return new WaitForSeconds(speedUpForXSecondsOnHit);
        agent.speed -= speedIncreaseOnHit;
    }

    public void OnHit()
    {
        hitEffect.UseEffect();
        anims.Animate(hitAnimName, layer: 1);
        StartCoroutine(SpeedUpForATime());
    }

    [Button]
    public void OnDie()
    {
        StopAllCoroutines();
        deadDetector.Die();
        anims.Animate(deathAnimName, this, GorePileSelf);
        looker.looking = false;
    }
}
