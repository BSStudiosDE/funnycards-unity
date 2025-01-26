using UnityEngine;
using UnityEngine.UI;

public class CardValues : MonoBehaviour
{
    public class Entities
    {
        // TIER S ENTITIES
        public Entity S0001 = new()
        {
            name = "Larry",
            health = 1000,
            attackDamage = 125,
            tier = "S",
            type = "entity",
            abilities = new Ability[1]
            {
                new()
                {
                    name = "Moustache Whip",
                    description = "Uses his moustache to whip an enemy of your choice, dealing 200 damage.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 0,
                        effect = "subtract_health",
                        amplifier = 200d
                    },
                    id = "S0001_ability1"
                }
            },
            image = null
        };

        public Entity S0002 = new()
        {
            name = "Adinath",
            health = 1250,
            attackDamage = 110,
            tier = "S",
            type = "entity",
            abilities = new Ability[2]
            {
                new()
                {
                    name = "Intimidating Smartness",
                    description = "Intimidates an enemy of your choice, making him cower and be stunned for 3 turns.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 3,
                        effect = "stun",
                        amplifier = 0
                    },
                    id = "S0002_ability1"
                },
                new()
                {
                    name = "Big Brain Moves",
                    description = "Uses his big brain to deal 250 damage to an enemy of your choice.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 0,
                        effect = "subtract_health",
                        amplifier = 250d
                    },
                    id = "S0002_ability2"
                }
            },
            image = null
        };

        public Entity S0003 = new()
        {
            name = "Reid",
            health = 1100,
            attackDamage = 120,
            tier = "S",
            type = "entity",
            abilities = new Ability[1]
            {
                new()
                {
                    name = "Math God",
                    description = "Uses his math skills to allow the entire team to dodge all attacks for 2 turns.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "player", "all" },
                        duration = 2,
                        effect = "damage_multiply",
                        amplifier = 0
                    },
                    id = "S0003_ability1"
                }
            },
        };

        // TIER A ENTITIES
        public Entity A0001 = new()
        {
            name = "Creb",
            health = 750,
            attackDamage = 95,
            tier = "A",
            type = "entity",
            abilities = new Ability[2]
            {
                new()
                {
                    name = "weirdly Cute Face",
                    description = "Distracts an enemy of your choice for 2 turns with his weirdly cute face.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 2,
                        effect = "stun",
                        amplifier = 0
                    },
                    id = "A0001_ability1"
                },
                new()
                {
                    name = "Creb Dance",
                    description = "Distracts all enemies for 1 turn with his sick dance moves.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "all" },
                        duration = 1,
                        effect = "skip_turn",
                        amplifier = 0
                    },
                    id = "A0001_ability2"
                }
            },
            image = null
        };
        public Entity A0002 = new()
        {
            name = "Wowosaurus",
            health = 800,
            attackDamage = 75,
            tier = "A",
            type = "entity",
            abilities = new Ability[2]
            {
                new()
                {
                    name = "Fire Spew of Death",
                    description = "Spews fire at an enemy of your choice, dealing 150 damage",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 0,
                        effect = "subtract_health",
                        amplifier = 150d
                    },
                    id = "A0002_ability1"
                },
                new()
                {
                    name = "Shield of Fire",
                    description = "Creates a shield of fire around himself, absorbing 200 damage.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "player", "self" },
                        duration = 0,
                        effect = "absorb_damage",
                        amplifier = 200d
                    },
                    id = "A0002_ability2"
                }
            },
            image = null,
        };

        public Entity A0003 = new()
        {
            name = "Eli",
            health = 850,
            attackDamage = 65,
            tier = "A",
            type = "entity",
            abilities = new Ability[1]
            {
                new()
                {
                    name = "Manipulation",
                    description = "Manipulates an enemy of your choice, making him attack his own team for 2 turns.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 2,
                        effect = "damage_target_switch",
                        amplifier = 0d
                    },
                    id = "A0003_ability1"
                }
            },
            image = null,
        };

        // TIER B ENTITIES
        public Entity B0001 = new()
        {
            name = "Uwusaurus",
            health = 550,
            attackDamage = 50,
            tier = "B",
            type = "entity",
            abilities= new Ability[2]
            {
                new()
                {
                    name = "uwu",
                    description = "does uwu to one enemy, dealing 100 damage",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "select" },
                        duration = 0,
                        effect = "subtract_health",
                        amplifier = 100d
                    },
                    id = "B0001_ability1"
                },
                new()
                {
                    name = "Smelly Feet",
                    description = "Uses his smelly feet to deal 50 damage to all enemies.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "enemy", "all" },
                        duration = 0,
                        effect = "subtract_health",
                        amplifier = 50d
                    },
                    id = "B0001_ability2"
                }
            },
            image = null
        };

        // TIER C ENTITIES
        public Entity C0001 = new()
        {
            name = "Fred",
            health = 350,
            attackDamage = 35,
            tier = "C",
            type = "entity",
            abilities = new Ability[1]
            {
                new()
                {
                    name = "Ordinariness",
                    description = "Is so normal he blends in with the background, making him invulnerable for 1 turn.",
                    cooldown = 10,
                    effect = new Effect()
                    {
                        target = new string[] { "player", "self" },
                        duration = 1,
                        effect = "damage_multiply",
                        amplifier = 0d
                    },
                    id = "C0001_ability1"
                }
            },
            image = null
        };



        // TIER D ENTITIES
        public Entity D0001 = new()
        {
            name = "John The Egg",
            health = 150,
            attackDamage = 20,
            tier = "D",
            type = "entity",
            abilities = new Ability[0],
            image = null,
        };

        // TIER F ENTITIES
        public Entity F0001 = new()
        {
            name = "Potato",
            health = 10,
            attackDamage = 1,
            tier = "F",
            type = "entity",
            abilities = new Ability[0],
            image = null
        };

        public Entity F0002 = new()
        {
            name = "Sarah",
            health = 40,
            attackDamage = 7,
            tier = "F",
            type = "entity",
            abilities = new Ability[0],
            image = null
        };
    }

    public class Items
    {

    }

    public Entities entities = new();
    public Items items = new();
}

public class Item
{
    public string name;
    public string description;
    public Effect effect;
    public string tier;
    public string type;
    public Image image;
}



public class Entity
{
    public string name;
    public int health;
    public int attackDamage;
    public string tier;
    public string type;
    public Ability[] abilities;
    public Image image;
}

public class Ability
{
    public string name;
    public string description;
    public int cooldown;
    public Effect effect;
    public string id;
}

public class Effect
{
    public string[] target;
    public int duration;
    public string effect;
    public double amplifier;
}
