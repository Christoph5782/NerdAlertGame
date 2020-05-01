using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOutline : MonoBehaviour
{
    public SpriteRenderer[] img;
    private Color white;
    public Color hellacolor;
    private int num = 0;
    public Image preview;
    public Sprite[] selected;

    private void Awake()
    {
        white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        hellacolor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        preview.sprite = selected[num];
    }

    private void Update()
    {
        if (img[0].color.Equals(img[img.Length - 1].color))
        {
            hellacolor = img[0].color;
            if (hellacolor.Equals(white)){
                num = 0;
            }
            else{
                num = 1;
            }
        }
    }

    public Color GetColor(int s)
    {
        if (s == 0){
            return new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else{
            return new Color(51f / 117f, 117f / 255f, 135f / 255f, 1.0f);
        }
    }

    public Sprite GetPreview(int a){
        return selected[a];
    }

    public int GetNum(){
        return num;
    }

    public void ChangeStyle(int s)
    {
        num = s;
        preview.sprite = selected[num];
        if (s == 0){
            hellacolor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else{
            hellacolor = new Color(51f/117f, 117f/255f, 135f/255f, 1.0f);
        }
        for (int i=0; i < img.Length; i++){
            img[i].color = hellacolor;
        }
    }
}