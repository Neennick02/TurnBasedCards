using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public int FightCount {  get; private set; }
    [SerializeField] CharacterAnimator enemyAnimator;

    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private ManaManager ManaManager;
    [SerializeField] private PlayerDeckManager PlayerDeckManager;

    private TurnManager turnManager;
     private HandManager handManager;
     private DeckManager deckManager;
    private void Start()
    {
        FightCount = 0;

        handManager = GetComponent<HandManager>();
        turnManager = GetComponent<TurnManager>();
        deckManager = GetComponent<DeckManager>();
    }

    public void StartNewFight()
    {
        StartCoroutine(ResetValues());
    }

    private void ResetPlayer()
    {
        playerHealth.IncreaseHealthbarSize();
        ManaManager.ResetMana();
    }
    IEnumerator ResetValues()
    {
        yield return new WaitForSeconds(1.0f);
        FightCount++;

        //enable cards
        GameObject cardHolder = turnManager.transform.GetChild(0).gameObject;
        if (cardHolder.CompareTag("CardHolder"))
            cardHolder.SetActive(true);

        //enable new enemy model and animator
        enemyAnimator.SwitchCharacter(FightCount);

        //reset player stats
        ResetPlayer();

        //reset game values
        ResetGame();
        enemyHealth.EnableNewCharacterStats();
    }

    private void ResetGame()
    {
        //reset to turn 1
        turnManager.ResetTurns();

        //remove cards and pick new
        handManager.ResetHand();

        //refill deck
        deckManager.ResetDeck();

        //remove pending effects
        PlayerDeckManager.ResetList();
    }
}
