using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleScript : MonoBehaviour
{
    [Range(0f, 1f)]
    public float power = 0.1f;


    [Header("Postion")]
    public bool jigglePosition = true;
    public Vector3 positionJiggleAmount;
    [Range(0, 100)]
    public float positionFrequency = 8;
    float positionTime;

    [Header("Rotation")]
    public bool jiggleRotation = true;
    public Vector3 rotationJiggleAmount;
    [Range(0, 100)]
    public float rotationFrequency = 8;
    float rotationTime;

    [Header("Scale")]
    public bool jiggleScale = true;
    public Vector3 scaleJiggleAmount = new Vector3(0.1f, -0.1f, 0.1f);
    [Range(0, 100)]
    public float scaleFrequency = 8;
    float scaleTime;
    Vector3 basePostion;
    Quaternion baseRotation;
    Vector3 baseScale;

    void Start()
    {
        basePostion = transform.localPosition;
        baseRotation = transform.localRotation;
        baseScale = transform.localScale;
    }

    void Update()
    {
        //var dt = Time.timeScale; paused if Time.timeScale is 0
        var dt = Time.unscaledDeltaTime;
        positionTime += dt * positionFrequency;
        rotationTime += dt * rotationFrequency;
        scaleTime += dt * scaleFrequency;

        if (jigglePosition)
        {
            transform.localPosition = basePostion + positionJiggleAmount * Mathf.Sin(positionTime) * power;
        }

        if (jiggleRotation)
        {
            transform.localRotation = baseRotation * Quaternion.Euler(rotationJiggleAmount * Mathf.Sin(rotationTime) * power);
        }
        if (jiggleScale)
        {
            transform.localScale = baseScale + scaleJiggleAmount * Mathf.Sin(scaleTime) * power;
        }
    }
}