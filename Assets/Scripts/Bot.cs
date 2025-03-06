using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Inventory botInventory = new();
    public Controller controller;

    public void TakeTurn()
    {
        Debug.Log("Bot.TakeTurn: Bot's turn started.");
        // Get a random bot pet
        Pet attacker = GetRandomAlivePet(controller.botPets);
        if (attacker == null)
        {
            Debug.Log("Bot.BotTurnCoroutine: No alive bot pets");
            return; // Exit the coroutine if there are no bot pets
        }

        // Get a random player pet
        Pet target = GetRandomAlivePet(controller.playerPets);
        if (target == null)
        {
            Debug.Log("Bot.BotTurnCoroutine: No alive player pets");
            return; // Exit the coroutine if there are no player pets
        }

        Debug.Log($"Bot.BotTurnCoroutine: Bot selecting attacker: {attacker.name}, target: {target.name}");

        // Let the controller know that these pets are selected
        Debug.Log($"Bot.BotTurnCoroutine: Calling controller.PetClicked(attacker) for {attacker.name}");
        controller.PetClicked(attacker);
        StartCoroutine(WaitThenClick(target));
    }
    private IEnumerator WaitThenClick(Pet target)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log($"Bot.BotTurnCoroutine: Calling controller.PetClicked(target) for {target.name}");
        controller.PetClicked(target);
    }
    private Pet GetRandomAlivePet(List<Pet> pets)
    {
        Debug.Log("Bot.GetRandomAlivePet: Selecting a random pet from list.");
        List<Pet> alivePets = new List<Pet>();
        foreach (Pet pet in pets)
        {
            if (pet.cardValue.health > 0)
            {
                alivePets.Add(pet);
            }
        }
        if (alivePets.Count == 0) return null;
        Pet selectedPet = alivePets[Random.Range(0, alivePets.Count)];
        Debug.Log($"Bot.GetRandomAlivePet: Selected pet {selectedPet.name}.");
        return selectedPet;
    }
}