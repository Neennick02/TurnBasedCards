using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDeckManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private CharacterAnimator characterAnimator;

    private Health _healthScript;
    private HealthPopup healthPopup;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private TextMeshProUGUI shieldCounter;

    [SerializeField] private GameObject HealParticles, DefendParticles, attackParticles;
    private void Start()
    {
        _healthScript = GetComponent<Health>();
        healthPopup = GetComponent<HealthPopup>();
    }


    public void UseTurn()
    {
        StartCoroutine(AiTurn());
    }

    private IEnumerator AiTurn()
    {
        //wait before starting turn
        float waitTime = Random.Range(1f, 3f);
        
        yield return new WaitForSeconds(waitTime);

        //get health values from healthScript
        int health = _healthScript.currentHealth;
        int max = _healthScript.statsObjects[_healthScript.currentEnemy].MaxHealth;

        float healthPercent = (float)health / max;

        //calculate random effect amount
        int amount = Random.Range(3, 8);

        //calculate decision int
        int random = Random.Range(1, 3);

        //check health amount
        switch (healthPercent)
        {
            case var expression when healthPercent > .8:// if higher that 80%
                Attack(amount); //always attack
                break;

            case var expression when healthPercent < .8 && healthPercent > .5:// if between 50% & 80%
                if (random == 1) Attack(amount); //random decision (33%)
                if(random == 2) Heal(amount);
                if(random == 3) Defend(amount);
                break;

            case var expression when healthPercent < .5 && healthPercent > .2:// if between 20% & 50%

                if (random >= 1 && random <= 2) Heal(amount); //heal 66% of the time
                else Defend(amount);                          //defend 33%
                break;

            case var expression when healthPercent < 0.2f && healthPercent > 0.0f:// if between 0% & 20%
                Heal(amount); //always heal
                break;
        }

        yield return new WaitForSeconds(waitTime);

        turnManager.ChangeTurn();
    }

    private void Attack(int damage)
    {
        //play animation
        characterAnimator.AttackAnimation();

        int remainingDamage = damage;

        int absorbed = Mathf.Min(_playerHealth.currentDefence, remainingDamage);
        _playerHealth.currentDefence -= absorbed;
        remainingDamage -= absorbed;

        _healthScript.UpdateShield(_playerHealth.currentDefence);

        //if there is damage remaining
        if(remainingDamage > 0)
        {
            _playerHealth.TakeDamage(remainingDamage);

            //wait to sync with animation
            StartCoroutine(Wait(0.9f));
        }
        
        //create damage popup
        Vector3 playerPosition = _playerHealth.transform.position;
        healthPopup.Create(playerPosition, damage, true);
    }

    private void Heal(int amount)
    {
        //visual effects
        characterAnimator.HealAnimation();
        Instantiate(HealParticles, transform);

        _healthScript.Heal(amount);

        healthPopup.Create(transform.position, amount, false);
    }

    private void Defend(int amount)
    {
        _healthScript.AddShield(amount);
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        //add particles
        Instantiate(attackParticles, new Vector3(
          _playerHealth.transform.position.x,
          _playerHealth.transform.position.y + 1,
          _playerHealth.transform.position.z), Quaternion.identity);
    }
}
