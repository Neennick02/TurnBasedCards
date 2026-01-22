using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float scrollSpeed;
    private void Update()
    {
        if (Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0)
        {
            float newY = transform.position.y - (Input.mouseScrollDelta.y * moveSpeed);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + scrollSpeed,
                transform.position.z);
        }

    }
}
