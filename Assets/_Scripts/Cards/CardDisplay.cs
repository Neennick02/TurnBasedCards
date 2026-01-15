using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardScriptableObject card { get; private set;}

    
    [SerializeField] private TextMeshPro title;
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
    DeckManager deckManager;
    HandManager handManager;

    private bool zoomed = false;

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();

        deckManager = FindFirstObjectByType<DeckManager>();
        handManager = FindFirstObjectByType<HandManager>();

        if( deckManager != null)
        {
            SetCardData();
        }
    }

    private void SetCardData()
    {
            //find random card
            card = deckManager._RemainingDeckList[Random.Range(0, deckManager._RemainingDeckList.Count)];

            //remove card from list
            deckManager.RemoveCardFromDeck(card);

            //add data onto card
            title.text = card.name;
            description.text = card.description;
            manaCost.text = card.manaCost.ToString();
            damage.text = card.attack.ToString();
            artwork.sprite = card.artwork;
            cardColor.sprite = card.cardAppearance;
    }

    public void SetCustomData(CardScriptableObject so)
    {
        title.text = so.name;
        description.text = so.description;
        manaCost.text = so.manaCost.ToString();
        damage.text = so.attack.ToString();
        artwork.sprite = so.artwork;
        cardColor.sprite = so.cardAppearance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!zoomed)
        {
            originalScale = transform.localScale;
            originalPos = transform.localPosition;

            transform.localScale = originalScale * hoverScale;
            transform.localPosition = new Vector3(originalPos.x, originalPos.y + yOffset, originalPos.z);


            sortingGroup.sortingOrder += 100;
            zoomed = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (zoomed)
        {
            transform.localScale = originalScale;
            transform.localPosition = originalPos;

            sortingGroup.sortingOrder -= 100;
            handManager.UpdateCardPositions();
            zoomed = false;
        }
    }
}
