using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class Controller : MonoBehaviour
{
    public int turns;
    public Runtime runtime = new();
    public enum TurnState { PlayerTurn, BotTurn }
    public TurnState currentTurnState;
    public List<Pet> playerPets = new();
    public List<Pet> botPets = new();

    [SerializeField] ActionLogManager actionLogManager;

    private Pet _selectedAttacker;
    private Pet _selectedTarget;
    private Bot _bot;
    [SerializeField] public CardValues cardValues;
    [SerializeField] private GameObject petPrefab;

    [SerializeField] private Vector3 playerPetStartPosition;
    [SerializeField] private float playerPetVerticalSpacing;
    [SerializeField] private Vector3 botPetStartPosition;
    [SerializeField] private float botPetVerticalSpacing;
    [SerializeField] private float petZPosition;

    private bool _gameOver;
    private bool _nextTurnClickable;
    private bool _isAttacking; // Flag to prevent actions during attacks

    private void Start()
    {
        Debug.Log("Controller.Start: Initializing...");
        InitializeSaveFile();
        _bot = gameObject.AddComponent<Bot>();
        _bot.controller = this;
        _bot.botInventory.entities = new[]
        {
            cardValues.PetA0002,
            cardValues.PetB0001
        };
        CreatePets();
        PositionPets();
        turns = 0;
        NextTurn();
        Debug.Log("Controller.Start: Initialization complete.");
    }
    private void InitializeSaveFile()
    {
        SaveFileManager saveFileManager = gameObject.AddComponent<SaveFileManager>();
        Player player = gameObject.AddComponent<Player>();
        saveFileManager.player = player;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "playerSave.json")))
        {
            saveFileManager.LoadGame();
        }
    }
    public void CreatePets()
    {
        Debug.Log("Controller.CreatePets: Starting pet creation...");
        Inventory playerInventory = new()
        {
            entities = new[]
            {
                cardValues.PetS0002,
                cardValues.PetA0001
            }
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
        for (var i = 0; i < playerPets.Count; i++)
        {
            // Calculate the position for each pet
            var position = playerPetStartPosition + new Vector3(0, i * playerPetVerticalSpacing, petZPosition);
            playerPets[i].transform.position = position;
            Debug.Log($"Controller.PositionPets: Positioned player pet {playerPets[i].name} at {position}");
        }

        //Position Bot pets
        for (var i = 0; i < botPets.Count; i++)
        {
            // Calculate the position for each pet
            var position = botPetStartPosition + new Vector3(0, i * botPetVerticalSpacing, petZPosition);
            botPets[i].transform.position = position;
            Debug.Log($"Controller.PositionPets: Positioned bot pet {botPets[i].name} at {position}");
        }
        Debug.Log("Controller.PositionPets: Pet positioning complete.");
    }
    
    public void NextTurn()
    {
        if (_gameOver)
        {
            Debug.Log("Controller.NextTurn: Game is over, not starting new turn.");
            return;
        }
        _selectedAttacker = null;
        _selectedTarget = null;
        Debug.Log($"Controller.NextTurn: Starting turn {turns}.");
        turns++;
        Debug.Log($"Controller.NextTurn: Turn {turns} started.");
        CheckForGameOver();
        StartCoroutine(AttackRound());
    }
    private IEnumerator AttackRound()
    {
        // Check if the game is over before proceeding
        if (_gameOver)
        {
            Debug.Log("Controller.AttackRound: Game is over, not starting new turn.");
            _nextTurnClickable = true;
            yield break;
        }
        _selectedAttacker = null;
        _selectedTarget = null;
        Debug.Log("Controller.AttackRound: AttackRound started.");
        //Player's Turn
        currentTurnState = TurnState.PlayerTurn;
        AddLog("It is your turn.");
        yield return new WaitForSeconds(1.0f);
        //Wait for player to attack
        while (_selectedAttacker == null || _selectedTarget == null)
        {
            yield return null;
        }
        StartCoroutine(Attack());
        while (_isAttacking)
        {
            yield return null;
        }
        //Check if the game is over.
        CheckForGameOver();
        if (_gameOver)
        {
            Debug.Log("Controller.AttackRound: Game is over, not starting bot's turn.");
            _nextTurnClickable = true;
            yield break;
        }
        //Bot's Turn
        currentTurnState = TurnState.BotTurn;
        _bot.TakeTurn();
        while (_isAttacking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);

        //Check if the game is over
        CheckForGameOver();
        //End Round
        _nextTurnClickable = true;
        Debug.Log("Controller.AttackRound: AttackRound ended.");
    }

    public void NextTurnButtonClicked()
    {
        if (_nextTurnClickable && !_gameOver)
        {
            _nextTurnClickable = false;
            NextTurn();
        }
    }
    public void PetClicked(Pet pet)
    {
        Debug.Log($"Controller.PetClicked: Pet {pet.name} clicked. Player's turn: {runtime.playerTurn}");
        if (_selectedAttacker != null && _selectedTarget != null)
        {
            StartCoroutine(Attack());
            return;
        }
        if(currentTurnState == TurnState.PlayerTurn && !_gameOver)
        {
            if (playerPets.Contains(pet))
            {
                if (_selectedAttacker == null)
                {
                    _selectedAttacker = pet;
                    Debug.Log($"Controller.PetClicked: Player Attacker Selected: {pet.name}");
                }
                else if (_selectedAttacker == pet)
                {
                    _selectedAttacker = null;
                    Debug.Log($"Controller.PetClicked: Player Attacker deselected: {pet.name}");
                    _selectedTarget = null;
                }
            }
            else if (botPets.Contains(pet))
            {
                if (_selectedAttacker != null)
                {
                    _selectedTarget = pet;
                    Debug.Log($"Controller.PetClicked: Player Target Selected: {pet.name}");
                }
                else
                {
                    Debug.Log("Controller.PetClicked: No attacker selected, target is ignored");
                }
            }
        }
        else if(currentTurnState == TurnState.BotTurn && !_gameOver)
        {
            if (botPets.Contains(pet))
            {
                if (_selectedAttacker == null)
                {
                    _selectedAttacker = pet;
                    Debug.Log($"Controller.PetClicked: Bot Attacker Selected: {pet.name}");
                }
                else if (_selectedAttacker == pet)
                {
                    _selectedAttacker = null;
                    Debug.Log($"Controller.PetClicked: Bot Attacker deselected: {pet.name}");
                    _selectedTarget = null;
                }
            }
            else if (playerPets.Contains(pet))
            {
                if (_selectedAttacker != null)
                {
                    _selectedTarget = pet;
                    Debug.Log($"Controller.PetClicked: Bot Target Selected: {pet.name}");
                }
                else
                {
                    Debug.Log("Controller.PetClicked: No attacker selected, target is ignored");
                }
            }
        }

        Debug.Log($"Controller.PetClicked: _selectedAttacker: {_selectedAttacker?.name}, _selectedTarget: {_selectedTarget?.name}");
    }
    private IEnumerator Attack()
    {
        _isAttacking = true;
        // Check if the game is over before attacking
        if (_gameOver)
        {
            Debug.Log("Controller.Attack: Game is over, not attacking.");
            _isAttacking = false;
            yield break;
        }
        if (_selectedAttacker == null || _selectedTarget == null)
        {
            Debug.Log("Controller.Attack: Attacker or target is null, not attacking.");
            _isAttacking = false;
            yield break;
        }
        Debug.Log($"Controller.Attack: Attack coroutine started. _selectedAttacker: {_selectedAttacker.name}, _selectedTarget: {_selectedTarget.name}");
        Debug.Log($"Controller.Attack: {_selectedAttacker.name} is attacking {_selectedTarget.name}");
        float damage = _selectedAttacker.cardValue.baseAttack * (1 + Random.Range(-_selectedAttacker.cardValue.attackDeviator, _selectedAttacker.cardValue.attackDeviator)); // damage is a float
        int roundedDamage = Mathf.RoundToInt(damage);
        _selectedTarget.ChangeHealth(-roundedDamage); // Round for changeHealth parameter
        AddLog($"{_selectedAttacker.name} attacks {_selectedTarget.name}, dealing {roundedDamage} damage");
        yield return new WaitForSeconds(1.0f);

        Debug.Log("Controller.Attack: Attack coroutine finished.");
        _selectedAttacker = null;
        _selectedTarget = null;
        _isAttacking = false;
    }

    public void RemovePetFromLists(Pet pet)
    {
        Debug.Log($"Controller.RemovePetFromLists: Removing pet {pet.name} from lists.");
        playerPets.Remove(pet);
        botPets.Remove(pet);
        Debug.Log($"Controller.RemovePetFromLists: Pet {pet.name} removed from lists.");
    }

    private void CheckForGameOver()
    {
        Debug.Log("Controller.CheckForGameOver: Checking for game over.");

        if (playerPets.All(pet => pet.cardValue.health <= 0) || playerPets.Sum(pet => pet.cardValue.health) <= 0)
        {
            Debug.Log("Controller.CheckForGameOver: Bot Wins!");
            EndGame("The enemy beat you :c");
        }
        else if (botPets.All(pet => pet.cardValue.health <= 0) || botPets.Sum(pet => pet.cardValue.health) <= 0)
        {
            Debug.Log("Controller.CheckForGameOver: Player Wins!");
            EndGame("You won!");
        }
        else
        {
            Debug.Log("Controller.CheckForGameOver: No game over condition met.");
        }
    }

    private void EndGame(string winner)
    {
        Debug.Log("Controller.EndGame: Game has ended.");
        AddLog($"The game has ended: {winner}");
        _gameOver = true;

        // Create copies of the lists to avoid modification issues
        List<Pet> playerPetsCopy = new List<Pet>(playerPets);
        List<Pet> botPetsCopy = new List<Pet>(botPets);

        // Remove all remaining pets
        foreach (var pet in playerPetsCopy)
        {
            Destroy(pet.gameObject);
        }
        foreach (var pet in botPetsCopy)
        {
            Destroy(pet.gameObject);
        }
    }
    public void AddLog(string message)
    {
        Debug.Log($"Controller.AddLog: {message}");
        actionLogManager.AddLogEntry(message);
    }
}

public class Runtime
{
    public bool playerTurn;
}