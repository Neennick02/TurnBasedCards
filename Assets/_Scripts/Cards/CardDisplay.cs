using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<CardScriptableObject> cards;
    public CardScriptableObject card { get; private set;}

    
    [SerializeField] private TextMeshPro name;
    [SerializeField] private TextMeshPro description;
    [SerializeField] private TextMeshPro manaCost;
    [SerializeField] private TextMeshPro damage;
    [SerializeField] private SpriteRenderer artwork;
    [SerializeField] private SpriteRenderer cardColor;

    public float hoverScale = 1.2f;
    public float yOffset = .1f;

    private Vector3 originalScale;
    private Vector3 originalPos;
    SortingGroup sortingGroup;

    private void Awake()
    {
        SetCardData();

        sortingGroup = GetComponent<SortingGroup>();
    }

    private void SetCardData()
    {
        card = cards[Random.Range(0, cards.Count)];

        name.text = card.name;
        description.text = card.description;
        manaCost.text = card.manaCost.ToString();
        damage.text = card.attack.ToString();
        artwork.sprite = card.artwork;
        cardColor.sprite = card.cardAppearance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalScale = transform.localScale;
        originalPos = transform.localPosition;

        transform.localScale = originalScale * hoverScale;
        transform.localPosition = new Vector3(originalPos.x, originalPos.y + yOffset, originalPos.z);


        sortingGroup.sortingOrder += 100;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        transform.localPosition = originalPos;



        sortingGroup.sortingOrder -= 100;

    }
}
