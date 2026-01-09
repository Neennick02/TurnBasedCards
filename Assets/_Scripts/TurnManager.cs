using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _activePlayer;
    [SerializeField] private ManaManager _manaManager;
    public ActivePlayer CurrentPlayer { get; private set; }

    public enum ActivePlayer
    {
        Player,
        Ai
    }

    [SerializeField] private PlayerDeckManager playerDeck;
    [SerializeField] private Button _nextTurnButton;


    [SerializeField] private EnemyDeck _enemyDeck;

    public int roundCounter { get; private set; }

    private void Start()
    {
        //players turn starts
        CurrentPlayer = ActivePlayer.Player;
        roundCounter = 0;
    }
    public void ChangeTurn()
    {
        //change turn
        if (CurrentPlayer == ActivePlayer.Player) CurrentPlayer = ActivePlayer.Ai;
        else CurrentPlayer = ActivePlayer.Player;


        //if players turn enable deck
        if (CurrentPlayer == ActivePlayer.Player)
        {
            roundCounter++;
            _manaManager.SetManaAmount(roundCounter);
        }


        if(CurrentPlayer == ActivePlayer.Ai)
        {
            _enemyDeck.UseTurn();
        }
    }

    private void Update()
    {        
        _activePlayer.text = "Current player : " + CurrentPlayer.ToString();
    }
}
