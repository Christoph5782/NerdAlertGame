using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionBriefingController : MonoBehaviour
{
    private int timer;

    public Text opname;
    public Text world;
    public Text missionnumber;
    public Text missionobjective;
    public Image missiondisp;
    public Button launchbtn;
    public GameObject disabledbtn;
    public AudioSource voice;

    public string[] objs;
    public string[] bossobjs;
    public Sprite[] missionimgs;
    public AudioClip[] voicelines;
    public AudioClip[] bosslines;
    public AudioClip[] missionmusic;

    private void Awake(){
        opname.text = SelectionController.GetOpName();
        int i = CampaignController.GetNumber();
        int w = CampaignController.GetWorld();
        int t = CampaignController.GetMissionType();
        int s = SelectionController.GetLeaderVoice();

        switch (w)
        {
            case 0:
                world.text = "Tutorial World";
                break;
            case 1:
                world.text = "World 1";
                break;
            case 2:
                world.text = "World 2";
                break;
            case 3:
                world.text = "World 3";
                break;
            case 4:
                world.text = "World 4";
                break;
            case 5:
                world.text = "Final World";
                break;
            default:
                world.text = "Tutorial World";
                break;

        }

        missiondisp.sprite = missionimgs[w];
        launchbtn.interactable = false;
        disabledbtn.SetActive(true);
        int r;

        if(i != 8) {
            missionnumber.text = "Mission " + ((i%4)+1).ToString();
            switch (t)
            {
                case 0:
                    r = Random.Range(0, 3);
                    voice.clip = voicelines[r + (s * 12)];
                    missionobjective.text = objs[r];
                    break;
                case 1:
                    r = Random.Range(0, 3);
                    voice.clip = voicelines[3 + r + (s * 12)];
                    missionobjective.text = objs[3+r];
                    break;
                case 2:
                    r = Random.Range(0, 3);
                    voice.clip = voicelines[6 + r + (s * 12)];
                    missionobjective.text = objs[6+r];
                    break;
                case 3:
                    r = Random.Range(0, 2);
                    voice.clip = voicelines[9 + r + (s * 12)];
                    missionobjective.text = objs[9+r];
                    break;
                case 4:
                    r = Random.Range(0, 2);
                    voice.clip = voicelines[10 + r + (s * 12)];
                    missionobjective.text = objs[10+r];
                    break;
                default:
                    voice.clip = voicelines[2 + (s * 12)];
                    missionobjective.text = objs[2];
                    break;
            }

        }
        else{
            missionnumber.text = "Final Mission";
            voice.clip = bosslines[w + (s * 6)];
            missionobjective.text = bossobjs[w];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 600;
        voice.Play();
        int w = CampaignController.GetWorld();
        if(w == 0 || w == 5)
        {
            w = 2;
        }
        DontDestroyMusic.ChangeMusic(w);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0){
            timer--;
        }
        if(timer == 0){
            timer = -1;
            launchbtn.interactable = true;
            disabledbtn.SetActive(false);
        }
    }

    public void LaunchMission(){
        SceneManager.LoadScene("Game");
    }
}
