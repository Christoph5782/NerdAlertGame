using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{

    public static AudioSource player;
    public static AudioClip[] music;

    public AudioSource playerinput;
    public AudioClip[] musicinput;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("musicplayer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        player = playerinput;
        music = musicinput;
    }

    public static void ChangeMusic(int s){

        if (s >= music.Length){
            print("panicpanicpanicpanicpanicpanic");
        }

        player.Stop();
        player.clip = music[s];
        player.Play();
    }
}
