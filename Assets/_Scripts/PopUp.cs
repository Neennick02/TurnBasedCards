using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    TextMeshPro text;
    private Color textColor;

    [SerializeField] private Vector3 randomOffset = new Vector3(0.5f, 0.3f, 0f);
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float fadeSpeed = 2f;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        textColor = text.color;

        Vector3 offset = new Vector3(
           Random.Range(-randomOffset.x, randomOffset.x),
           Random.Range(0f, randomOffset.y),
           Random.Range(-randomOffset.z, randomOffset.z)
       );

        transform.position += offset;
    }
    public void SetText(int amount)
    {
        text.SetText(amount.ToString());
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if(lifeTime < lifeTime / 3)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            text.color = textColor;
        }
        if (textColor.a <= 0) Destroy(gameObject);
    }
}
