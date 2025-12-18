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
        StartCoroutine(Wait());

        if (_healthScript.ReturnHealth() < _healthScript.ReturnMaxHealth())
        {
            int healthAmount = Random.Range(10, 20);
            Heal(healthAmount);
        }
        else
        {
            int damageAmount = Random.Range(10, 20);
            Attack(damageAmount);
        }
        turnManager.ChangeTurn();
    }

    private IEnumerator Wait()
    {
        float waitTime = Random.Range(0.3f, 2f);
        yield return new WaitForSeconds(waitTime);
    }

    private void Attack(int amount)
    {
        Debug.Log(gameObject.name + "is attacking " + amount + "points");

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
}
