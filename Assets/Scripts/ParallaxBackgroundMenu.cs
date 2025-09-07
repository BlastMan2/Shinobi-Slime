using UnityEngine;
using UnityEngine.UI;


public class ParallaxBackgroundMenu : MonoBehaviour
{

    public ParallaxLayer[] layers;
    [SerializeField] private float speedMultiplier = 1f;

    private float[] tileWidths;

    private void Start()
    {
        tileWidths = new float[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i].tiles.Length < 2)
            {
                Debug.LogError("Each UIParallaxLayer needs exactly 2 tiles.");
                continue;
            }

            // Assuming both tiles are the same size
            tileWidths[i] = layers[i].tiles[0].rect.width;
        }
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            RectTransform left = layers[i].tiles[0];
            RectTransform right = layers[i].tiles[1];

            // Move both tiles leftward
            foreach (var tile in layers[i].tiles)
            {
                tile.anchoredPosition += Vector2.left * layers[i].speed * speedMultiplier * Time.unscaledDeltaTime;
            }

            // If the left tile is completely off-screen to the left, move it to the right
            if (left.anchoredPosition.x <= -tileWidths[i])
            {
                left.anchoredPosition = new Vector2(Mathf.Round(right.anchoredPosition.x + tileWidths[i]), left.anchoredPosition.y);

                // Swap references
                layers[i].tiles[0] = right;
                layers[i].tiles[1] = left;
            }
        }
    }
}
