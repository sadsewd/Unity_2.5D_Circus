using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveScript : MonoBehaviour
{
    public GameObject gameObject2, gameObject3, gameObject4;

    public void ToggleActiveAfterDelay(int obj)
    {
        switch (obj)
        {
            case 1:
                gameObject2.SetActive(!gameObject2.activeSelf);
                break;
            case 2:
                gameObject3.SetActive(!gameObject2.activeSelf);
                break;
            case 3:
                gameObject4.SetActive(!gameObject2.activeSelf);
                break;
            default:
                Debug.Log("Objekts nepastāv (1-3)");
                break;
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
