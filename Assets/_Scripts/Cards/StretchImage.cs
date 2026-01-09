using UnityEngine;

public class StretchImage : MonoBehaviour
{
    [SerializeField] private SpriteMask mask;

    private void Start()
    {
        Fit();
    }

    private void Fit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null || mask == null) return;

        Bounds spriteBounds = sr.bounds;
        Bounds maskBounds = mask.bounds;

        float scaleX = maskBounds.size.x / spriteBounds.size.x;
        float scaleY = maskBounds.size.y / spriteBounds.size.y;

        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}
