using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{

    public static AudioSource player;
    public static AudioClip[] music;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("musicplayer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static void ChangeMusic(AudioClip s){

        /*if (s >= music.Length){
            print("panicpanicpanicpanicpanicpanic");
        }*/

        //player.Stop();
        //player.clip = s;
        //player.Play();
    }
}
