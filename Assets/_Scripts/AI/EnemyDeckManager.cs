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
        float waitTime = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(waitTime);

        int health = _healthScript.statsObject.Health;
        int max = _healthScript.statsObject.MaxHealth;

        //check health
        if (health < max - (health / 3))
        {
            int healAmount = Random.Range(1, 10);

            int randomInt = Random.Range(0, 2);

            if(randomInt == 0)
            {
                Heal(healAmount);
            }
            else if(randomInt == 1)
            {
                characterAnimator.SpellAnimation();

                //add particle effect
                Instantiate(attackParticles, new Vector3(
                    transform.position.x,
                    transform.position.y + 1,
                    transform.position.z),
                    Quaternion.identity);

                _healthScript.AddShield(healAmount);
            }
            else
            {
                Attack(healAmount);
            }
        }
        //if full health
        else
        {
            int damageAmount = Random.Range(1, 10);
            Attack(damageAmount);
        }
        yield return new WaitForSeconds(waitTime);

        turnManager.ChangeTurn();
    }

    private void Attack(int remainingDamage)
    {
        characterAnimator.AttackAnimation();


        if (_playerHealth.statsObject.Defence > remainingDamage)
        {
            remainingDamage -= _playerHealth.statsObject.Defence;
            remainingDamage = 0;
        }
        else
        {
            _playerHealth.statsObject.Defence -= remainingDamage;
            _playerHealth.statsObject.Defence = 0;
        }

        _healthScript.UpdateShield(_playerHealth.statsObject.Defence);

        if(remainingDamage > 0)
        {
            _playerHealth.TakeDamageOrHeal(-remainingDamage);

            //wait to sync with animation
            StartCoroutine(Wait(0.9f));
        }
        

        Vector3 playerPosition = _playerHealth.transform.position;
        healthPopup.Create(playerPosition, remainingDamage, true);
    }

    private void Heal(int amount)
    {
        //visual effects
        characterAnimator.HealAnimation();
        Instantiate(HealParticles, new Vector3(
                    transform.position.x,
                    transform.position.y + 1,
                    transform.position.z),
                    Quaternion.identity);

        _healthScript.TakeDamageOrHeal(amount);

        healthPopup.Create(transform.position, amount, false);
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
