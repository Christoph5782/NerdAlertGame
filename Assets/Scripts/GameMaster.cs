using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static bool missionsuccess = false;
    public static AudioSource voice;
    public static AudioSource sfx;
    public static VoicePackage[] package;
    public int totalenemies;
    public int totalusers;
    public static Sprite[] tempsprite;

    private bool endmission = false;
    private static int afktimer;
    public AudioSource voiceinput;
    public AudioSource sfxinput;
    public VoicePackage[] packageinput;
    public Sprite[] tempspriteinput;




    public Unit selectedUnit;

    public int playerTurn = 1;

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }
    private void Awake()
    {
        voice = voiceinput;
        sfx = sfxinput;
        package = packageinput;
        tempsprite = tempspriteinput;
    }

    void Start()
    {
        CountTroopsAlive();
        missionsuccess = false;
        endmission = false;
        afktimer = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
        if (endmission && voice.isPlaying == false){
            DontDestroyMusic.ChangeMusic(0);
            SceneManager.LoadScene("Campaign");
        }
        afktimer++;
        if(afktimer == 10000){
            if (playerTurn == 1){
                int i = Random.Range(0, 6);
                int r = SelectionController.GetCharVoice(i);
                if (r == -1)
                {
                    r = SelectionController.GetLeaderVoice();
                }
                voice.Stop();
                voice.clip = package[r].Getuserafk();
                voice.Play();
            }
            afktimer = 0;
        }
    }

    public static void ResetAFK()
    {
        afktimer = 0;
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
            voice.Stop();
            voice.clip = package[SelectionController.GetLeaderVoice()].GetalltargetkiaE();
            voice.Play();
            endmission = true;
        }
        else if (totalusers == 0)
        {
            print("User Lost");
            missionsuccess = false;
            voice.Stop();
            voice.clip = package[SelectionController.GetLeaderVoice()].Gettroopsextracted();
            voice.Play();
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
