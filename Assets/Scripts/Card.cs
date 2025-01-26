using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public Sprite cardImage;
    public int health;
    public int attackDamage;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro cardNameText;
    private TextMeshPro cardHealthText;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
        cardNameText = texts[0];
        cardHealthText = texts[1];

        health = 10;
        attackDamage = 5;

        if (cardNameText != null) cardNameText.text = cardName;
        if (cardHealthText != null) cardHealthText.text = "Health: " + health.ToString();

        if (cardImage != null) spriteRenderer.sprite = cardImage;

    }

    private void OnMouseDown()
    {
        if (health > 0) TakeDamage(1);
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Card \"" + cardName + "\" took " + damage + " damage! " + health + " health");
        cardHealthText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            spriteRenderer.color = Color.gray;
            Debug.Log("Card " + cardName + " has been defeated!");
        }
    }
}