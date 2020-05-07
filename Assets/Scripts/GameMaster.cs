using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static bool missionsuccess = false;
    public int totalenemies;
    public int totalusers;

    private bool endmission = false;
    //public AudioSource voice;




    public Unit selectedUnit;

    public int playerTurn = 1;

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    void Start()
    {
        CountTroopsAlive();
        missionsuccess = false;
        endmission = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
        if (endmission){
            SceneManager.LoadScene("Campaign");
        }
    }

    private void CountTroopsAlive()
    {
        totalenemies = 0;
        totalusers = 0;

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (unit.playerNumber == 1)
            {
                totalusers++;
            }
            else
            {
                totalenemies++;
            }
        }

        if(totalenemies == 0)
        {
            print("User Wins!");
            missionsuccess = true;
            //play victory audio
            endmission = true;
        }
        else if (totalusers == 0)
        {
            print("User Lost");
            missionsuccess = false;
            //play lost audio
            endmission = true;
        }
    }

    void EndTurn()
    {
        CountTroopsAlive();

        if (playerTurn == 1)
        {
            playerTurn = 2;
        } else if (playerTurn == 2)
        {
            playerTurn = 1;
        }

        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.weaponIcon.SetActive(false);
            unit.hasAttacked = false;
        }
    }

}
