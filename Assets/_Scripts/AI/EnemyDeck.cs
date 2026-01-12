using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDeck : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private Health _healthScript;
    private HealthPopup healthPopup;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private TextMeshProUGUI shieldCounter;
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

        int health = _healthScript._health;
        int max = _healthScript._maxHealth;

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
        if (_playerHealth.shieldAmount > remainingDamage)
        {
            remainingDamage -= _playerHealth.shieldAmount;
            remainingDamage = 0;
        }
        else
        {
            _playerHealth.shieldAmount -= remainingDamage;
            _playerHealth.shieldAmount = 0;
        }

        _healthScript.UpdateShield(_playerHealth.shieldAmount);

        if(remainingDamage > 0)
        _playerHealth.TakeDamageOrHeal(-remainingDamage);

        Vector3 playerPosition = _playerHealth.transform.position;
        healthPopup.Create(playerPosition, remainingDamage, true);
    }

    private void Heal(int amount)
    {
        _healthScript.TakeDamageOrHeal(amount);

        healthPopup.Create(transform.position, amount, false);
    }
}
