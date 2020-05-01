using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour
{
    public GameObject[] panel;
    // Start is called before the first frame update
    void Start(){
        for(int i=0; i<panel.Length; i++){
            panel[i].SetActive(false);
        }
    }

    public void OpenPanel(int chloe){
        for (int i = 0; i < panel.Length; i++){
            if (i != chloe){
                panel[i].SetActive(false);
            }
            else{
                panel[chloe].SetActive(true);
            }
        }
        
    }

    public void ClosePanel(int chloe){
        panel[chloe].SetActive(false);
    }
}
