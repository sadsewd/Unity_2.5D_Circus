using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RolledNumberScript : MonoBehaviour
{
    DiceRollScript diceRollScript;
    [SerializeField] TextMeshProUGUI rolledNumberText;
    void Awake()
    {
        diceRollScript = FindObjectOfType<DiceRollScript>();
    }

    void Update()
    {
        if(diceRollScript != null)
        {
            if (diceRollScript.isLanded) rolledNumberText.text = diceRollScript.diceFaceNum;
        }else Debug.LogError("DiceRollScript not found in scene!");
    }
}
