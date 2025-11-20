using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using static Extensions.AnimEX;

public class Health : MonoBehaviour
{
    [TitleGroup("Parameters")]
    [SerializeField] float maxHp;
    [SerializeField, ReadOnly] float currentHp;

    [TitleGroup("Parameters")]
    [SerializeField] float speedUpForXSecondsOnHit = 0.8f;
    [SerializeField] float speedIncreaseOnHit = 2f;


    [TitleGroup("Effects")] 
    [SerializeField] EffectUser hitEffect;
    [SerializeField] EffectUser dieEffect;

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
    }

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    public void TakeDmg(float amount)
    {
        this.Log("Ive been hit");
        currentHp -= amount;

        if (IsDead()) Die();
        else Hit();
    }

    void Hit()
    {
        hitEffect.UseEffect(this);
        anims.Animate(hitAnimName, layer: 1);
        StartCoroutine(SpeedUpForATime());
    }

    bool IsDead()
    {
        if (currentHp <= 0) return true;
        return false;
    }

    void Die()
    {
        StopAllCoroutines();
        deadDetector.Die();
        anims.Animate(deathAnimName, this, GorePileSelf);
        dieEffect.UseEffect(this);
        looker.looking = false;
    }

    void GorePileSelf()
    {
        bodyRef.SetActive(false);
        gorePileRef.SetActive(true);
        gorePileRef.transform.SetParent(null);
        gameObject.SetActive(false);
    }


    IEnumerator SpeedUpForATime()
    {
        agent.speed += speedIncreaseOnHit;
        yield return new WaitForSeconds(speedUpForXSecondsOnHit);
        agent.speed -= speedIncreaseOnHit;
    }
}
