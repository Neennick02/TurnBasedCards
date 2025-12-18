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


    [SerializeField] private EnemyDeck _enemyDeck;
    enum Turns
    {
        Player,
        Ai
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
        if (currentTurn > Turns.Ai)
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

        if(currentTurn == Turns.Ai)
        {
            _enemyDeck.UseTurn();
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
