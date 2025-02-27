using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CardValues cardValues;
    [SerializeField] private GameObject petPrefab; // Reference to the PetEntity prefab

    public Inventory playerInventory = new();

    void Start()
    {
        playerInventory.gold = 100;
        playerInventory.gems = 10;

        playerInventory.entities = new CardValue[]
        {
            cardValues.PetS0001
        };

        playerInventory.items = new CardValue[]
        {
            // Add items here
        };

        // Instantiate pets
        foreach (var petValue in playerInventory.entities)
        {
            GameObject petObject = Instantiate(petPrefab);
            Pet petComponent = petObject.GetComponent<Pet>();
            petComponent.CreatePet(petValue);
        }
    }
}

public class Inventory
{
    public CardValue[] entities;
    public CardValue[] items;

    public double gold;
    public double gems;
}
