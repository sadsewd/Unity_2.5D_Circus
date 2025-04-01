using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollScript : MonoBehaviour
{
    Rigidbody rBody;
    Vector3 position;
    [SerializeField]private float maxRandForceVal, startRollingForce;
    float forceX, forceY, forceZ;
    public string diceFaceNum;
    public bool isLanded = false;
    public bool firstThrow = false;
    void Awake()
    {
        Initialize(0);
    }

    void Update()
    {
        if (rBody != null)
        {
            if (Input.GetMouseButtonDown(0) && isLanded || Input.GetMouseButtonDown(0) && !firstThrow)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                    {
                        if(!firstThrow)
                            firstThrow = true;

                        RollDice();
                    }
                }
            }
        }
    }
    public void Initialize(int node)
    {
        if (node == 0)
        {
            rBody = GetComponent<Rigidbody>();
            position = transform.position;

        }
        else if (node == 1)
        {
            transform.position = position;
        }
        firstThrow = false;
        isLanded = false;
        rBody.isKinematic = true;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);

    }

    private void RollDice()
    {
        rBody.isKinematic = false;
        forceX = Random.Range(0, maxRandForceVal);
        forceY = Random.Range(0, maxRandForceVal);
        forceZ = Random.Range(0, maxRandForceVal);
        rBody.AddForce(Vector3.up*Random.Range(800,startRollingForce));
        rBody.AddTorque(forceX, forceY, forceZ);
    }

    public void ResetDice() {
        firstThrow = false;
        isLanded = false;
        rBody.isKinematic = true;
        transform.position = position;
    }
}
