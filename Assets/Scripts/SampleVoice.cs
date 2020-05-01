using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleVoice : MonoBehaviour
{
    public int num;
    public Image preview;
    public Sprite[] selected;
    public AudioSource ctrl;
    public AudioClip[] voices;
    // Start is called before the first frame update
    void Awake()
    {
        num = 0;
        preview.sprite = selected[num];
    }

    // Update is called once per frame
    void Update(){
        for (int i = 0; i < selected.Length; i++){
            if (selected[i].Equals(preview.sprite)){
                num = i;
            }
        }
    }

    public void ChangeStyle(int s)
    {
        num = s;
        preview.sprite = selected[num];
    }

    public void PlayVoice(int s){
        int r = Random.Range(0, 8);//0 is inclusive, 8 is exclusive, don't know why
        if (ctrl.isPlaying){
            ctrl.Stop();
        }
        ctrl.clip = voices[s*8 + r];
        ctrl.Play();
    }

    public int GetNum()
    {
        return num;
    }

    public Sprite GetPreview(int a)
    {
        return selected[a];
    }
}
