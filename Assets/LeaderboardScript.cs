using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    public string dataFileName = "data.json";
    private GameDataList dataList;
    public GameObject leaderPrefab;
    private int index = 0;
    [Serializable]
    public class GameData
    {
        public string name;
        public int points;
        public string time;
    }

    [Serializable]
    public class GameDataList
    {
        public List<GameData> data = new List<GameData>();
    }

    private void Awake()
    {
        LoadData();
        display(index);
    }

    public void Next()
    {
        if (dataList != null && dataList.data.Count > 0)
        {
            if (index + 4 < dataList.data.Count+3)  // Prevent going beyond the last entry
            {
                index += 4;
                display(index);
            }
        }
    }

    public void Previous()
    {
        if (dataList != null && dataList.data.Count > 0)
        {
            if (index - 4 >= 0)  // Prevent index from going below 0
            {
                index -= 4;
                display(index);
            }
        }
    }

    public void display(int init)
    {
        if (leaderPrefab == null)
        {
            Debug.LogError("LeaderPrefab is not assigned!");
            return;
        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        int initIndex = init;
        float startY = 150f;

        for (int i = 0; i < 4; i++)
        {
            try
            {
                if (dataList.data[initIndex] != null)//Really bad but my brain is not braining so try catch will do 
                {
                    var gameData = dataList.data[initIndex];
                    GameObject newEntry = Instantiate(leaderPrefab, transform);
                    newEntry.transform.localPosition = new Vector3(0, startY, 0);
                    TextMeshProUGUI[] text = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
                    text[0].text = gameData.name + " " + gameData.points.ToString() + " " + gameData.time;
                    startY -= 100f;
                    initIndex++;
                }
            }
            catch
            {
                break;
            }

        }
    }

    public void SaveData(string playerName = "test", int playerPoints = 0, string playerTime = "test")
    {
        string filePath = Application.persistentDataPath + "/" + dataFileName;
        GameDataList dataList = new GameDataList();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            dataList = JsonUtility.FromJson<GameDataList>(json) ?? new GameDataList();
        }

        GameData newEntry = new GameData { name = playerName, points = playerPoints, time = playerTime };
        dataList.data.Add(newEntry);
        string newJson = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(filePath, newJson);
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/" + dataFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            dataList = JsonUtility.FromJson<GameDataList>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found: " + filePath);
        }
    }
}
