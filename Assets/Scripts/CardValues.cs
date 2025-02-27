using UnityEngine;

public class CardValues : MonoBehaviour
{
    // TIER S CARDS
    public CardValue PetS0001 = new()
    {
        name = "Sample Pet",
        health = 1000,
        baseAttack = 100,
        attackDeviator = 0.1f,
        tier = "S",
        type = "Pet",
        image = null, // Add the sprite for the pet here
        abilities = new Ability[]
        {
            new()
            {
                name = "Sample Ability",
                description = "Does Sample Ability Things which deals 50 damage to all enemies",
                id = "S0001_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new string[] { "enemy", "all" },
                        amplifier = -50f,
                        duration = 0
                    }
                }
            },
            new()
            {
                name = "very cool ability",
                description = "Does very cool ability things",
                id = "S0001_ability1",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new string[] { "player", "all" },
                        amplifier = 10f,
                        duration = 5
                    },
                    new()
                    {
                        type = "updateHealth",
                        target = new string[] { "player", "select" },
                        amplifier = 50f,
                        duration = 0
                    }
                }
            }
        }
    };
}

public class CardValue
{
    public string name;
    public float health;
    public float baseAttack;
    public float attackDeviator;
    public string tier;
    public string type;
    public Sprite image; // Add the sprite property
    public Ability[] abilities;
}

public class Ability
{
    public string name;
    public string description;
    public string id;
    public Effect[] effects;
}

public class Effect
{
    public string type;
    public string[] target;
    public float amplifier;
    public int duration;
}
