using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Defaults defaults;

    public GameObject cardPrefab;
    public Sprite[] cardImages;
    public string[] cardNames;
    public int columns; // Number of columns in the grid
    public float spacing;
    public float scale;

    private void Start()
    {

        scale = defaults.defaultCardScale;
        spacing = defaults.defaultCardSpacing;
        columns = 3;

        for (int i = 0; i < cardImages.Length; i++)
        {
            // Instantiate a new card
            GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);

            // Get the Card component and set its properties
            Card cardScript = card.GetComponent<Card>();
            cardScript.cardName = (cardNames != null && cardNames.Length > i) ? cardNames[i] : "Card " + i + " (automatically assigned)";
            cardScript.cardImage = cardImages[i];

            // Transform the card (position, rotation, scale)
            Transform cardTransform = card.transform;
            int row = i / columns;
            int column = i % columns;
            Vector3 position = new Vector3(column * spacing, -row * spacing, 0); // Arrange cards in a grid
            cardTransform.position = position;
            cardTransform.rotation = Quaternion.Euler(0, 0, 0);
            cardTransform.localScale = new Vector3(scale, scale, scale);

            // Log the position for debugging
            Debug.Log($"Card {i} position: {position}");
        }
    }
}
