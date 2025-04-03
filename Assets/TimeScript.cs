using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    private float initTime;
    public TextMeshProUGUI textField;
    void Start()
    {
        initTime = Time.time;
    }

    void Update()
    {   
        float currentTime = GetTime();
        textField.text = GetClockTime();
    }

    public int GetHours()
    {
        return Mathf.FloorToInt(GetTime() / 3600) % 24;
    }

    public int GetMinutes()
    {
        return Mathf.FloorToInt(GetTime() / 60) % 60;
    }

    public int GetSeconds()
    {
        return Mathf.FloorToInt(GetTime()) % 60;
    }

    public float GetTime() 
    {
        return Time.time - initTime;//Laiks no spēles palaišanas sākuma - Laiks kopš spēles uzsākšanas
    }
    public string GetClockTime()
    {
        return (GetHours() > 9 ? GetHours().ToString() : ("0" + GetHours().ToString())) + ":" + (GetMinutes() > 9 ? GetMinutes().ToString() : ("0" + GetMinutes().ToString())) + ":" + (GetSeconds() > 9 ? GetSeconds().ToString() : ("0" + GetSeconds().ToString()));
    }
}
