using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory playerInventory = new()
    {
        gold = 100,
        gems = 0
    };
}

[System.Serializable]
public class Inventory
{
    public CardValue[] entities;
    public CardValue[] items;

    public int gold;
    public int gems;
}