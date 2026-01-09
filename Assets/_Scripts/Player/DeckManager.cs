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

    [SerializeField] private TurnManager turnManager;
    [SerializeField] private HandManager handManager;

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
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("card used");

                    //find scriptable object
                    CardScriptableObject currentCard = hit.transform.GetComponent<CardDisplay>().card;

                    UseTurn(currentCard);

                    //remove card from array
                    handManager.handCards.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);

                    //update hand
                    handManager.UpdateCardPositions();
                }
            }
        }
    }

    private void UseTurn(CardScriptableObject card)
    {
        //first card effect
        if(card.type1 == CardScriptableObject.Type.Damage)
        {
            Attack(card.attack);
        }
        else if(card.type1 == CardScriptableObject.Type.Heal)
        {
            Heal(card.heal);
        }
        else if(card.type1 == CardScriptableObject.Type.Defend)
        {
            Defend(card.defend);
        }

        //second effect
        if (card.type2 == CardScriptableObject.Type.None) return;

        if (card.type2 == CardScriptableObject.Type.Damage)
        {
            Attack(card.attack);
        }
        else if (card.type2 == CardScriptableObject.Type.Heal)
        {
            Heal(card.heal);
        }
        else if (card.type2 == CardScriptableObject.Type.Defend)
        {
            Defend(card.defend);
        }

    }

    public void EnableCards(bool active)
    {
        for (int i = 0; i < cardButtons.Length; i++)
        {
            cardButtons[i].enabled = active;
        }
    }

    public void Attack(int amount)
    {

        Debug.Log(gameObject.name + " is attacking " + amount + " points");

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
