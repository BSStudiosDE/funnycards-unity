using TMPro;
using UnityEngine;

public class Pet : MonoBehaviour
{

    public void CreatePet(CardValue petValue)
    {
        // Set the pet's properties
        gameObject.name = petValue.name;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        if (petValue.image != null) spriteRenderer.sprite = petValue.image;

        // Create and set up the name text
        GameObject nameTextObject = new GameObject("NameText");
        nameTextObject.transform.SetParent(transform);
        TextMeshPro nameText = nameTextObject.AddComponent<TextMeshPro>();

        nameText.text = petValue.name;
        nameText.fontSize = 35;
        nameText.alignment = TextAlignmentOptions.Center;
        nameTextObject.transform.localPosition = new Vector3(0, 0.7f, 0); // Position above the pet

        // Create and set up the health text
        GameObject healthTextObject = new GameObject("HealthText");
        healthTextObject.transform.SetParent(transform);
        TextMeshPro healthText = healthTextObject.AddComponent<TextMeshPro>();

        healthText.text = "Health: " + petValue.health;
        healthText.fontSize = 35;
        healthText.alignment = TextAlignmentOptions.Center;
        healthTextObject.transform.localPosition = new Vector3(0, -0.7f, 0); // Position below the pet
    }
}
