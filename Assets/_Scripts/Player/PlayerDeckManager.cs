using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    private List _deck = new List();

    //health variables
    [SerializeField] private Health enemyHealth;
    private Health playerHealth;
    private HealthPopup healthPopup;


    //scripts links
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private HandManager handManager;
    [SerializeField] private ManaManager manaManager;

    private bool cardsDrawn;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {

        if (turnManager.CurrentPlayer == TurnManager.ActivePlayer.Ai) cardsDrawn = false;
        else
        {
            //draw cards
            if (!cardsDrawn)
            {
                handManager.DrawCards();
                cardsDrawn = true;
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
        if (card.manaCost > manaManager.CurrentMana) return;

        manaManager.DrainMana(card.manaCost);

        //first card effect

        switch (card.type1) 
        {
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

        //second effect
        switch (card.type1)
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

        //remove card from hand
        RemoveCard(hit);
    }

    private void RemoveCard(RaycastHit hit)
    {
        //remove card from array
        handManager.handCards.Remove(hit.transform.gameObject);
        Destroy(hit.transform.gameObject);

        //update hand
        handManager.UpdateCardPositions();
    }
    public void Attack(int amount)
    {

        Debug.Log(gameObject.name + " is attacking " + amount + " points");
        Debug.Log(playerHealth.shieldAmount);
        if(enemyHealth.shieldAmount > 0)
        {
            amount -= enemyHealth.shieldAmount;
        }

        enemyHealth.TakeDamageOrHeal(-amount);

        healthPopup = enemyHealth.GetComponent<HealthPopup>();
        healthPopup.Create(healthPopup.transform.position, amount, true);
    }

    public void Heal(int amount)
    {
        Debug.Log(gameObject.name + " is healing " + amount + " points");

        playerHealth.TakeDamageOrHeal(amount);
        healthPopup = GetComponent<HealthPopup>();
        healthPopup.Create(transform.position, amount, false);
    }

    public void Defend(int amount)
    {
        Debug.Log(gameObject.name + "is defending " + amount + " points");

    }
}
