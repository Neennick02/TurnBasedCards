using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Button[] cardButtons;
    private HealthPopup healthPopup;

    private List _deck = new List();

    [SerializeField] private Health enemyHealth;
    private Health playerHealth;


    private void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    public void EnableCards(bool active)
    {
        for (int i = 0; i < cardButtons.Length; i++)
        {
            cardButtons[i].enabled = active;
        }
    }

    public void Attack()
    {
        int amount = Random.Range(10, 20);

        Debug.Log(gameObject.name + "is attacking " + amount + "points");

        enemyHealth.TakeDamageOrHeal(-amount);

        healthPopup = enemyHealth.GetComponent<HealthPopup>();
        healthPopup.Create(healthPopup.transform.position, amount, true);
    }

    public void Heal()
    {
        int amount = Random.Range(10, 20);
        Debug.Log(gameObject.name + "is healing " + amount + "points");

        playerHealth.TakeDamageOrHeal(amount);
        healthPopup = GetComponent<HealthPopup>();
        healthPopup.Create(transform.position, amount, false);
    }
}
