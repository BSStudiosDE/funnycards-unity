using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bot : MonoBehaviour
{
    public Inventory botInventory = new();
    public Controller controller;
    public void TakeTurn()
    {
        //Check if there are still pets alive to attack.
        if (controller.playerPets.All(pet => pet.cardValue.health <= 0) || controller.botPets.All(pet => pet.cardValue.health <= 0))
        {
            Debug.Log("Bot.TakeTurn: No pets remaining, skipping turn");
            return;
        }
        // Get a random bot pet
        var attacker = GetRandomAlivePet(controller.botPets);
        if (attacker == null)
        {
            Debug.Log("Bot.TakeTurn: No alive bot pets");
            return;
        }

        // Get a random player pet
        var target = GetRandomAlivePet(controller.playerPets);
        if (target == null)
        {
            Debug.Log("Bot.TakeTurn: No alive player pets");
            return;
        }

        Debug.Log($"Bot.TakeTurn: Bot selecting attacker: {attacker.name}, target: {target.name}");
        controller.AddLog($"Bot selects {attacker.name} to attack {target.name}.");
        // Let the controller know that these pets are selected
        controller.PetClicked(attacker, 1);
        controller.PetClicked(target, 1);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private Pet GetRandomAlivePet(List<Pet> pets)
    {
        Debug.Log("Bot.GetRandomAlivePet: Selecting a random pet from list.");
        var alivePets = new List<Pet>();
        foreach (var pet in pets)
        {
            if (pet.cardValue.health > 0)
            {
                alivePets.Add(pet);
            }
        }
        if (alivePets.Count == 0) return null;
        var selectedPet = alivePets[Random.Range(0, alivePets.Count)];
        Debug.Log($"Bot.GetRandomAlivePet: Selected pet {selectedPet.name}.");
        return selectedPet;
    }
}