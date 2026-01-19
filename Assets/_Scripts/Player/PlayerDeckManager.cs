using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    private Health playerHealth;
    private HealthPopup healthPopup;


    [Header("Script Links")]
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private HandManager handManager;
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private CharacterAnimator animator;

    [Header("Particle Effects")]
    [SerializeField] private GameObject healParticles, shieldParticles, attackParticles;

    private bool cardsDrawn;
    private void Start()
    {
        playerHealth = GetComponent<Health>();
        healthPopup = enemyHealth.GetComponent<HealthPopup>();
    }

    private void Update()
    {

        if (turnManager.CurrentPlayer == TurnManager.ActivePlayer.Ai) cardsDrawn = false;
        else
        {
            if (!cardsDrawn)
            {
                //draw cards
                handManager.DrawCards();
                cardsDrawn = true;

                //use waitList
                ProcessEffectOverTime();
            }

            //check mouse hover 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && hit.transform.CompareTag("Card"))
            {
                //find scriptable object
                CardScriptableObject currentCard = hit.transform.GetComponent<CardDisplay>().card;

                //on left click use card
                if (Input.GetMouseButtonDown(0))
                { 
                    UseCard(currentCard, hit);
                }
                //on right click discard card
                else if(Input.GetMouseButton(1))
                {
                    RemoveCard(hit);
                }
            }
        }
    }

    private void UseCard(CardScriptableObject card, RaycastHit hit)
    {
        //check mana 
        if (card.manaCost > manaManager.stats.CurrentMana) return;

        manaManager.DrainMana(card.manaCost);

        int effectAmount = 0; //amount used if card is active for > 1 round


        //first card effect
        switch (card.type1) 
        {
            case CardScriptableObject.Type.Damage:
                Attack(card.attack);
                effectAmount = card.attack;

                break;
            case CardScriptableObject.Type.Heal:
                Heal(card.heal);
                effectAmount = card.heal;
                break;

            case CardScriptableObject.Type.Defend:
                Defend(card.defend);
                effectAmount = card.defend;
                break;
        }

        //second effect
        switch (card.type2)
        {
            case CardScriptableObject.Type.None:
                break;
            case CardScriptableObject.Type.Damage:
                Attack(card.attack);

                break;
            case CardScriptableObject.Type.Heal:
                Heal(card.heal);
                break;
            case CardScriptableObject.Type.Defend:
                Defend(card.defend);
                break;
        }

        //check if card is used in more than 1 round
        if(card.turns > 1)
        {
            AddDamageOverTime(effectAmount , card.turns - 1, card.type1);
        }

        //removes card from hand
        RemoveCard(hit);
    }

    public void ProcessEffectOverTime()
    {
        //loop over effects que
        for (int i = enemyHealth.activeDotEffects.Count - 1; i >= 0; i--)
        {
            OverTimeEffect dot = enemyHealth.activeDotEffects[i].GetComponent<OverTimeEffect>();
            GameObject instance = enemyHealth.activeDotEffects[i];

            //apply damage
            switch (dot.Type) 
            {
                case OverTimeEffect.EffectType.Attack: 
                    Attack(dot.EffectAmountPerTurn);
                    break;

                case OverTimeEffect.EffectType.Heal: 
                    Heal(dot.EffectAmountPerTurn);
                    break;

                case OverTimeEffect.EffectType.Defend: 
                    playerHealth.AddShield(dot.EffectAmountPerTurn); 
                    break;
            }
            //use turn
            dot.remainingTurns--;

            //remove from list
            if (dot.remainingTurns <= 0)
            {
                enemyHealth.activeDotEffects.RemoveAt(i);
                Destroy(instance);
            }
        }
    }

    private void AddDamageOverTime(int amount, int turns, CardScriptableObject.Type type)
    {
        GameObject dotInstance = Instantiate(dotPrefab, transform.position, transform.rotation);
        OverTimeEffect dot = dotInstance.GetComponent<OverTimeEffect>();
        dot.EffectAmountPerTurn = amount;
        dot.remainingTurns = turns;

        switch(type) //check the effect type
        {
            case CardScriptableObject.Type.Damage:
                dot.Type = OverTimeEffect.EffectType.Attack;
                //add to enemy list
                enemyHealth.activeDotEffects.Add(dotInstance);
                break;

                case CardScriptableObject.Type.Defend:
                dot.Type = OverTimeEffect.EffectType.Defend;
                //add to player list
                enemyHealth.activeDotEffects.Add(dotInstance);
                break;

                case CardScriptableObject.Type.Heal:
                dot.Type = OverTimeEffect.EffectType.Heal;
                //add to player list
                enemyHealth.activeDotEffects.Add(dotInstance);
                break;
        }
    }

    private void RemoveCard(RaycastHit hit)
    {
        //remove card from array
        handManager.handCards.Remove(hit.transform.gameObject);
        Destroy(hit.transform.gameObject);

        //update hand
        handManager.UpdateCardPositions();
    }
    private void Attack(int damage)
    {
        //play animation
        animator.AttackAnimation();

        int remainingDamage = damage;

        int absorbed = Mathf.Min(enemyHealth.currentDefence, remainingDamage);
        playerHealth.currentDefence -= absorbed;
        remainingDamage -= absorbed;

        enemyHealth.UpdateShield(enemyHealth.currentDefence);

        //if there is damage remaining
        if (remainingDamage > 0)
        {
            enemyHealth.TakeDamage(-remainingDamage);

            //wait to sync with animation
            StartCoroutine(AttackParticlesRoutine(0.9f));
        }

        //create damage popup
        Vector3 playerPosition = enemyHealth.transform.position;
        healthPopup.Create(playerPosition, damage, true);
    }



    public void Heal(int healAmount)
    {
        animator.HealAnimation();
        Instantiate(healParticles, transform);

        playerHealth.Heal(healAmount);
        healthPopup = GetComponent<HealthPopup>();
        healthPopup.Create(transform.position, healAmount, false);
    }

    public void Defend(int amount)
    {
        animator.SpellAnimation();
        Instantiate(shieldParticles, new Vector3(
            transform.position.x, 
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);

        playerHealth.statsObjects[enemyHealth.currentEnemy].Defence += amount;
        playerHealth.UpdateShield(playerHealth.statsObjects[enemyHealth.currentEnemy].Defence);
    }

    IEnumerator AttackParticlesRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        //add particles
        Instantiate(attackParticles, new Vector3(
          enemyHealth.transform.position.x,
          enemyHealth.transform.position.y + 1,
           enemyHealth.transform.position.z), Quaternion.identity);
    }
}
