using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveScript : MonoBehaviour
{
    public GameObject gameObject2, gameObject3;

    public void ToggleActiveAfterDelay(float delay)
    {
        StartCoroutine(ToggleActiveCoroutine(delay));
    }

    public void ToggleActiveAfterDelay1(float delay)
    {
        StartCoroutine(ToggleActiveCoroutine1(delay));
    }

    private IEnumerator ToggleActiveCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject2.SetActive(!gameObject2.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private IEnumerator ToggleActiveCoroutine1(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject3.SetActive(!gameObject2.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
