using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    private int maxHandSize = 5;
    [SerializeField] private GameObject cardPrefab;

    private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] public List<GameObject> handCards = new();

    
    private void Start()
    {
        splineContainer = GetComponentInChildren<SplineContainer>();
    }
    private void Update()
    {

        // Handle hover raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Transform newHoveredCard = null;

        //check for card
        if (Physics.Raycast(ray, out hit, 100f) && hit.transform.CompareTag("Card"))
        {
            newHoveredCard = hit.transform;
        }
    }
    public void DrawCards()
    {
        //picks up cards
        if (handCards.Count >= maxHandSize) return;

        StartCoroutine(DrawCardsRoutine());
    }

    private IEnumerator DrawCardsRoutine()
    {
        while (handCards.Count < maxHandSize)
        {
            GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
            g.transform.SetParent(splineContainer.transform);

            //add to hand list
            handCards.Add(g);

            //update spacing
            UpdateCardPositions();

            //wait before grabbing new card
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void UpdateCardPositions()
    {
        //check if player has cards
        if (handCards.Count == 0) return;

        //calculate spacing
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;

        Spline spline = splineContainer.Spline;

        for (int i = 0; i < handCards.Count; i++)
        {
            float position = firstCardPosition + i * cardSpacing;

            //converts to world position
            Vector3 splineLocalPos = spline.EvaluatePosition(position);
            Vector3 splinePosition= splineContainer.transform.TransformPoint(splineLocalPos);
            
            // Z offset
             splinePosition += Camera.main.transform.forward * (i * -0.01f);

            Vector3 forward = spline.EvaluateTangent(position);
            Vector3 up = spline.EvaluateUpVector(position);

            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            //set card position
            handCards[i].transform.DOMove(splinePosition, .3f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, .3f);

            //update sorting group on the parent of the cards
            var sortingGroup = handCards[i].GetComponent<SortingGroup>();

            if (sortingGroup != null)
            {
                //the latest card lays on top 
                sortingGroup.sortingLayerName = "Cards";
                sortingGroup.sortingOrder = i;
            }
        }
    }
}
