using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardScriptableObject> CompleteDeckList;
    public List<CardScriptableObject> _RemainingDeckList;

    private void Start()
    {
        _RemainingDeckList = new List<CardScriptableObject> (CompleteDeckList);
    }
    public void RemoveCardFromDeck(CardScriptableObject card)
    {
        _RemainingDeckList.Remove(card);

        if(_RemainingDeckList.Count <= 1)
        {
            _RemainingDeckList = new List<CardScriptableObject>(CompleteDeckList);
        }
    }
}
