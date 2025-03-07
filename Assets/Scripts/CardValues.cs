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
        imageName = "Larry",
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
        imageName = "missing",
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
        imageName = "missing",
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
    
    public CardValue PetA0001 = new()
    {
        name = "Creb",
        health = 750,
        baseAttack = 95,
        attackDeviator = 0.1f,
        tier = "A",
        type = "Pet",
        imageName = "missing",
        abilities = new Ability[]
        {
            new()
            {
                name = "Weirdly Cute Face",
                description = "Uses his weirdly cute face to distract an enemy of your choice for 2 turns.",
                cooldown = 10,
                id = "A0001_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "stun",
                        target = new [] { "enemy", "select" },
                        amplifier = 0,
                        duration = 2
                    }
                }
            },
            new()
            {
                name = "Creb Dance",
                description = "Does his silly creb dance which distracts all enemies for 1 turn with his sick dance moves",
                cooldown = 10,
                id = "A0001_ability1",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "stun",
                        target = new [] { "enemy", "all" },
                        amplifier = 0,
                        duration = 1
                    }
                }
            }
        }
    };

    public CardValue PetA0002 = new()
    {
        name = "Wowosaurus",
        health = 800,
        baseAttack = 75,
        attackDeviator = 0.1f,
        tier = "A",
        type = "Pet",
        imageName = "missing",
        abilities = new Ability[]
        {
            new()
            {
                name = "Fire Spew of DEATH",
                description = "Spews a bunch of fire at an enemy of your choice, dealing 150 damage.",
                cooldown = 10,
                id = "A0002_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new [] { "enemy", "select" },
                        amplifier = -150,
                        duration = 0
                    }
                }
            },
            new()
            {
                name = "Shield of Fire",
                description = "Creates a shield of fire around himself, absorbing 200 damage.",
                cooldown = 10,
                id = "A0002_ability1",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "absorbDamage",
                        target = new[] { "player", "this" },
                        amplifier = 200,
                        duration = 0
                    }
                }
            }
        }
    };

    public CardValue PetA0003 = new()
    {
        name = "Eli",
        health = 850,
        baseAttack = 65,
        attackDeviator = 0.1f,
        tier = "A",
        type = "Pet",
        imageName = "missing",
        abilities = new Ability[]
        {
            new()
            {
                name = "Manipulation",
                description = "Manipulates an enemy of your choice, making him attack his own team for 2 turns.",
                cooldown = 10,
                id = "A0003_ability0",
                effects = new Effect[]
                {
                    new()
                    {
                        type = "damageTargetSwap",
                        target = new []{ "enemy", "select" },
                        amplifier = 0,
                        duration = 2
                    }
                }
            }
        }
    };

    // TIER B CARDS

    public CardValue PetB0001 = new()
    {
        name = "Uwusaurus",
        health = 550,
        baseAttack = 50,
        attackDeviator = 0.1f,
        tier = "B",
        type = "Pet",
        imageName = "missing",
        abilities = new Ability[]
        {
            new()
            {
                name = "uwu",
                description = "summons u, w, and u around and enemy of your choice, dealing 100 damage.",
                cooldown = 10,
                effects = new Effect[]
                {
                    new()
                    {
                        type = "updateHealth",
                        target = new []{ "enemy", "select" },
                        amplifier = -100,
                        duration = 0
                    }
                }
            }
        }
    };

    // TIER C CARDS

    // TIER S ITEMS

    // OTHER SHIT
    public Sprite GetPetSprite(string imageName)
    {
        Debug.Log($"CardValues.GetPetSprite: Attempting to load sprite: Images/Pets/{imageName}");
        Sprite sprite = Resources.Load<Sprite>($"Images/Pets/{imageName}");
        if (sprite == null)
        {
            Debug.LogError($"CardValues.GetPetSprite: Failed to load sprite: Images/Pets/{imageName}");
        }
        return sprite;
    }
    
}

public class CardValue
{

    public string type;
    public string name;
    public float health;
    public float baseAttack;
    public float attackDeviator;
    public string tier;
    public string imageName;
    public Sprite image;
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