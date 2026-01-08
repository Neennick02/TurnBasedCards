using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    public List<CardScriptableObject> cards;
    private CardScriptableObject card;

    [SerializeField] private TextMeshPro name;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private TextMeshPro manaCost;
    [SerializeField] private TextMeshPro damage;
    [SerializeField] private SpriteRenderer artwork;


    private void Start()
    {
        card = cards[Random.Range(0, cards.Count)];

        name.text = card.name;
        description.text = card.description;
        manaCost.text = card.manaCost.ToString();
        damage.text = card.attack.ToString();
        artwork.sprite = card.artwork;
    }
}
