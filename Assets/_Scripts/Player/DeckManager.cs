using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Button[] cardButtons;

    private List _deck = new List();


    


    private void Start()
    {
        
    }

    public void EnableCards(bool active)
    {
        for (int i = 0; i < cardButtons.Length; i++)
        {
            cardButtons[i].enabled = active;
        }
    }
}
