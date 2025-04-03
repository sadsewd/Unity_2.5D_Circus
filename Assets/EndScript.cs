using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject gameManager, timeInstance, leaderboardInstance;
    GameManagerScript gameManagerScript;
    TimeScript timeScript;
    SceneChangeScript sceneChangeScript;
    LeaderboardScript leaderboardScript;
    private string name, time;
    private int moves;
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        name = gameManagerScript.winnerName;
        moves = gameManagerScript.winnerMoves;
        timeScript = timeInstance.GetComponent<TimeScript>();
        time = timeScript.GetClockTime();
        leaderboardScript = leaderboardInstance.GetComponent<LeaderboardScript>();
        leaderboardScript.SaveData(name, (moves*Mathf.CeilToInt((timeScript.GetTime()))), time);
        sceneChangeScript = FindObjectOfType<SceneChangeScript>();
    }

    public void EndGame() 
    {
        StartCoroutine(sceneChangeScript.DelayMainMenu());
    }
}
