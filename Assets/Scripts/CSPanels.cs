using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSPanels : MonoBehaviour
{
    public GameObject[] panel;
    public GameObject[] character;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].SetActive(false);
            character[i].SetActive(true);
        }
    }

    public void OpenPanel(int chloe)
    {
        for (int i = 0; i < panel.Length; i++)
        {
            if (i != chloe)
            {
                panel[i].SetActive(false);
                character[i].SetActive(true);
            }
            else
            {
                panel[chloe].SetActive(!panel[chloe].activeSelf);
                character[chloe].SetActive(!character[chloe].activeSelf);
            }
        }

    }

    public void ClosePanel(int chloe)
    {
        panel[chloe].SetActive(false);
        character[chloe].SetActive(true);
    }
}
