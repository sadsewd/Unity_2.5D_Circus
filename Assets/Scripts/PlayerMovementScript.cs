using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private List<Transform> pathCells;
    public int currentCellIndex = 0;
    public Boolean finished = false;
    public Boolean isMoving = false;
    private Animator animator;
    public int moves = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetPathCells(List<Transform> cells)
    {
        pathCells = cells;
    }


    public IEnumerator MoveSteps(int steps)//Pārvietošanās pa soļiem
    {
        int targetIndex = currentCellIndex + steps;
        yield return StartCoroutine(MoveAlongPath(targetIndex));
    }

    public IEnumerator MoveStepsDirect(int index)//Tieša pārejo no viena lauka uz mērķa indeksa lauku
    {
        Transform nextCell = pathCells[index];
        currentCellIndex = index;
        while (Vector3.Distance(transform.position, new Vector3(nextCell.position.x, 25, nextCell.position.z)) > 0.05f)//Salīdzina distanci starp spēlētāju un nākamo aili. Darbību beidz kad distancē ir mazāka par 0.05f, jeb kad ir nonācis uz ailē
        {
            Vector3 targetPosition = new Vector3(nextCell.position.x, 25, nextCell.position.z); //Nosaka nonākamās ailes pozīciju, Y ass paliek konstanta lai tēli nebūtu zemē
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);//Parvietošanās
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator MoveAlongPath(int targetIndex)//Pārvietošanās pa ailēm
    {
        if (currentCellIndex != 63)
        {
            isMoving = true;
            animator.SetBool("isWalking", true);
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
            animator.SetBool("isWalking", false);
            isMoving = false;
            if (targetIndex == 63)
            {
                finished = true;
                animator.SetBool("isFinished", true);
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

        yield return new WaitForSeconds(0.1f);
    }

}
