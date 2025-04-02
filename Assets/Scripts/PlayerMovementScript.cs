using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private List<Transform> pathCells;
    private int currentCellIndex = 0;
    public Boolean finished = false;
    public void SetPathCells(List<Transform> cells)
    {
        pathCells = cells;
    }

    public void MoveSteps(int steps)
    {
        int targetIndex = (currentCellIndex + steps);
        StartCoroutine(MoveAlongPath(targetIndex));
    }

    private IEnumerator MoveAlongPath(int targetIndex)//Pārvietošanās pa ailēm
    {
        if (currentCellIndex != 63)
        {
            if (targetIndex > 63)
            {
                while (currentCellIndex < 63)
                {
                    currentCellIndex++;
                    yield return MoveFunc();
                }
                while (currentCellIndex >= 63 - (targetIndex % pathCells.Count))
                {
                    currentCellIndex--;
                    yield return MoveFunc();
                }
            }
            else
            {
                while (currentCellIndex < targetIndex)
                {
                    currentCellIndex++;
                    yield return MoveFunc();
                }
            }
            if (targetIndex == 63)
            {
                finished = true;
            }
        }
    }
    private IEnumerator MoveFunc()
    {
        Transform nextCell = pathCells[currentCellIndex];

        while (Vector3.Distance(transform.position, new Vector3(nextCell.position.x, 25, nextCell.position.z)) > 0.05f)//Salīdzina distanci starp spēlētāju un nākamo aili. Darbību beidz kad distancē ir mazāka par 0.05f, jeb kad ir nonācis uz ailē
        {
            Vector3 targetPosition = new Vector3(nextCell.position.x, 25, nextCell.position.z); //Nosaka nonākamās ailes pozīciju, Y ass paliek konstanta lai tēli nebūtu zemē
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);//Parvietošanās
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
    }

}
