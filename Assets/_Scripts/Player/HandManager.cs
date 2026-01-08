using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize = 5;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SplineContainer splineContainer;


    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<GameObject> handCards = new();

    [SerializeField] Vector3 zoomScale;

    float duration = .5f;
    Transform hoveredCard = null;
    private Dictionary<Transform, Tween> cardTweens = new();
    Transform previousCard = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCards();
        }

        // Handle hover raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Transform newHoveredCard = null;

        if (Physics.Raycast(ray, out hit, 100f) && hit.transform.CompareTag("Card"))
        {
            newHoveredCard = hit.transform;
        }

        // If hovered card changed
        if (hoveredCard != newHoveredCard)
        {
            // Shrink previous hovered card
            if (hoveredCard != null)
            {
                StartCardTween(hoveredCard, Vector3.one);
            }

            hoveredCard = newHoveredCard;

            // Zoom new hovered card
            if (hoveredCard != null)
            {
                StartCardTween(hoveredCard, zoomScale);
            }
        }
    }
    private void StartCardTween(Transform card, Vector3 targetScale)
    {
        // Kill any previous tween on this card
        if (cardTweens.ContainsKey(card))
        {
            cardTweens[card].Kill();
            cardTweens.Remove(card);
        }

        // Start new tween
        Tween t = card.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
        cardTweens[card] = t;

        // Remove tween from dictionary once complete
        t.OnComplete(() => cardTweens.Remove(card));
    }

    private void DrawCards()
    {
        if (handCards.Count >= maxHandSize) return;
        GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        g.transform.SetParent(splineContainer.transform);
        handCards.Add(g);
        UpdateCardPositions();
    }
    private void UpdateCardPositions()
    {
        //check if player has cards
        if (handCards.Count == 0) return;

        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;

            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);

              Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);

            var sortingGroup = handCards[i].GetComponent<SortingGroup>();
            if (sortingGroup != null)
            {
                sortingGroup.sortingLayerName = "Cards";
                sortingGroup.sortingOrder = i;
            }
        }
    }
}
