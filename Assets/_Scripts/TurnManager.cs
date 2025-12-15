using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _activePlayer;
    Turns currentTurn;
    private int _playerAmount;


    [SerializeField] private DeckManager playerDeck;
    [SerializeField] private Button _nextTurnButton;


    [SerializeField] private List<EnemyDeck> _enemyDecks = new List<EnemyDeck>();
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
        //change turn
        currentTurn++;

        //if last players turn return to first player
        if (currentTurn > Turns.Opponent3)
        {
            currentTurn = Turns.Player;
        }

        //if players turn enable deck
        if(currentTurn == Turns.Player)
        {
            EnablePlayerCards(true);
        }
        else
        {
            EnablePlayerCards(false);
        }


        switch (currentTurn)
        {
            case Turns.Opponent1:
                _enemyDecks[0].UseTurn();
                break;

            case Turns.Opponent2:
                _enemyDecks[1].UseTurn();
                break;

            case Turns.Opponent3:
                _enemyDecks[2].UseTurn();
                break;
        }
    }

    private void Update()
    {
        _activePlayer.text = "Current player : " +currentTurn.ToString();

    }
    

    private void EnablePlayerCards(bool active)
    {
        playerDeck.EnableCards(active);
        _nextTurnButton.enabled = active;
    }
}
