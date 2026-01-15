using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAnimator : MonoBehaviour
{
    public List<Animator> Animators = new List<Animator>();
    public List<GameObject> CharacterModels = new List<GameObject>();

    private int modelCount;

    private void Start()
    {
        EnableNextCharacter(0);
    }

    public void SwitchCharacter(int count)
    {
        switch (count) 
        {
            case 0:
                EnableNextCharacter(0);
                break;
            case 1:
                EnableNextCharacter(1);
                break;
            case 2:
                EnableNextCharacter(2);
                break;
        }

    }

    private void EnableNextCharacter(int current)
    {
        modelCount = current;   
        for (int i = 0; i < CharacterModels.Count; i++)
        {
            CharacterModels[i].SetActive(false);
            CharacterModels[current].SetActive(true);
        }
    }

    public void AttackAnimation()
    {
        ActivateAnimation("IsAttacking", modelCount);
    }

    public void HealAnimation()
    {
        ActivateAnimation("IsHealing", modelCount);
    }

    public void SpellAnimation()
    {
        ActivateAnimation("IsCasting", modelCount);
    }

    public void DeathAnimation()
    {
        ActivateAnimation("IsDead", modelCount);
    }

    private void ActivateAnimation(string name, int currentAnimator)
    {
        StopCoroutine(WaitForAnimation());
        Animators[currentAnimator].SetBool(name, true);
        StartCoroutine(WaitForAnimation());
    }

    public void ResetBools()
    {
        Animators[modelCount].SetBool("IsAttacking", false);
        Animators[modelCount].SetBool("IsHealing", false);
        Animators[modelCount].SetBool("IsCasting", false);
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        ResetBools();
    }
}
