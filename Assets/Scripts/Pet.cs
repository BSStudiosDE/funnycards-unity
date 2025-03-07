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
                    Debug.LogWarning("Pet.CreatePet: cardValue.image is null, no sprite will be displayed.");
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
        // Create and set up the name text
        var nameTextObject = new GameObject("NameText");
        nameTextObject.transform.SetParent(transform);
        var nameText = nameTextObject.AddComponent<TextMeshPro>();

        nameText.text = petValue.name;
        nameText.fontSize = 3;
        nameText.alignment = TextAlignmentOptions.Center;
        nameTextObject.transform.localPosition = new Vector3(0, 0.7f, 0); // Position above the pet

        // Create and set up the health text
        var healthTextObject = new GameObject("HealthText");
        healthTextObject.transform.SetParent(transform);
        _healthText = healthTextObject.AddComponent<TextMeshPro>();

        _healthText.text = "Health: " + petValue.health;
        _healthText.fontSize = 3;
        _healthText.alignment = TextAlignmentOptions.Center;
        healthTextObject.transform.localPosition = new Vector3(0, -0.7f, 0); // Position below the pet

        //tag the game object as a pet
        gameObject.tag = "Pet";
        Debug.Log($"Pet.CreatePet: Pet {petValue.name} created.");
    }

    public void ChangeHealth(float changeAmount)
    {
        Debug.Log($"Pet.ChangeHealth: Changing health of {name} by {changeAmount}.");
        cardValue.health += changeAmount;
        _healthText.text = "Health: " + cardValue.health;
        Debug.Log($"Pet.ChangeHealth: {name}'s health is now {cardValue.health}.");
    }
    private void OnMouseDown()
    {
        Debug.Log($"Pet.OnMouseDown: {name} was clicked.");
        // Tell the controller that this pet was clicked
        GameObject controllerObject = GameObject.Find("Controller"); // Find the Controller GameObject
        if (controllerObject != null)
        {
            Controller controller = controllerObject.GetComponent<Controller>();
            if (controller != null)
            {
                controller.PetClicked(this);
            }
        }
        Debug.Log($"Pet.OnMouseDown: Finished.");
    }
}