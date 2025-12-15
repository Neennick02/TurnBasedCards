using System;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private string _activePlayer;
    Turns currentTurn;
    private int _playerAmount;


    [SerializeField] private DeckManager playerDeck;
    [SerializeField] private Button _nextTurnButton;

    enum Turns
    {
        Player,
        Opponent1,
        Opponent2,
        Opponent3
    }
    private void Start()
    {
        currentTurn = Turns.Player;
        _playerAmount = System.Enum.GetValues(typeof(Turns)).Length;
    }
    public void ChangeTurn()
    {
        currentTurn++;
        if (currentTurn > Turns.Opponent3)
        {
            currentTurn = Turns.Player;
        }

        if(currentTurn == Turns.Player)
        {
            EnablePlayerCards(true);
        }
        else
        {
            EnablePlayerCards(false);
        }
    }

    private void Update()
    {
        _activePlayer = currentTurn.ToString();
    }

    private void EnablePlayerCards(bool active)
    {
        playerDeck.EnableCards(active);
        _nextTurnButton.enabled = active;
    }
}
