using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    public List<CardScriptableObject> cards;
    public CardScriptableObject card { get; private set;}


    [SerializeField] private TextMeshPro name;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private TextMeshPro manaCost;
    [SerializeField] private TextMeshPro damage;
    [SerializeField] private SpriteRenderer artwork;


    private void Awake()
    {
        card = cards[Random.Range(0, cards.Count)];

        name.text = card.name;
        description.text = card.description;
        manaCost.text = card.manaCost.ToString();
        damage.text = card.attack.ToString();
        artwork.sprite = card.artwork;
    }
}
