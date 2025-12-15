using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private Health _healthScript;

    [SerializeField] private List<Health> opponentsHealth = new List<Health>();

    private void Start()
    {
        _healthScript = GetComponent<Health>();
    }


    public void UseTurn()
    {
        if(_healthScript.ReturnHealth() < _healthScript.ReturnMaxHealth())
        {
            int healthAmount = Random.Range(10, 20);
            Heal(healthAmount);
        }
        else
        {
            int damageAmount = Random.Range(10, 20);
            Attack(damageAmount);
        }

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        turnManager.ChangeTurn();
    }

    private void Attack(int amount)
    {
        Debug.Log(gameObject.name + "is attacking " + amount + "points");

        //find enemy to attack
        Health randomHealth = opponentsHealth[Random.Range(0, opponentsHealth.Count)];

        randomHealth.TakeDamageOrHeal(-amount);
    }

    private void Heal(int amount)
    {
        Debug.Log(gameObject.name + "is healing " + amount + "points");

        _healthScript.TakeDamageOrHeal(amount);
    }
}
