using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory playerInventory = new();
}
public class Inventory
{
    public CardValue[] entities;
    public CardValue[] items;

    public double gold;
    public double gems;
}