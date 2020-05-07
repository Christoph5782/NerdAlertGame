using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject youWinText;
    public GameObject youLoseText;
    public GameObject returnMenuButton;
    public GameObject tileScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

        youWinText.SetActive(false);
        youLoseText.SetActive(false);
        tileScreen.SetActive(false);
        returnMenuButton.SetActive(false);
    }

    public void Win()
    {
        youWinText.SetActive(true);
        returnMenuButton.SetActive(true);
        tileScreen.SetActive(true);

    }

    public void Lose()
    {
        youLoseText.SetActive(true);
        returnMenuButton.SetActive(true);
        tileScreen.SetActive(true);
    }
}
