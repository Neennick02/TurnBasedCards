using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private Health _healthScript;
    private HealthPopup healthPopup;

    [SerializeField] private Health _playerHealth;

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
        float waitTime = Random.Range(0.5f, 3f);
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
                AddShield(healAmount);
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
        turnManager.ChangeTurn();
    }

    private void Attack(int amount)
    {
        Debug.Log(gameObject.name + "is attacking " + amount + "points");

        if(_playerHealth.shieldAmount > 0)
        {
            amount -= _playerHealth.shieldAmount;
        }

        _playerHealth.TakeDamageOrHeal(-amount);

        Vector3 playerPosition = _playerHealth.transform.position;
        healthPopup.Create(playerPosition, amount, true);
    }

    private void Heal(int amount)
    {
        Debug.Log(gameObject.name + "is healing " + amount + "points");

        _healthScript.TakeDamageOrHeal(amount);

        healthPopup.Create(transform.position, amount, false);
    }

    private void AddShield(int amount)
    {
        _healthScript.shieldAmount += amount;
    }
}
