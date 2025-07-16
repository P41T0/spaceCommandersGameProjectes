using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRowScript : MonoBehaviour
{
    private GameObject sceneController;
    private SceneControllerScript scScript;
    private bool accSpeedPosReached;
    private bool accShootingPosReached;
    private int numEnems;
    private int rowNumber;
    private List<bool> aliveEnemsInRow;
    [SerializeField] private int enemyRowScore;
    [SerializeField] private GameObject[] enemies;



    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("SceneController");
        scScript = sceneController.GetComponent<SceneControllerScript>();
        accShootingPosReached = false;
        accSpeedPosReached = false;
        numEnems = enemies.Length;
        aliveEnemsInRow = new List<bool> { };
        for (int i = 0; i < numEnems; i++)
        {
            enemies[i].GetComponent<EnemyScript>().SetEnemyNumber(i);
            aliveEnemsInRow.Add(true);
        }
    }


    public void SetRowNumber(int setRowNumber)
    {
        rowNumber = setRowNumber;
    }

    //posicio a la que acceleren de cop
    public void RowAccSpeedPosReached()
    {
        if (accSpeedPosReached == false)
        {
            accSpeedPosReached = true;
            scScript.AccSpeedPosReached();
        }
    }

    //posicio a la que comencen a disparar super ràpid
    public void RowAccShootingPosReached()
    {
        if (accShootingPosReached == false)
        {
            accShootingPosReached = true;
            scScript.AccShootingPosReached();
        }
    }



    public void EnemyDied(int enemyNumber)
    {
        numEnems--;
        if (aliveEnemsInRow[enemyNumber] == true)
        {
            aliveEnemsInRow[enemyNumber] = false;
        }
        scScript.EnemySCDied(enemyRowScore);
        if (numEnems == 0)
        {
            scScript.EnemyRowDestroyed(accShootingPosReached, rowNumber);
        }
    }



    public void RestartRow()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyScript>().Restart();
            aliveEnemsInRow[i] = true;
        }
        accShootingPosReached = false;
        accSpeedPosReached = true;
        numEnems = enemies.Length;
    }



    public void IncrementAnimSpeed()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (aliveEnemsInRow[i] == true)
            {
                enemies[i].GetComponent<EnemyScript>().ChangeAnimationSpeed();
            }
        }
    }
}
