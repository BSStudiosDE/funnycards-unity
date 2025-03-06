using UnityEngine;

public class CardValues : MonoBehaviour
{
    // TIER S CARDS
    public CardValue PetS0001 = new()
    {
        name = "Larry",
        health = 1000,
        baseAttack = 125,
        attackDeviator = 0.1f,
        tier = "S",
        type = "Pet",
        image = null, 
        abilities = new Ability[]
        { 
            new()
            {
                name = "Moustache Whip",
                description = "Uses his moustache to whip an enemy of your choice, dealing 200 damage.",
                cooldown = 10,
                id = "S0001_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new[] { "enemy", "select" },
                        amplifier = -200,
                        duration = 0
                    }
                }
            }
        }
    };
    
    public CardValue PetS0002 = new()
    {
        name = "Adinath",
        health = 1250,
        baseAttack = 110,
        attackDeviator = 0.1f,
        tier = "S",
        type = "Pet",
        image = null, 
        abilities = new Ability[]
        {
            new()
            {
                name = "Intimidating Smartness",
                description = "Intimidates an enemy of your choice, making him cower and be stunned for 3 turns.",
                cooldown = 10,
                id = "S0002_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "stun",
                        target = new[] { "enemy", "select" },
                        amplifier = 0,
                        duration = 3,
                    }
                }
            },
            new()
            {
                name = "Big Brain Moves",
                description = "Uses his big brain to deal 250 damage to an enemy of your choice.",
                cooldown = 10,
                id = "S0002_ability1",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new[] { "enemy", "select"},
                        amplifier = -250,
                        duration = 0,
                    }
                }
            }
        }
    };

    public CardValue PetS0003 = new()
    {
        name = "Reid",
        health = 1100,
        baseAttack = 120,
        attackDeviator = 0.1f,
        tier = "S",
        type = "Pet",
        image = null, 
        abilities = new Ability[]
        {
            new()
            {
                name = "Math God",
                description = "Uses his math skills to allow the entire team to dodge all attacks for 2 turns.",
                cooldown = 10,
                id = "S0003_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "multiplyDamage",
                        target = new [] { "player", "all" },
                        amplifier = 0,
                        duration = 2,
                    }
                }
            }
        }
    };

    // TIER A CARDS
    
    // TIER B CARDS

    // TIER C CARDS
    
    // TIER S ITEMS

}

public class CardValue
{
    
    public string type;
    public string name;
    public float health;
    public float baseAttack;
    public float attackDeviator;
    public string tier;
    public Sprite image; // Add the sprite property 
    public Ability[] abilities;
}

public class Ability
{
    public string name;
    public string description;
    public int cooldown;
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