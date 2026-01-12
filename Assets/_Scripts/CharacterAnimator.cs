using System.Collections;
using UnityEngine;
public class CharacterAnimator : MonoBehaviour
{
     Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackAnimation()
    {
        ActivateAnimation("IsAttacking");
    }

    public void HealAnimation()
    {
        ActivateAnimation("IsHealing");
    }

    public void SpellAnimation()
    {
        ActivateAnimation("IsCasting");
    }

    public void DeathAnimation()
    {
        ActivateAnimation("IsDead");
    }

    private void ActivateAnimation(string name)
    {
        StopCoroutine(WaitForAnimation());
        animator.SetBool(name, true);
        StartCoroutine(WaitForAnimation());
    }

    public void ResetBools()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsHealing", false);
        animator.SetBool("IsCasting", false);
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        ResetBools();
    }
}
