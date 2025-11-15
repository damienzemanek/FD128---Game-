using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField, ReadOnly] float currentHp;
    [TitleGroup("Refs")] [SerializeField] EffectUser hitEffect;
    [TitleGroup("Refs")][SerializeField] EffectUser dieEffect;

    [TitleGroup("Refs")][SerializeField] GameObject bodyRef;
    [TitleGroup("Refs")][SerializeField] GameObject gorePileRef;
    [TitleGroup("Refs")][SerializeField] ConstantLookAt looker;
    [TitleGroup("Refs")][SerializeField] DeadDetector deadDetector;



    [TitleGroup("Anims")] [SerializeField] AnimationController anims;
    [TitleGroup("Anims")] [SerializeField] string deathAnimName;

    private void Awake()
    {
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
    }

    bool IsDead()
    {
        if (currentHp <= 0) return true;
        return false;
    }

    void Die()
    {
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
}
