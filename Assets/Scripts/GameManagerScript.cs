using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    public GameObject cellsParent; //Aiļu Virs-objekts
    DiceRollScript diceRollScript;
    public List<Transform> boardCells = new List<Transform>();
    public List<PlayerMovement> players = new List<PlayerMovement>();
    private int currentPlayerIndex = 0;
    private void Start()
    {
        diceRollScript = FindObjectOfType<DiceRollScript>();
        for (int i = 0; i < cellsParent.transform.childCount; i++)//Ievieto visas ailes sarakstā
        {
            boardCells.Add(cellsParent.transform.GetChild(i));
        }
        players = new List<PlayerMovement>(FindObjectsOfType<PlayerMovement>());//Visi spēlētāji sarakstā

        foreach (var player in players)
        {
            player.SetPathCells(boardCells);//Visas ailes padotas spēlētajiem
        }
    }

    private void Update()
    {
        if (diceRollScript == null) return;//Neizpilžas kods kamēr kauliņa skripta instance nav izveidota

        if (!string.IsNullOrEmpty(diceRollScript.diceFaceNum) && diceRollScript.isLanded)
        {
            Move();
            diceRollScript.Initialize(1);
        }

        foreach(var player in players)
        {
            if (player.finished)
            {
                Debug.Log("Player " + player.name + " has finished!");
            }
        }
    }


    public void Move()
    {
        int rolledNumber = Int32.Parse(diceRollScript.diceFaceNum);
        PlayerMovement currentPlayer = players[currentPlayerIndex];

        currentPlayer.MoveSteps(rolledNumber);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; // Cycle players
    }
}
