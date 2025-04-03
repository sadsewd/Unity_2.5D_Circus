using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject cellsParent; //Aiļu Virs-objekts
    DiceRollScript diceRollScript;
    public List<Transform> boardCells = new List<Transform>();
    public List<PlayerMovement> players = new List<PlayerMovement>();
    private int currentPlayerIndex = 0;
    public GameObject endScreen;
    public TextMeshProUGUI currentPlayerTextField;
    private bool isGameFinished = false;
    PlayerMovement currentPlayer;
    TextMeshPro playerNameText;

    private Dictionary<int, int> laddersAndSlides = new Dictionary<int, int>
    {
        { 19, 12 },
        { 25, 22 },
        { 42, 27 },
        { 53, 35 },
        { 45, 34 },
        { 60, 51 },
        { 6, 9 },
        { 11, 21 },
        { 17, 30 },
        { 28, 54 },
        { 37, 41 },

    };
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
        if (!isGameFinished) {
            currentPlayer = players[currentPlayerIndex];
            playerNameText = currentPlayer.GetComponentInChildren<TextMeshPro>();
            currentPlayerTextField.text = playerNameText.text;
            if (!string.IsNullOrEmpty(diceRollScript.diceFaceNum) && diceRollScript.isLanded)
            {
                Move();
                diceRollScript.Initialize(1);
            }
            foreach (var player in players)
            {
                if (player.finished)
                {
                    if (endScreen != null)
                    {


                        Transform winnerTextObj = endScreen.transform.Find("Winner");
                        TextMeshProUGUI textComponent = winnerTextObj.GetComponent<TextMeshProUGUI>();
                        textComponent.text = "Player " + playerNameText.text + " has finished!";
                        isGameFinished = true;
                        endScreen.SetActive(true);
                    }
                    else Debug.Log("End screen not assigned.");
                }
            }
        }
    }


    public void Move()
    {
        PlayerMovement currentPlayer = players[currentPlayerIndex];
        StartCoroutine(MoveAndCheckSnakesLadders(currentPlayer));
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; //Nākamais spēlētajs
    }

    private IEnumerator MoveAndCheckSnakesLadders(PlayerMovement player)
    {
        int rolledNumber = Int32.Parse(diceRollScript.diceFaceNum);
        yield return player.MoveSteps(rolledNumber); //Aiziet uz pozīciju kuru norāda kauliņš, veic pa soļiem sekojot lauku secībai

        if (laddersAndSlides.ContainsKey(player.currentCellIndex))//Ja nonāk pozīcijā kur kāpnes vai kritiens, veic tiešu pāreju no viena lauka uz otru (Sākuma un beigu pozīcija norādīta vārdnīcā)
        {
            int newCellIndex = laddersAndSlides[player.currentCellIndex];//Poz uz kuru jadodas
            yield return player.MoveStepsDirect(newCellIndex);//Izsauc funkciju kura veic tiešu pāreju
        }
    }
}
