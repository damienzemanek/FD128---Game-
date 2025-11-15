using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [TitleGroup("Parameters")][SerializeField] float maxHp;
    [TitleGroup("Parameters")][SerializeField, ReadOnly] float currentHp;

    [TitleGroup("Parameters")][SerializeField] float speedUpForXSecondsOnHit = 0.8f;
    [TitleGroup("Parameters")][SerializeField] float speedIncreaseOnHit = 2f;


    [TitleGroup("Effects")] [SerializeField] EffectUser hitEffect;
    [TitleGroup("Effects")][SerializeField] EffectUser dieEffect;

    [TitleGroup("Refs")][SerializeField] GameObject bodyRef;
    [TitleGroup("Refs")][SerializeField] GameObject gorePileRef;
    [TitleGroup("Refs")][SerializeField] ConstantLookAt looker;
    [TitleGroup("Refs")][SerializeField] DeadDetector deadDetector;
    [TitleGroup("Refs")][SerializeField] NavMeshAgent agent;

    [TitleGroup("Anims")] [SerializeField] AnimationController anims;
    [TitleGroup("Anims")] [SerializeField] string deathAnimName;

    private void Awake()
    {
        agent.Ensure(this);
        hitEffect.Ensure(this);
        dieEffect.Ensure(this);
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
        hitEffect.UseEffect();
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
        anims.AnimateThen(deathAnimName, GorePileSelf);
        dieEffect.UseEffect();
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
