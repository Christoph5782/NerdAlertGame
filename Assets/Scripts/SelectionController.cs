using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionController : MonoBehaviour
{
    private static string operationname;
    private static int[] teamchars = new int[6];
    private static int leadervoice = 6;
    private static int teamleader = 0;

    private int pagenum = 0;
    private readonly int MAXINE = 8;
    private readonly Color DESELECTED = new Color(0f / 255f, 208f / 255f, 255f / 255f, 1.0f);
    private readonly Color SELECTED = new Color(255f / 255f, 175f / 255f, 0f / 255f, 1.0f);
    private readonly string filename = "CharSaveData.json";
    private string filepath;
    private int[] selectedchar = new int[6];
    private int[] displayedpanel = new int[8];
    private int squadsize = 6;

    //Main Objects
    public Text operation;
    public Text objective;
    public Text world;
    public SpriteRenderer[] lgchar0;
    public SpriteRenderer[] lgchar1;
    public SpriteRenderer[] lgchar2;
    public SpriteRenderer[] lgchar3;
    public SpriteRenderer[] lgchar4;
    public SpriteRenderer[] lgchar5;
    public SpriteRenderer[] smchar0;
    public SpriteRenderer[] smchar1;
    public SpriteRenderer[] smchar2;
    public SpriteRenderer[] smchar3;
    public SpriteRenderer[] smchar4;
    public SpriteRenderer[] smchar5;
    public SpriteRenderer[] smweapon0;
    public SpriteRenderer[] smweapon1;
    public SpriteRenderer[] smweapon2;
    public SpriteRenderer[] smweapon3;
    public SpriteRenderer[] smweapon4;
    public SpriteRenderer[] smweapon5;
    public Text[] charname;
    public Text[] charclass;
    public GameObject[] leadericon;

    //Panels
    public GameObject[] charsel0;
    public GameObject[] charsel1;
    public GameObject[] charsel2;
    public GameObject[] charsel3;
    public GameObject[] charsel4;
    public GameObject[] charsel5;
    public GameObject[] blankchar0;
    public GameObject[] blankchar1;
    public GameObject[] blankchar2;
    public GameObject[] blankchar3;
    public GameObject[] blankchar4;
    public GameObject[] blankchar5;
    public SpriteRenderer[] panelchars0;
    public SpriteRenderer[] panelchars1;
    public SpriteRenderer[] panelchars2;
    public SpriteRenderer[] panelchars3;
    public SpriteRenderer[] panelchars4;
    public SpriteRenderer[] panelchars5;
    public Text[] panelcharinfo0;
    public Text[] panelcharinfo1;
    public Text[] panelcharinfo2;
    public Text[] panelcharinfo3;
    public Text[] panelcharinfo4;
    public Text[] panelcharinfo5;
    public GameObject[] nextpagebutton;
    public GameObject[] prevpagebutton;
    public Text[] pagetext;

    //Load in assets
    public SwapStyle[] lgstyle;
    public SwapStyle[] smstyle;
    public string[] classtype;
    public Color[] classcolor;

    CharSaveLoad.GameData gamedata;

    void Awake()
    {
        if (gamedata == null)
        {
            gamedata = new CharSaveLoad.GameData();
        }

        filepath = Path.Combine(Application.dataPath, filename);
    }

    // Start is called before the first frame update
    private void Start()
    {
        FixSortingOrder();
        LoadGameData();
        for (int i=0; i<squadsize; i++){
            selectedchar[i] = i;
        }
        for (int i=0; i< squadsize; i++){
            UpdateLargeChar(i);
            UpdateDisplay(i);
            UpdatePanel(i,0);
        }
        UpdateLeader();
        SetOperationName();
        DisplayMissionOBJ();
        DisplayWorld();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGameData()
    {
        string json;

        if (File.Exists(filepath))
        {
            json = File.ReadAllText(filepath);
            gamedata = JsonUtility.FromJson<CharSaveLoad.GameData>(json);
        }
        else
        {
            CreateDefaultSave();
        }
    }

    void SaveGameData()
    {
        string json = JsonUtility.ToJson(gamedata);
        if (File.Exists(filepath))
        {
            File.Create(filepath).Dispose();
        }
        File.WriteAllText(filepath, json);
    }

    void CreateDefaultSave()
    {
        gamedata.charname.Add("Player Name");
        gamedata.hairtype.Add(1);
        gamedata.hairprim.Add(new Color(31f / 255f, 194f / 255f, 164f / 255f, 1.0f));
        gamedata.hairsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.facialhairtype.Add(2);
        gamedata.facialhairprim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.shirttype.Add(1);
        gamedata.shirtprim.Add(new Color(41f / 255f, 41f / 255f, 41f / 255f, 1.0f));
        gamedata.shirtsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.shirtlogo.Add(new Color(240f / 255f, 240f / 255f, 240f / 255f, 1.0f));
        gamedata.shirttrim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.pantstype.Add(1);
        gamedata.pantscolor.Add(new Color(23f / 255f, 97f / 255f, 189f / 255f, 1.0f));
        gamedata.shoeprim.Add(new Color(69f / 255f, 69f / 255f, 69f / 255f, 1.0f));
        gamedata.shoedec.Add(new Color(51f / 255f, 204f / 255f, 51f / 255f, 1.0f));
        gamedata.acctype.Add(4);
        gamedata.accprim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.accsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.outlinetype.Add(0);
        gamedata.skincolor.Add(new Color(1.0f, 228f / 255f, 207f / 255f, 1.0f));
        gamedata.classtype.Add(1);
        gamedata.weaponprim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.weaponsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.voicetype.Add(4);
        gamedata.charbio.Add("");
        gamedata.status.Add(0);
        gamedata.kills.Add(0);
        gamedata.missions.Add(0);
        gamedata.version.Add(0);
        SaveGameData();
    }


    private void UpdateLargeChar(int a)
    {
        int i = selectedchar[a];
        SpriteRenderer[] lgchar;

        switch (a)
        {
            case 0:
                lgchar = lgchar0;
                break;
            case 1:
                lgchar = lgchar1;
                break;
            case 2:
                lgchar = lgchar2;
                break;
            case 3:
                lgchar = lgchar3;
                break;
            case 4:
                lgchar = lgchar4;
                break;
            case 5:
                lgchar = lgchar5;
                break;
            default:
                Debug.Log("Character number to large: " + a);
                return;
        }


        SwapStyleLG(lgchar[8], lgchar[9], lgchar[10], lgchar[11], lgchar[7], gamedata.hairtype[i], 0);//hair
        SwapStyleLG(lgchar[13], lgchar[14], lgchar[15], lgchar[16], lgchar[12], gamedata.facialhairtype[i], 1);//facial
        SwapStyleLG(lgchar[18], lgchar[19], lgchar[20], lgchar[21], lgchar[17], gamedata.shirttype[i], 2);//shirt
        SwapStyleLG(lgchar[22], lgchar[23], lgchar[24], lgchar[25], lgchar[6], gamedata.pantstype[i], 3);//pants
        SwapStyleLG(lgchar[27], lgchar[28], lgchar[29], lgchar[30], lgchar[26], gamedata.acctype[i], 4);//acc
        //SwapStyleLG(lgchar[32], lgchar[33], lgchar[34], lgchar[35], lgchar[31], gamedata.classtype[i], 5);//class

        SwapColorLG(lgchar[8], gamedata.hairprim[i], 0);
        SwapColorLG(lgchar[9], gamedata.hairsec[i], 1);
        SwapColorLG(lgchar[13], gamedata.facialhairprim[i], 2);
        SwapColorLG(lgchar[18], gamedata.shirtprim[i], 3);
        SwapColorLG(lgchar[19], gamedata.shirtsec[i], 4);
        SwapColorLG(lgchar[20], gamedata.shirtlogo[i], 5);
        SwapColorLG(lgchar[21], gamedata.shirttrim[i], 6);
        SwapColorLG(lgchar[22], gamedata.pantscolor[i], 7);
        SwapColorLG(lgchar[23], gamedata.shoeprim[i], 8);
        SwapColorLG(lgchar[24], gamedata.shoedec[i], 9);
        SwapColorLG(lgchar[27], gamedata.accprim[i], 10);
        SwapColorLG(lgchar[28], gamedata.accsec[i], 11);
        SwapColorLG(lgchar[2], gamedata.skincolor[i], 12);
        //SwapColorLG(lgchar[32], gamedata.weaponprim[i], 13);
        //SwapColorLG(lgchar[33], gamedata.weaponsec[i], 14);

        SwapOutlineLG(lgchar, gamedata.outlinetype[i]);
    }

    private void SwapOutlineLG(SpriteRenderer[] lgchar, int s)
    {
        Color hellacolor;
        if (s == 0){
            hellacolor =  new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else{
            hellacolor =  new Color(51f / 117f, 117f / 255f, 135f / 255f, 1.0f);
        }
        lgchar[0].color = hellacolor;
        lgchar[7].color = hellacolor;
        lgchar[12].color = hellacolor;
        lgchar[17].color = hellacolor;
        lgchar[26].color = hellacolor;
    }
    private void SwapStyleLG(SpriteRenderer prim, SpriteRenderer sec, SpriteRenderer logo, SpriteRenderer trim, SpriteRenderer outln, int a, int s)
    {
        prim.sprite = lgstyle[s].GetSprite(a, 0);
        sec.sprite = lgstyle[s].GetSprite(a, 1);
        logo.sprite = lgstyle[s].GetSprite(a, 2);
        trim.sprite = lgstyle[s].GetSprite(a, 3);
        outln.sprite = lgstyle[s].GetSprite(a, 4);
    }

    private void SwapColorLG(SpriteRenderer img, Color col, int s)
    {
        img.color = col;
    }

    private void UpdatePanel(int panel, int page)
    {
        int[] a = new int[MAXINE];
        int slot = 0;
        int next = -1;
        SpriteRenderer[] smchar;
        Text[] charinfo;

        for (int i = 0; i < gamedata.hairtype.Count && page != next; i++){

            if (slot == 0){
                for(int s=0; s<MAXINE; s++){
                    a[s] = -1;
                }
            }

            a[slot] = i;
            slot++;
            for (int s = 0; s < squadsize; s++){
                if (selectedchar[s] == i){
                    if (s != panel){
                        a[slot] = -1;
                        slot--;
                    }
                }
            }
            if (slot == MAXINE){
                slot = 0;
                next++;
            }
        }


        if(a[MAXINE-1] != -1 && a[MAXINE-1] < gamedata.hairtype.Count - 1){
            nextpagebutton[panel].SetActive(true);
        }
        else{
            nextpagebutton[panel].SetActive(false);
        }


        if(page != 0){
            prevpagebutton[panel].SetActive(true);
        }
        else{
            prevpagebutton[panel].SetActive(false);
        }

        pagetext[panel].text = "Page " + (page + 1).ToString();



        for (int i = 0; i < MAXINE && a[i] != -1; i++){
            switch (panel){
                case 0:
                    smchar = panelchars0;
                    charinfo = panelcharinfo0;
                    break;
                case 1:
                    smchar = panelchars1;
                    charinfo = panelcharinfo1;
                    break;
                case 2:
                    smchar = panelchars2;
                    charinfo = panelcharinfo2;
                    break;
                case 3:
                    smchar = panelchars3;
                    charinfo = panelcharinfo3;
                    break;
                case 4:
                    smchar = panelchars4;
                    charinfo = panelcharinfo4;
                    break;
                case 5:
                    smchar = panelchars5;
                    charinfo = panelcharinfo5;
                    break;
                default:
                    Debug.Log("Character number to large: " + a);
                    return;
            }//End Switch


            SwapStyleSM(smchar[8 + 37 * i], smchar[9 + 37 * i], smchar[10 + 37 * i], smchar[11 + 37 * i], smchar[7 + 37 * i], gamedata.hairtype[a[i]], 0);//hair
            SwapStyleSM(smchar[13 + 37 * i], smchar[14 + 37 * i], smchar[15 + 37 * i], smchar[16 + 37 * i], smchar[12 + 37 * i], gamedata.facialhairtype[a[i]], 1);//facial
            SwapStyleSM(smchar[18 + 37 * i], smchar[19 + 37 * i], smchar[20 + 37 * i], smchar[21 + 37 * i], smchar[17 + 37 * i], gamedata.shirttype[a[i]], 2);//shirt
            SwapStyleSM(smchar[22 + 37 * i], smchar[23 + 37 * i], smchar[24 + 37 * i], smchar[25 + 37 * i], smchar[6 + 37 * i], gamedata.pantstype[a[i]], 3);//pants
            SwapStyleSM(smchar[27 + 37 * i], smchar[28 + 37 * i], smchar[29 + 37 * i], smchar[30 + 37 * i], smchar[26 + 37 * i], gamedata.acctype[a[i]], 4);//acc
            SwapStyleSM(smchar[31 + 37 * i], smchar[3 + 37 * i], smchar[4 + 37 * i], smchar[5 + 37 * i], smchar[6 + 37 * i], gamedata.classtype[a[i]], 5);//class
            SwapStyleSM(smchar[32 + 37 * i], smchar[3 + 37 * i], smchar[4 + 37 * i], smchar[5 + 37 * i], smchar[6 + 37 * i], gamedata.status[a[i]], 6);//status

            SwapColorSM(smchar[8 + 37 * i], gamedata.hairprim[a[i]]);
            SwapColorSM(smchar[9 + 37 * i], gamedata.hairsec[a[i]]);
            SwapColorSM(smchar[13 + 37 * i], gamedata.facialhairprim[a[i]]);
            SwapColorSM(smchar[18 + 37 * i], gamedata.shirtprim[a[i]]);
            SwapColorSM(smchar[19 + 37 * i], gamedata.shirtsec[a[i]]);
            SwapColorSM(smchar[20 + 37 * i], gamedata.shirtlogo[a[i]]);
            SwapColorSM(smchar[21 + 37 * i], gamedata.shirttrim[a[i]]);
            SwapColorSM(smchar[22 + 37 * i], gamedata.pantscolor[a[i]]);
            SwapColorSM(smchar[23 + 37 * i], gamedata.shoeprim[a[i]]);
            SwapColorSM(smchar[24 + 37 * i], gamedata.shoedec[a[i]]);
            SwapColorSM(smchar[27 + 37 * i], gamedata.accprim[a[i]]);
            SwapColorSM(smchar[28 + 37 * i], gamedata.accsec[a[i]]);
            SwapColorSM(smchar[2 + 37 * i], gamedata.skincolor[a[i]]);

            SwapOutlineSM(smchar, gamedata.outlinetype[a[i]], i);//outline

            charinfo[0 + 4 * i].text = gamedata.charname[a[i]];
            charinfo[1 + 4 * i].text = gamedata.kills[a[i]].ToString();
            charinfo[2 + 4 * i].text = gamedata.missions[a[i]].ToString();
            charinfo[3 + 4 * i].text = "v" + gamedata.version[a[i]].ToString() + ".0";

            smchar[33 + 37 * i].color = DESELECTED;
            smchar[34 + 37 * i].color = DESELECTED;
            smchar[35 + 37 * i].color = DESELECTED;
            smchar[36 + 37 * i].color = DESELECTED;

        }//End For loop
        //UpdateSelected(selectedchar);
        for(int i=0; i<MAXINE; i++)
        {
            displayedpanel[i] = a[i];
        }
    }

    private void SwapOutlineSM(SpriteRenderer[] smchar, int s, int i)
    {
        Color hellacolor;
        if (s == 0){
            hellacolor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else{
            hellacolor = new Color(51f / 117f, 117f / 255f, 135f / 255f, 1.0f);
        }
        smchar[0 + 37 * i].color = hellacolor;
        smchar[7 + 37 * i].color = hellacolor;
        smchar[12 + 37 * i].color = hellacolor;
        smchar[17 + 37 * i].color = hellacolor;
        smchar[26 + 37 * i].color = hellacolor;
    }

    private void SwapStyleSM(SpriteRenderer prim, SpriteRenderer sec, SpriteRenderer logo, SpriteRenderer trim, SpriteRenderer outln, int a, int s)
    {
        prim.sprite = smstyle[s].GetSprite(a, 0);
        sec.sprite = smstyle[s].GetSprite(a, 1);
        logo.sprite = smstyle[s].GetSprite(a, 2);
        trim.sprite = smstyle[s].GetSprite(a, 3);
        outln.sprite = smstyle[s].GetSprite(a, 4);
    }

    private void SwapColorSM(SpriteRenderer img, Color col)
    {
        img.color = col;
    }

    public void UpdateDisplay(int a){
        int i = selectedchar[a];
        SpriteRenderer[] smchar;
        SpriteRenderer[] smweapon;

        switch (a)
        {
            case 0:
                smchar = smchar0;
                smweapon = smweapon0;
                break;
            case 1:
                smchar = smchar1;
                smweapon = smweapon1;
                break;
            case 2:
                smchar = smchar2;
                smweapon = smweapon2;
                break;
            case 3:
                smchar = smchar3;
                smweapon = smweapon3;
                break;
            case 4:
                smchar = smchar4;
                smweapon = smweapon4;
                break;
            case 5:
                smchar = smchar5;
                smweapon = smweapon5;
                break;
            default:
                Debug.Log("Character number to large: " + a);
                return;
        }


        SwapStyleSM(smchar[8], smchar[9], smchar[10], smchar[11], smchar[7], gamedata.hairtype[i], 0);//hair
        SwapStyleSM(smchar[13], smchar[14], smchar[15], smchar[16], smchar[12], gamedata.facialhairtype[i], 1);//facial
        SwapStyleSM(smchar[18], smchar[19], smchar[20], smchar[21], smchar[17], gamedata.shirttype[i], 2);//shirt
        SwapStyleSM(smchar[22], smchar[23], smchar[24], smchar[25], smchar[6], gamedata.pantstype[i], 3);//pants
        SwapStyleSM(smchar[27], smchar[28], smchar[29], smchar[30], smchar[26], gamedata.acctype[i], 4);//acc
        SwapStyleLG(smweapon[1], smweapon[2], smweapon[3], smweapon[4], smweapon[0], gamedata.classtype[i], 5);//Weapon - Yes, the LG is intentional
   
        SwapColorSM(smchar[8], gamedata.hairprim[i]);
        SwapColorSM(smchar[9], gamedata.hairsec[i]);
        SwapColorSM(smchar[13], gamedata.facialhairprim[i]);
        SwapColorSM(smchar[18], gamedata.shirtprim[i]);
        SwapColorSM(smchar[19], gamedata.shirtsec[i]);
        SwapColorSM(smchar[20], gamedata.shirtlogo[i]);
        SwapColorSM(smchar[21], gamedata.shirttrim[i]);
        SwapColorSM(smchar[22], gamedata.pantscolor[i]);
        SwapColorSM(smchar[23], gamedata.shoeprim[i]);
        SwapColorSM(smchar[24], gamedata.shoedec[i]);
        SwapColorSM(smchar[27], gamedata.accprim[i]);
        SwapColorSM(smchar[28], gamedata.accsec[i]);
        SwapColorSM(smchar[2], gamedata.skincolor[i]);
        SwapColorSM(smweapon[1], gamedata.weaponprim[i]);
        SwapColorSM(smweapon[2], gamedata.weaponsec[i]);

        SwapOutlineSM(smchar, gamedata.outlinetype[i], 0);

        charname[a].text = gamedata.charname[i];
        charclass[a].text = classtype[gamedata.classtype[i]];
        charclass[a].color = classcolor[gamedata.classtype[i]];
    }

    public void UpdateLeader(){
        int leaderindex = 0;
        int highlvl = gamedata.version[selectedchar[0]];
        for(int i=1; i<squadsize; i++){
            if(gamedata.version[selectedchar[i]] > highlvl){
                highlvl = gamedata.version[selectedchar[i]];
                leaderindex = i;
            }
        }
        for(int i=0; i<squadsize; i++){
            if (i == leaderindex){
                leadericon[i].SetActive(true);
            }
            else{
                leadericon[i].SetActive(false);
            }
        }
        teamleader = selectedchar[leaderindex];
        leadervoice = gamedata.voicetype[teamleader];
    }

    public void ChangeChar(int s){
        int slot = s / 10;
        int num = s % 10;

        selectedchar[slot] = displayedpanel[num];
        UpdateLargeChar(slot);
        UpdateDisplay(slot);
        UpdateLeader();
    }

    public void LaunchGame() { 
        for (int i = 0; i < 6; i++) {
            teamchars[i] = selectedchar[i];
        }
        leadervoice = gamedata.voicetype[teamleader];
        SceneManager.LoadScene("MissionBriefing");
    }

    public void ReturnToCamp(){
        SceneManager.LoadScene("Campaign");
    }
    public static int[] GetSelectedCharArray(){
        return teamchars;
    }
    public static int GetSelectedCharPerson(int s){
        return teamchars[s];
    }

    public void OpenPanel(int s){
        pagenum = 0;
        UpdatePanel(s, 0);
    }

    public void NextPage(int s)
    {
        pagenum++;
        UpdatePanel(s, pagenum);
    }

    public void PrevPage(int s)
    {
        pagenum--;
        UpdatePanel(s, pagenum);
    }

    public void SetOperationName(){

        string[] adjs = {"Enduring", "Hella", "Teal", "Sunless", "Autumn", "Hidden", "Bitter", "Misty", "Silent", "Empty", "Dry", "Dark", "Summer", "Icy", "Delicate", "Quiet", "White", "Cool", "Spring", "Winter", "Patient", "Twilight", "Dawn", "Crimson", "Wispy", "Weathered", "Blue", "Billowing", "Broken", "Cold", "Damp", "Falling", "Frosty", "Green", "Long", "Late", "Lingering", "Bold", "Little", "Morning", "Muddy", "Old", "Red", "Rough", "Still", "Small", "Sparkling", "Throbbing", "Shy", "Wandering", "Withered", "Wild", "Black", "Young", "Holy", "Solitary", "Fragrant", "Aged", "Snowy", "Proud", "Floral", "Restless", "Divine", "Polished", "Ancient", "Purple", "Lively", "Nameless" };
        string[] nouns = {"Humm", "Waterfall", "River", "Breeze", "Moon", "Rain", "Wind", "Sea", "Morning", "Snow", "Lake", "Sunset", "Pine", "Shadow", "Leaf", "Dawn", "Glitter", "Forest", "Hill", "Cloud", "Meadow", "Sun", "Glade", "Bird", "Brook", "Butterfly", "Bush", "Dew", "Dust", "Field", "Fire", "Flower", "Firefly", "Feather", "Grass", "Haze", "Mountain", "Night", "Pond", "Darkness", "Snowflake", "Silence", "Sound", "Sky", "Shape", "Surf", "Thunder", "Violet", "Water", "Wildflower", "Wave", "Water", "Resonance", "Khan", "Wood", "Dream", "Cherry", "Tree", "Fog", "Frost", "Voice", "Paper", "Frog", "Smoke", "Star" };
        int r = Random.Range(0, adjs.Length);
        int s = Random.Range(0, nouns.Length);

        operation.text = "Operation " + adjs[r] + " " + nouns[s];
        operationname = operation.text;//I don't know if this works or not yet


    }
    public void DisplayMissionOBJ(){

        if (CampaignController.GetNumber() == 8){
            switch (CampaignController.GetWorld()){
                case 0:
                    objective.text = "Eliminate All Enemies";
                    break;
                case 1:
                    objective.text = "Outlast the Onslaught";
                    break;
                case 2:
                    objective.text = "Destory the Infected Directory";
                    break;
                case 3:
                    objective.text = "Protect the File";
                    break;
                case 4:
                    objective.text = "Escort the VIP";
                    break;
                case 5:
                    objective.text = "Escort the VIP";
                    break;
                default:
                    objective.text = "Eliminate All Enemies";
                    break;
            }
        }


        switch (CampaignController.GetMissionType())
        {
            case 0:
                objective.text = "Eliminate All Enemies";
                break;
            case 1:
                objective.text = "Outlast the Onslaught";
                break;
            case 2:
                objective.text = "Destory the Infected Directory";
                break;
            case 3:
                objective.text = "Protect the File";
                break;
            case 4:
                objective.text = "Escort the VIP";
                break;
            default:
                objective.text = "Eliminate All Enemies";
                break;

        }
    }
    public void DisplayWorld(){
        switch (CampaignController.GetWorld())
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
    }
    private void FixSortingOrder(){
        for(int i=0; i<smchar0.Length; i++){
            smchar0[i].sortingOrder += 10;
            smchar1[i].sortingOrder += 10;
            smchar2[i].sortingOrder += 10;
            smchar3[i].sortingOrder += 10;
            smchar4[i].sortingOrder += 10;
            smchar5[i].sortingOrder += 10;
        }
        for(int i=0; i< lgchar0.Length; i++){
            lgchar0[i].sortingOrder += 10;
            lgchar1[i].sortingOrder += 10;
            lgchar2[i].sortingOrder += 10;
            lgchar3[i].sortingOrder += 10;
            lgchar4[i].sortingOrder += 10;
            lgchar5[i].sortingOrder += 10;
        }
        for (int i=0; i< smweapon0.Length; i++){
            smweapon0[i].sortingOrder += 10;
            smweapon1[i].sortingOrder += 10;
            smweapon2[i].sortingOrder += 10;
            smweapon3[i].sortingOrder += 10;
            smweapon4[i].sortingOrder += 10;
            smweapon5[i].sortingOrder += 10;
        }
        for (int i=0; i< panelchars0.Length; i++){
            panelchars0[i].sortingOrder += 10;
            panelchars1[i].sortingOrder += 10;
            panelchars2[i].sortingOrder += 10;
            panelchars3[i].sortingOrder += 10;
            panelchars4[i].sortingOrder += 10;
            panelchars5[i].sortingOrder += 10;
        }
    }

    public static string GetOpName(){
        return operationname;
    }

    public static int GetLeaderVoice(){
        return leadervoice;
    }
}
