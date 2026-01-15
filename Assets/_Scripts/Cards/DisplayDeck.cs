using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDeck : MonoBehaviour
{
    public List<CardScriptableObject> cards;
    public GameObject cardPrefab;

    public float xOffSet = 5;
    public float yOffSet = 5;

    public int colomnCount = 3;

    Rigidbody rb;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            int column = i % colomnCount;
            int row = i / colomnCount;

            float xPos = transform.position.x + (column * xOffSet);
            float yPos = transform.position.y - (row * yOffSet);

            Vector3 position = new Vector3(xPos , yPos, transform.position.z);

            GameObject go = Instantiate(cardPrefab, position, Quaternion.identity);
            go.transform.SetParent(transform);
            CardDisplay cardDataScript = go.GetComponent<CardDisplay>();

            CardScriptableObject cardSO = cards[i];

            cardDataScript.SetCustomData(cardSO);
        }

        rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(0, 3, 0), ForceMode.Force);
    }

    private void Update()
    {
        float newY = transform.position.y - (Input.mouseScrollDelta.y * moveSpeed);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
