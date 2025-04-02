using UnityEngine;
using TMPro;

public class CellTextManager : MonoBehaviour
{
    void Start()
    {
        SetAllChildTexts();
    }

    public void SetAllChildTexts()
    {
        int index = 1;
        foreach (Transform child in transform)
        {
            TextMeshPro textComponent = child.GetComponentInChildren<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.text = index.ToString();
            }
            index++;
        }
    }
}
