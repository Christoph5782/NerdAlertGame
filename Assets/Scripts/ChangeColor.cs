using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour{

    public SpriteRenderer img;
    public Image preview;
    public Color hellacolor;
    public InputField input;

    private void Awake(){
        hellacolor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void Update(){
        if (img.color.Equals(preview.color)){
            hellacolor = preview.color;
        }
    }

    private int IsHex(char a){
        if ((a >= '0' && a <= '9') || (a >= 'A' && a <= 'F') || (a >= 'a' && a <= 'f')){
            switch (a){
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'a':
                    return 10;
                case 'b':
                    return 11;
                case 'c':
                    return 12;
                case 'd':
                    return 13;
                case 'e':
                    return 14;
                case 'f':
                    return 15;
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                default:
                    return -1;
            }
        }
        else{
            return -1;
        }
    }

    public float ToHex(char a, char b){
        int c = IsHex(a);
        int d = IsHex(b);
        if(c==17 || d == 17){
            return 0.0f;
        }
        else{
            return (float)(c * 16 + d) / (255);
        }
    }

    public void ChangeHellaColor(string a){
        if(a.Length != 6){
            return;
        }
        char r1 = a[0];
        char r2 = a[1];
        char g1 = a[2];
        char g2 = a[3];
        char b1 = a[4];
        char b2 = a[5];

        float r = ToHex(r1, r2);
        float g = ToHex(g1, g2);
        float b = ToHex(b1, b2);
        hellacolor = new Color(r, g, b, 1.0f);
        img.color = hellacolor;
        preview.color = hellacolor;
        input.text = a;
    }

    public void InputChanged(InputField val){
        string value = val.text;
        ChangeHellaColor(value);
    }

    public void SwitchChar(SpriteRenderer newimg,  Color a)
    {
        //This is to prevent the old styles from going onto the new char - Likely not needed, but meh
        img = null;

        //This is the actual change
        hellacolor = new Color(a.r, a.g, a.b, a.a);
        img = newimg;
    }

    public Color GetColor(){
        return hellacolor;
    }
}
