using TMPro;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public CardValue cardValue; // The card value associated with this pet
    private TextMeshPro _healthText;
    public void CreatePet(CardValue petValue)
    {
        Debug.Log($"Pet.CreatePet: Creating pet {petValue.name}...");
        // Set the pet's properties
        cardValue = petValue;
        gameObject.name = petValue.name;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        GameObject controllerObject = GameObject.Find("Controller"); // Find the Controller GameObject
        if (controllerObject != null)
        {
            Controller controller = controllerObject.GetComponent<Controller>();
            if (controller != null)
            {
                Debug.Log($"Pet.CreatePet: Loading image: {petValue.imageName}");
                cardValue.image = controller.cardValues.GetPetSprite(cardValue.imageName);
                Debug.Log($"Pet.CreatePet: Image loaded: {(cardValue.image != null ? "Yes" : "No")}"); // Check if null
                if(cardValue.image != null)
                    spriteRenderer.sprite = cardValue.image;
                else
                {
                    Debug.LogWarning("Pet.CreatePet: cardValue.image is null, the missing sprite texture will be displayed.");
                }
            }
            else
            {
                Debug.LogError("Pet.CreatePet: Controller component not found on 'Controller' game object.");
            }
        }
        else
        {
            Debug.LogError("Pet.CreatePet: 'Controller' game object not found in scene.");
        }

        TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("Funnypixel_1");
        if (fontAsset == null)
        {
            Debug.LogError("Pet.CreatePet: Failed to load font Funnypixel_1 SDF");
        }
        
        // Create and set up the name text
        var nameTextObject = new GameObject("NameText");
        nameTextObject.transform.SetParent(transform);
        var nameText = nameTextObject.AddComponent<TextMeshPro>();
        if(fontAsset != null)
            nameText.font = fontAsset;
        nameText.text = petValue.name;
        nameText.fontSize = 2;
        nameText.alignment = TextAlignmentOptions.Center;
        nameTextObject.transform.localPosition = new Vector3(0, 0.5f, 0); // Position above the pet

        // Create and set up the health text
        var healthTextObject = new GameObject("HealthText");
        healthTextObject.transform.SetParent(transform);
        _healthText = healthTextObject.AddComponent<TextMeshPro>();
        if(fontAsset != null)
            _healthText.font = fontAsset;
        _healthText.text = "Health: " + petValue.health;
        _healthText.fontSize = 2;
        _healthText.alignment = TextAlignmentOptions.Center;
        healthTextObject.transform.localPosition = new Vector3(0, -0.5f, 0); // Position below the pet

        //tag the game object as a pet
        gameObject.tag = "Pet";
        Debug.Log($"Pet.CreatePet: Pet {petValue.name} created.");
    }

    

    public void ChangeHealth(float amount)
    {
        Debug.Log($"Pet.ChangeHealth: Changing health of {cardValue.name} by {amount}.");
        cardValue.health += amount;
        if (cardValue.health <= 0)
        {
            cardValue.health = 0;
        }
        Debug.Log($"Pet.ChangeHealth: {cardValue.name}'s health is now {cardValue.health}.");
    }
    
    private void OnMouseDown()
    {
        Debug.Log($"{cardValue.name} was clicked.");
        Controller controller = FindObjectOfType<Controller>();
        controller.PetClicked(this, 0);
        Debug.Log("Finished.");
    }
}