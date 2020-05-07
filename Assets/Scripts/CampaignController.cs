using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CampaignController : MonoBehaviour
{
    private static int missiontype = 0;
    private static int worldnum = 1;
    private static int missionnum = 0;

    private int selected = -1;
    private int count = 0;
    private readonly string filename = "CampaignProgress.json";
    private string filepath;
    private readonly Color DESELECTED = new Color(0f / 255f, 208f / 255f, 255f / 255f, 1.0f);
    private readonly Color SELECTED = new Color(255f / 255f, 175f / 255f, 0f / 255f, 1.0f);
    private readonly Color COMPLETED = new Color(0f / 255f, 255f / 255f, 47f / 255f, 1.0f);
    private List<bool> world;

    public Image dispbg;
    public Text worldtext;
    public Text objtext;
    public Image logo;
    public Sprite[] background;
    public string[] worldname;
    public string[] worldobj;
    public Sprite[] logoimgs;
    public GameObject enemies;
    public Button[] lvlselect;
    public Image[] lvlborder;
    public GameObject[] lvldisabled;
    public GameObject disablelaunch;
    public Button launchbutton;
    public GameObject nextbutton;
    public GameObject prevbutton;
    public Text[] missiontext;


    public int[] tutorialworld;
    public int[] world1;
    public int[] world2;
    public int[] world3;
    public int[] world4;
    public int[] finalworld;
    public int[] finalbattletype;
    public string[] missiondisc;

    CampaignData campaigndata;

    private void Awake()
    {
        if (campaigndata == null){
            campaigndata = new CampaignData();
        }

        filepath = Path.Combine(Application.dataPath, filename);
    }

    // Start is called before the first frame update
    void Start(){
        LoadGameData();
        worldnum = FindStartPage();
        LoadWorld(worldnum);
        if(GameMaster.missionsuccess == true){
            MarkComplete();
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies.transform.Translate(0.01f, 0.0f, 0.0f);
        count++;
        if (count >= 1235)
        {
            enemies.transform.Translate(count * -0.01f, 0.0f, 0.0f);
            count = 0;
        }
    }

    private int FindStartPage(){
        if (!campaigndata.worldT[8]){
            return 0;
        }
        if (!campaigndata.world1[8]){
            return 1;
        }
        if (!campaigndata.world2[8]){
            return 2;
        }
        if (!campaigndata.world3[8]){
            return 3;
        }
        if (!campaigndata.world4[8]){
            return 4;
        }
        return 5;
    }

    private void LoadWorld(int w){

        switch (w)
        {
            case 0:
                world = campaigndata.worldT;
                break;
            case 1:
                world = campaigndata.world1;
                break;
            case 2:
                world = campaigndata.world2;
                break;
            case 3:
                world = campaigndata.world3;
                break;
            case 4:
                world = campaigndata.world4;
                break;
            case 5:
                world = campaigndata.worldF;
                break;
            default:
                print("panicpanicpanicpanicpanicpanicpanic");
                world = campaigndata.worldT;
                w = 0;
                break;
        }

        dispbg.sprite = background[w];
        worldtext.text = worldname[w];
        objtext.text = worldobj[w];
        logo.sprite = logoimgs[w];
        UpdateGametypes(w);

        if (worldnum == 5 || (worldnum == 4 && (!campaigndata.world1[8] || !campaigndata.world2[8] || !campaigndata.world3[8] || !campaigndata.world4[8])) || worldnum == 0 && !campaigndata.worldT[8]){
            nextbutton.SetActive(false);
        }
        else{
            nextbutton.SetActive(true);
        }

        if (worldnum == 0){
            prevbutton.SetActive(false);
        }
        else{
            prevbutton.SetActive(true);
        }

        ResetBorders(world);
        for (int i = 0; i < 9; i++){
            lvlselect[i].interactable = false;
            lvldisabled[i].SetActive(true);
        }
        lvlselect[0].interactable = true;
        lvldisabled[0].SetActive(false);
        lvlselect[4].interactable = true;
        lvldisabled[4].SetActive(false);

        if(world[0] || world[4]){
            lvlselect[1].interactable = true;
            lvldisabled[1].SetActive(false);
            lvlselect[5].interactable = true;
            lvldisabled[5].SetActive(false);
        }
        else{
            return;
        }

        if(world[1] || world[5]){
            lvlselect[2].interactable = true;
            lvldisabled[2].SetActive(false);
            lvlselect[6].interactable = true;
            lvldisabled[6].SetActive(false);
        }
        else{
            return;
        }

        if(world[2] || world[6]){
            lvlselect[3].interactable = true;
            lvldisabled[3].SetActive(false);
            lvlselect[7].interactable = true;
            lvldisabled[7].SetActive(false);
        }
        else{
            return;
        }

        if(world[3] || world[7]){
            lvlselect[8].interactable = true;
            lvldisabled[8].SetActive(false);
        }
        return;
    }

    private void UpdateGametypes(int w){

        int[] a;

        switch (w){
            case 0:
                a = tutorialworld;
                break;
            case 1:
                a = world1;
                break;
            case 2:
                a = world2;
                break;
            case 3:
                a = world3;
                break;
            case 4:
                a = world4;
                break;
            case 5:
                a = finalworld;
                break;
            default:
                print("panicpanicpanicpanicpanicpanicpanic");
                a = tutorialworld;
                w = 0;
                break;
        }

        for(int i=0; i<8; i++){
            missiontext[i].text = missiondisc[a[i]];
        }
    }

    private void ResetBorders(List<bool> world)
    {
        for(int i=0; i<9; i++){
            lvlborder[i].color = DESELECTED;
            if (world[i]){
                lvlborder[i].color = COMPLETED;
            }
        }
        selected = -1;
        launchbutton.interactable = false;
        disablelaunch.SetActive(true);
    }

    private void MarkComplete(){

        int w = worldnum;

        switch (w)
        {
            case 0:
                world = campaigndata.worldT;
                break;
            case 1:
                world = campaigndata.world1;
                break;
            case 2:
                world = campaigndata.world2;
                break;
            case 3:
                world = campaigndata.world3;
                break;
            case 4:
                world = campaigndata.world4;
                break;
            case 5:
                world = campaigndata.worldF;
                break;
            default:
                print("panicpanicpanicpanicpanicpanicpanic");
                world = campaigndata.worldT;
                w = 0;
                break;
        }

        world[missionnum] = true;
    }

    public void LoadGameData()
    {
        string json;

        if (File.Exists(filepath))
        {
            json = File.ReadAllText(filepath);
            campaigndata = JsonUtility.FromJson<CampaignData>(json);
        }
        else
        {
            CreateDefaultSave();
        }
    }

    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(campaigndata);
        if (File.Exists(filepath))
        {
            File.Create(filepath).Dispose();
        }
        File.WriteAllText(filepath, json);
    }

    public void CreateDefaultSave(){

        for (int i = 0; i<9; i++){
            campaigndata.worldT.Add(false);
            campaigndata.world1.Add(false);
            campaigndata.world2.Add(false);
            campaigndata.world3.Add(false);
            campaigndata.world4.Add(false);
            campaigndata.worldF.Add(false);
        }

        SaveGameData();
    }


    public void MissionSelect(int s){
        ResetBorders(world);
        launchbutton.interactable = true;
        disablelaunch.SetActive(false);
        lvlborder[s].color = SELECTED;
        selected = s;
    }

    public void NextWorld(){
        worldnum++;
        LoadWorld(worldnum);
    }
    public void PrevWorld(){
        worldnum--;
        LoadWorld(worldnum);
    }


    public void ToEditor(){
        SceneManager.LoadScene("CharaterCreation");
    }
    public void ToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void ToSelection(){
        UpdateStaticVar();
        SceneManager.LoadScene("CharacterSelection");
    }

    private void UpdateStaticVar(){

        missionnum = selected;

        if(missionnum == 8){
            missiontype = finalbattletype[worldnum];
            return;
        }

        switch (worldnum){
            case 0:
                missiontype = tutorialworld[missionnum];
                break;
            case 1:
                missiontype = world1[missionnum];
                break;
            case 2:
                missiontype = world2[missionnum];
                break;
            case 3:
                missiontype = world3[missionnum];
                break;
            case 4:
                missiontype = world4[missionnum];
                break;
            case 5:
                missiontype = finalworld[missionnum];
                break;
            default:
                print("panicpanicpanicpanicpanicpanicpanic");
                missiontype = tutorialworld[missionnum];
                worldnum = 0;
                break;
        }
    }
    public static int GetMissionType(){
        return missiontype;
    }
    public static int GetWorld(){
        return worldnum;
    }
    public static int GetNumber(){
        return missionnum;
    }

    [System.Serializable]
    public class CampaignData
    {
        public List<bool> worldT = new List<bool>();
        public List<bool> world1 = new List<bool>();
        public List<bool> world2 = new List<bool>();
        public List<bool> world3 = new List<bool>();
        public List<bool> world4 = new List<bool>();
        public List<bool> worldF = new List<bool>();
    }
}
