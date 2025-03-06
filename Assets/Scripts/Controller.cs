using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int turns;
    public Runtime runtime = new();

    public List<Pet> playerPets = new();
    public List<Pet> botPets = new();

    private Pet _selectedAttacker;
    private Pet _selectedTarget;
    private Bot _bot;
    [SerializeField] private CardValues cardValues;
    [SerializeField] private GameObject petPrefab;

    [SerializeField] private Vector3 playerPetStartPosition;
    [SerializeField] private float playerPetVerticalSpacing;
    [SerializeField] private Vector3 botPetStartPosition;
    [SerializeField] private float botPetVerticalSpacing;
    [SerializeField] private float petZPosition = 0f;

    private void Start()
    {
        Debug.Log("Controller.Start: Initializing...");
        _bot = gameObject.AddComponent<Bot>();
        _bot.controller = this;
        _bot.botInventory.entities = new CardValue[]
        {
            cardValues.PetS0002,
            cardValues.PetS0003
        };
        CreatePets();
        PositionPets();
        turns = 0;
        NextTurn();
        Debug.Log("Controller.Start: Initialization complete.");
    }

    public void CreatePets()
    {
        Debug.Log("Controller.CreatePets: Starting pet creation...");
        Inventory playerInventory = new();

        playerInventory.entities = new CardValue[]
        {
            cardValues.PetS0001
        };
        // Instantiate pets
        foreach (var petValue in playerInventory.entities)
        {
            GameObject petObject = Instantiate(petPrefab);
            Pet petComponent = petObject.GetComponent<Pet>();
            petComponent.CreatePet(petValue);
            playerPets.Add(petComponent);
            Debug.Log($"Controller.CreatePets: Created player pet: {petValue.name}");
        }
        foreach (var petValue in _bot.botInventory.entities)
        {
            GameObject petObject = Instantiate(petPrefab);
            Pet petComponent = petObject.GetComponent<Pet>();
            petComponent.CreatePet(petValue);
            botPets.Add(petComponent);
            Debug.Log($"Controller.CreatePets: Created bot pet: {petValue.name}");
        }
        Debug.Log("Controller.CreatePets: Pet creation complete.");
    }

    public void PositionPets()
    {
        Debug.Log("Controller.PositionPets: Starting pet positioning...");
        //Position Player pets
        for (int i = 0; i < playerPets.Count; i++)
        {
            // Calculate the position for each pet
            Vector3 position = playerPetStartPosition + new Vector3(0, i * playerPetVerticalSpacing, petZPosition);
            playerPets[i].transform.position = position;
            Debug.Log($"Controller.PositionPets: Positioned player pet {playerPets[i].name} at {position}");
        }

        //Position Bot pets
        for (int i = 0; i < botPets.Count; i++)
        {
            // Calculate the position for each pet
            Vector3 position = botPetStartPosition + new Vector3(0, i * botPetVerticalSpacing, petZPosition);
            botPets[i].transform.position = position;
            Debug.Log($"Controller.PositionPets: Positioned bot pet {botPets[i].name} at {position}");
        }
        Debug.Log("Controller.PositionPets: Pet positioning complete.");
    }

    public void NextTurn()
    {
        Debug.Log($"Controller.NextTurn: Starting turn {turns}, Player Turn: {runtime.playerTurn}");
        CheckForGameOver();
        _selectedAttacker = null;
        _selectedTarget = null;
        Debug.Log($"Controller.NextTurn: _selectedAttacker set to null, _selectedTarget set to null.");
        if (turns % 2 == 0)
        {
            runtime.playerTurn = true;
            Debug.Log("Controller.NextTurn: Starting PlayerTurn coroutine.");
            StartCoroutine(PlayerTurn());
        }
        else
        {
            runtime.playerTurn = false;
            Debug.Log("Controller.NextTurn: Starting EnemyTurn coroutine.");
            StartCoroutine(EnemyTurn());
        }
        turns++;
        Debug.Log($"Controller.NextTurn: Turn {turns} started. Player Turn: {runtime.playerTurn}");
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Controller.PlayerTurn: Player's turn coroutine started.");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Controller.PlayerTurn: Player's turn coroutine finished.");
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Controller.EnemyTurn: Enemy's turn coroutine started.");
        yield return new WaitForSeconds(1.0f);
        _bot.TakeTurn();
        Debug.Log("Controller.EnemyTurn: Enemy's turn coroutine finished.");
    }

    public void PetClicked(Pet pet)
    {
        Debug.Log($"Controller.PetClicked: Pet {pet.name} clicked. Player's turn: {runtime.playerTurn}");
        if (runtime.playerTurn)
        {
            Debug.Log("Controller.PetClicked: It is the player's turn.");
            if (playerPets.Contains(pet))
            {
                Debug.Log($"Controller.PetClicked: Clicked pet {pet.name} is a player pet.");
                if (_selectedAttacker == null)
                {
                    _selectedAttacker = pet;
                    Debug.Log($"Controller.PetClicked: Player Attacker Selected: {pet.name}");
                }
            }
            else if (botPets.Contains(pet))
            {
                Debug.Log($"Controller.PetClicked: Clicked pet {pet.name} is a bot pet.");
                if (_selectedAttacker != null)
                {
                    _selectedTarget = pet;
                    Debug.Log($"Controller.PetClicked: Player Target Selected: {pet.name}");
                    StartCoroutine(Attack());
                }
            }
        }
        else
        {
            Debug.Log("Controller.PetClicked: It is the bot's turn.");
            if (botPets.Contains(pet))
            {
                Debug.Log($"Controller.PetClicked: Clicked pet {pet.name} is a bot pet.");
                if (_selectedAttacker == null)
                {
                    _selectedAttacker = pet;
                    Debug.Log($"Controller.PetClicked: Bot Attacker Selected: {pet.name}");
                }
            }
            else if (playerPets.Contains(pet))
            {
                Debug.Log($"Controller.PetClicked: Clicked pet {pet.name} is a player pet.");
                if (_selectedAttacker != null)
                {
                    _selectedTarget = pet;
                    Debug.Log($"Controller.PetClicked: Bot Target Selected: {pet.name}");
                    StartCoroutine(Attack());
                }
            }
        }
        Debug.Log($"Controller.PetClicked: _selectedAttacker: {_selectedAttacker?.name}, _selectedTarget: {_selectedTarget?.name}");
    }

    private IEnumerator Attack()
    {
        Debug.Log($"Controller.Attack: Attack coroutine started. _selectedAttacker: {_selectedAttacker.name}, _selectedTarget: {_selectedTarget.name}");
        //Make sure both an attacker and a target exist.
        if (_selectedAttacker != null && _selectedTarget != null)
        {
            Debug.Log($"Controller.Attack: {_selectedAttacker.name} is attacking {_selectedTarget.name}");
            _selectedTarget.ChangeHealth(-1 * (_selectedAttacker.cardValue.baseAttack * (1 + Random.Range(0f, _selectedAttacker.cardValue.attackDeviator))));
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Controller.Attack: Attack coroutine finished.");
        NextTurn();
    }
    private void CheckForGameOver()
    {
        Debug.Log("Controller.CheckForGameOver: Checking for game over.");
        if (playerPets.All(pet => pet.cardValue.health <= 0))
        {
            Debug.Log("Controller.CheckForGameOver: Bot Wins!");
            EndGame();
        }
        else if (botPets.All(pet => pet.cardValue.health <= 0))
        {
            Debug.Log("Controller.CheckForGameOver: Player Wins!");
            EndGame();
        }
        Debug.Log("Controller.CheckForGameOver: No game over condition met.");
    }
    private void EndGame()
    {
        Debug.Log("Controller.EndGame: Game has ended.");
    }
}

public class Runtime
{
    public bool playerTurn;
}