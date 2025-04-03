using UnityEngine;

public class DecorSpriteScript : MonoBehaviour
{
    public Color newColor = Color.red; // Set color in Inspector

    void Start()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = newColor;
        }
    }
}
