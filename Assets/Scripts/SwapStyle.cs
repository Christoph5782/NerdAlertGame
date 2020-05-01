using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapStyle : MonoBehaviour
{
    public SpriteRenderer primimg;
    public SpriteRenderer secimg;
    public SpriteRenderer logoimg;
    public SpriteRenderer trimimg;
    public SpriteRenderer outlineimg;
    public Sprite[] primstyle;
    public Sprite[] secstyle;
    public Sprite[] logostyle;
    public Sprite[] trimstyle;
    public Sprite[] outline;
    public int num;
    public Image preview;
    public Sprite[] selected;

    // Start is called before the first frame update
    void Awake(){
        primimg.sprite = primstyle[num];
        secimg.sprite = secstyle[num];
        logoimg.sprite = logostyle[num];
        trimimg.sprite = trimstyle[num];
        outlineimg.sprite = outline[num];
        preview.sprite = selected[num];
    }

    // Update is called once per frame
    void Update(){
        for(int i=0; i<primstyle.Length; i++){
            if(primstyle[i].Equals(primimg.sprite) && selected[i].Equals(preview.sprite)){
                num = i;
            }
        }
    }

    public void ChangeStyle(int s){
        num = s;
        primimg.sprite = primstyle[num];
        secimg.sprite = secstyle[num];
        logoimg.sprite = logostyle[num];
        trimimg.sprite = trimstyle[num];
        outlineimg.sprite = outline[num];
        preview.sprite = selected[num];

        
    }

    public void SwitchChar(SpriteRenderer prim, SpriteRenderer sec, SpriteRenderer logo, SpriteRenderer trim, SpriteRenderer outln, int a){
        //This is to prevent the old styles from going onto the new char - Likely not needed, but meh
        primimg = null;
        secimg = null;
        logoimg = null;
        trimimg = null;
        outlineimg = null;

        //This is the actual change
        num = a;
        primimg = prim;
        secimg = sec;
        logoimg = logo;
        trimimg = trim;
        outlineimg = outln;
    }

    public Sprite GetSprite(int a, int type){
        switch (type){
            case 0:
                return primstyle[a];
            case 1:
                return secstyle[a];
            case 2:
                return logostyle[a];
            case 3:
                return trimstyle[a];
            case 4:
                return outline[a];
            case 5:
                return selected[a];
            default:
                return null;
        }
    }

    public int GetNum(){
        return num;
    }
}
