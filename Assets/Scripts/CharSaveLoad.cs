using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CharSaveLoad : MonoBehaviour
{
    private int pagenum = 0;
    private readonly int MAXINE = 10;
    private readonly Color DESELECTED = new Color(0f/255f, 208f/255f, 255f/255f, 1.0f);
    private readonly Color SELECTED = new Color(255f/255f, 175f/255f, 0f/255f, 1.0f);
    private readonly string filename = "CharSaveData.json";
    private string filepath;
    public static CharSaveLoad _instance;
    public SwapStyle[] style;
    public ChangeColor[] color;
    public SwapStyle[] smstyle;
    public ChangeColor[] smcolor;
    public SpriteRenderer[] smchar;
    public int selectedchar;
    public SpriteRenderer[] lgchar;
    public Image[] previewstyle;
    public Image[] previewcolor;
    private int stagger;
    public InputField[] colorinput;
    public InputField charname;
    public InputField charbio;
    public Text[] charinfo;
    public Text lgname;
    public GameObject[] charsel;
    public GameObject[] addchar;
    public GameObject[] blankchar;
    public GameObject lgchardisp;
    public GameObject lgweapondisp;
    public GameObject nextpagebutton;
    public GameObject prevpagebutton;
    public Text pagetext;
    public ChangeOutline lgoutline;
    public SampleVoice lgvoice;

    public CharSaveLoad Instance {
        get { return _instance; }
    }

    GameData gamedata;

    private void Awake(){

        DontDestroyOnLoad(gameObject);//I dont remember why this is here

        if (_instance == null){
            //_instance = new CharSaveLoad();//it doesn't like this
        }

        if (gamedata == null){
            gamedata = new GameData();
        }

        filepath = Path.Combine(Application.dataPath, filename);
    }

    // Start is called before the first frame update
    void Start(){
        selectedchar = 0;
        LoadGameData();
        UpdateSelected(selectedchar);
        UpdatePage(0);
        stagger = 0;
        lgname.text = gamedata.charname[0];
        lgchardisp.SetActive(true);
        lgweapondisp.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if (stagger > 0){
            stagger--;
        }
    }

    private void UpdateSelected(int a){
        pagetext.text = "Page " + (pagenum+1).ToString();
        blankchar[0].SetActive(false);
        charsel[0].SetActive(false);
        addchar[0].SetActive(true);

        for (int i=1; i<MAXINE; i++){
            blankchar[i].SetActive(true);
            addchar[i].SetActive(false);
            charsel[i].SetActive(false);
        }


        for (int i=0; i<MAXINE && i<gamedata.hairtype.Count - pagenum*10; i++){
            blankchar[i].SetActive(false);
            addchar[i].SetActive(false);
            charsel[i].SetActive(true);
            if (i + 1 < MAXINE){
                addchar[i+1].SetActive(true);
            }
            else{
                nextpagebutton.SetActive(true);
            }

            if (a != i + pagenum*10){
                smchar[33 + 37 * i].color = DESELECTED;
                smchar[34 + 37 * i].color = DESELECTED;
                smchar[35 + 37 * i].color = DESELECTED;
                smchar[36 + 37 * i].color = DESELECTED;
            }
            else{
                smchar[33 + 37 * i].color = SELECTED;
                smchar[34 + 37 * i].color = SELECTED;
                smchar[35 + 37 * i].color = SELECTED;
                smchar[36 + 37 * i].color = SELECTED;

                charname.text = gamedata.charname[a];
                charbio.text = gamedata.charbio[a];

                SwapStyleLG(lgchar[8], lgchar[9], lgchar[10], lgchar[11], lgchar[7], gamedata.hairtype[i + pagenum * 10], 0);//hair
                SwapStyleLG(lgchar[13], lgchar[14], lgchar[15], lgchar[16], lgchar[12], gamedata.facialhairtype[i + pagenum * 10], 1);//facial
                SwapStyleLG(lgchar[18], lgchar[19], lgchar[20], lgchar[21], lgchar[17], gamedata.shirttype[i + pagenum * 10], 2);//shirt
                SwapStyleLG(lgchar[22], lgchar[23], lgchar[24], lgchar[25], lgchar[6], gamedata.pantstype[i + pagenum * 10], 3);//pants
                SwapStyleLG(lgchar[27], lgchar[28], lgchar[29], lgchar[30], lgchar[26], gamedata.acctype[i + pagenum * 10], 4);//acc
                SwapStyleLG(lgchar[32], lgchar[33], lgchar[34], lgchar[35], lgchar[31], gamedata.classtype[i + pagenum * 10], 5);//class

                SwapColorLG(lgchar[8], gamedata.hairprim[i + pagenum * 10], 0);
                SwapColorLG(lgchar[9], gamedata.hairsec[i + pagenum * 10], 1);
                SwapColorLG(lgchar[13], gamedata.facialhairprim[i + pagenum * 10], 2);
                SwapColorLG(lgchar[18], gamedata.shirtprim[i + pagenum * 10], 3);
                SwapColorLG(lgchar[19], gamedata.shirtsec[i + pagenum * 10], 4);
                SwapColorLG(lgchar[20], gamedata.shirtlogo[i + pagenum * 10], 5);
                SwapColorLG(lgchar[21], gamedata.shirttrim[i + pagenum * 10], 6);
                SwapColorLG(lgchar[22], gamedata.pantscolor[i + pagenum * 10], 7);
                SwapColorLG(lgchar[23], gamedata.shoeprim[i + pagenum * 10], 8);
                SwapColorLG(lgchar[24], gamedata.shoedec[i + pagenum * 10], 9);
                SwapColorLG(lgchar[27], gamedata.accprim[i + pagenum * 10], 10);
                SwapColorLG(lgchar[28], gamedata.accsec[i + pagenum * 10], 11);
                SwapColorLG(lgchar[2], gamedata.skincolor[i + pagenum * 10], 12);
                SwapColorLG(lgchar[32], gamedata.weaponprim[i + pagenum * 10], 13);
                SwapColorLG(lgchar[33], gamedata.weaponsec[i + pagenum * 10], 14);

                SwapOutlineLG(gamedata.outlinetype[i + pagenum * 10]);
                SwapVoiceLG(gamedata.voicetype[i + pagenum * 10]);
            }
        }
    }

    private void UpdatePage(int page){
        for(int i=0; i< MAXINE && i < gamedata.hairtype.Count - page * 10; i++){
            SwapStyleSM(smchar[8 + 37 * i], smchar[9 + 37 * i], smchar[10 + 37 * i], smchar[11 + 37 * i], smchar[7 + 37 * i], gamedata.hairtype[i + page * 10], 0);//hair
            SwapStyleSM(smchar[13 + 37 * i], smchar[14 + 37 * i], smchar[15 + 37 * i], smchar[16 + 37 * i], smchar[12 + 37 * i], gamedata.facialhairtype[i + page * 10], 1);//facial
            SwapStyleSM(smchar[18 + 37 * i], smchar[19 + 37 * i], smchar[20 + 37 * i], smchar[21 + 37 * i], smchar[17 + 37 * i], gamedata.shirttype[i + page * 10], 2);//shirt
            SwapStyleSM(smchar[22 + 37 * i], smchar[23 + 37 * i], smchar[24 + 37 * i], smchar[25 + 37 * i], smchar[6 + 37 * i], gamedata.pantstype[i + page * 10], 3);//pants
            SwapStyleSM(smchar[27 + 37 * i], smchar[28 + 37 * i], smchar[29 + 37 * i], smchar[30 + 37 * i], smchar[26 + 37 * i], gamedata.acctype[i + page * 10], 4);//acc
            SwapStyleSM(smchar[31 + 37 * i], smchar[3 + 37 * i], smchar[4 + 37 * i], smchar[5 + 37 * i], smchar[6 + 37 * i], gamedata.classtype[i + page * 10], 5);//class
            SwapStyleSM(smchar[32 + 37 * i], smchar[3 + 37 * i], smchar[4 + 37 * i], smchar[5 + 37 * i], smchar[6 + 37 * i], gamedata.status[i + page * 10], 6);//status

            SwapColorSM(smchar[8 + 37 * i], gamedata.hairprim[i + page * 10]);
            SwapColorSM(smchar[9 + 37 * i], gamedata.hairsec[i + page * 10]);
            SwapColorSM(smchar[13 + 37 * i], gamedata.facialhairprim[i + page * 10]);
            SwapColorSM(smchar[18 + 37 * i], gamedata.shirtprim[i + page * 10]);
            SwapColorSM(smchar[19 + 37 * i], gamedata.shirtsec[i + page * 10]);
            SwapColorSM(smchar[20 + 37 * i], gamedata.shirtlogo[i + page * 10]);
            SwapColorSM(smchar[21 + 37 * i], gamedata.shirttrim[i + page * 10]);
            SwapColorSM(smchar[22 + 37 * i], gamedata.pantscolor[i + page * 10]);
            SwapColorSM(smchar[23 + 37 * i], gamedata.shoeprim[i + page * 10]);
            SwapColorSM(smchar[24 + 37 * i], gamedata.shoedec[i + page * 10]);
            SwapColorSM(smchar[27 + 37 * i], gamedata.accprim[i + page * 10]);
            SwapColorSM(smchar[28 + 37 * i], gamedata.accsec[i + page * 10]);
            SwapColorSM(smchar[2 + 37 * i], gamedata.skincolor[i + page * 10]);

            SwapOutlineSM(gamedata.outlinetype[i + page * 10], i);//outline

            charinfo[0 + 4 * i].text = gamedata.charname[i + page * 10];
            charinfo[1 + 4 * i].text = gamedata.kills[i + page * 10].ToString();
            charinfo[2 + 4 * i].text = gamedata.missions[i + page * 10].ToString();
            charinfo[3 + 4 * i].text = "v" + gamedata.version[i + page * 10].ToString() + ".0";

        }
        UpdateSelected(selectedchar);
    }

    private void SwapOutlineSM(int a, int i){
        Color hellacolor = lgoutline.GetColor(a);
        smchar[0 + 37 * i].color = hellacolor;
        smchar[7 + 37 * i].color = hellacolor;
        smchar[12 + 37 * i].color = hellacolor;
        smchar[17 + 37 * i].color = hellacolor;
        smchar[26 + 37 * i].color = hellacolor;
    }

    private void SwapStyleSM(SpriteRenderer prim, SpriteRenderer sec, SpriteRenderer logo, SpriteRenderer trim, SpriteRenderer outln, int a, int s){
        prim.sprite = smstyle[s].GetSprite(a, 0);
        sec.sprite = smstyle[s].GetSprite(a, 1);
        logo.sprite = smstyle[s].GetSprite(a, 2);
        trim.sprite = smstyle[s].GetSprite(a, 3);
        outln.sprite = smstyle[s].GetSprite(a, 4);
    }

    private void SwapColorSM(SpriteRenderer img, Color col){
        img.color = col;
    }
    private void SwapOutlineLG(int a){
        Color hellacolor = lgoutline.GetColor(a);
        lgchar[0].color = hellacolor;
        lgchar[7].color = hellacolor;
        lgchar[12].color = hellacolor;
        lgchar[17].color = hellacolor;
        lgchar[26].color = hellacolor;
        previewstyle[6].sprite = lgoutline.GetPreview(a);
    }
    private void SwapVoiceLG(int a){
        previewstyle[7].sprite = lgvoice.GetPreview(a);
    }
    private void SwapStyleLG(SpriteRenderer prim, SpriteRenderer sec, SpriteRenderer logo, SpriteRenderer trim, SpriteRenderer outln, int a, int s)
    {
        prim.sprite = style[s].GetSprite(a, 0);
        sec.sprite = style[s].GetSprite(a, 1);
        logo.sprite = style[s].GetSprite(a, 2);
        trim.sprite = style[s].GetSprite(a, 3);
        outln.sprite = style[s].GetSprite(a, 4);
        previewstyle[s].sprite = style[s].GetSprite(a, 5);
    }

    private void SwapColorLG(SpriteRenderer img, Color col, int s)
    {
        img.color = col;
        previewcolor[s].color = col;
        ColorToHex(col, colorinput[s]);
    }

    void LoadGameData(){
        string json;

        if (File.Exists(filepath)){
            json = File.ReadAllText(filepath);
            gamedata = JsonUtility.FromJson<GameData>(json);
        }
        else{
            CreateDefaultSave();
        }
    }

    void SaveGameData(){
        string json = JsonUtility.ToJson(gamedata);
        if (File.Exists(filepath)){
            File.Create(filepath).Dispose();
        }
        File.WriteAllText(filepath, json);
    }

    void CreateDefaultSave(){
        gamedata.charname.Add("Player Name");
        gamedata.hairtype.Add(1);
        gamedata.hairprim.Add(new Color(31f/255f, 194f/255f, 164f/255f, 1.0f));
        gamedata.hairsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.facialhairtype.Add(2);
        gamedata.facialhairprim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.shirttype.Add(1);
        gamedata.shirtprim.Add(new Color(41f/255f, 41f/255f, 41f/255f, 1.0f));
        gamedata.shirtsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.shirtlogo.Add(new Color(240f/255f, 240f/255f, 240f/255f, 1.0f));
        gamedata.shirttrim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.pantstype.Add(1);
        gamedata.pantscolor.Add(new Color(23f/255f, 97f/255f, 189f/255f, 1.0f));
        gamedata.shoeprim.Add(new Color(69f/255f, 69f/255f, 69f/255f, 1.0f));
        gamedata.shoedec.Add(new Color(51f/255f, 204f/255f, 51f/255f, 1.0f));
        gamedata.acctype.Add(4);
        gamedata.accprim.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.accsec.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        gamedata.outlinetype.Add(0);
        gamedata.skincolor.Add(new Color(1.0f, 228f/255f, 207f/255f, 1.0f));
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

    public void SaveColor(int index, Color col) {
        int i = selectedchar % 10;
        int a = selectedchar;
        switch (index) {
            case 0:
                SwapColorSM(smchar[8 + 37 * i], gamedata.hairprim[a]);
                gamedata.hairprim[a] = col;
                break;
            case 1:
                SwapColorSM(smchar[9 + 37 * i], gamedata.hairsec[a]);
                gamedata.hairsec[a] = col;
                break;
            case 2:
                SwapColorSM(smchar[13 + 37 * i], gamedata.facialhairprim[a]);
                gamedata.facialhairprim[a] = col;
                break;
            case 3:
                SwapColorSM(smchar[18 + 37 * i], gamedata.shirtprim[a]);
                gamedata.shirtprim[a] = col;
                break;
            case 4:
                SwapColorSM(smchar[19 + 37 * i], gamedata.shirtsec[a]);
                gamedata.shirtsec[a] = col;
                break;
            case 5:
                SwapColorSM(smchar[20 + 37 * i], gamedata.shirtlogo[a]);
                gamedata.shirtlogo[a] = col;
                break;
            case 6:
                SwapColorSM(smchar[21 + 37 * i], gamedata.shirttrim[a]);
                gamedata.shirttrim[a] = col;
                break;
            case 7:
                SwapColorSM(smchar[22 + 37 * i], gamedata.pantscolor[a]);
                gamedata.pantscolor[a] = col;
                break;
            case 8:
                SwapColorSM(smchar[23 + 37 * i], gamedata.shoeprim[a]);
                gamedata.shoeprim[a] = col;
                break;
            case 9:
                SwapColorSM(smchar[24 + 37 * i], gamedata.shoedec[a]);
                gamedata.shoedec[a] = col;
                break;
            case 10:
                SwapColorSM(smchar[27 + 37 * i], gamedata.accprim[a]);
                gamedata.accprim[a] = col;
                break;
            case 11:
                SwapColorSM(smchar[28 + 37 * i], gamedata.accsec[a]);
                gamedata.accsec[a] = col;
                break;
            case 12:
                SwapColorSM(smchar[2 + 37 * i], gamedata.skincolor[a]);
                gamedata.skincolor[a] = col;
                break;
            case 13:
                gamedata.weaponprim[a] = col;
                break;
            case 14:
                gamedata.weaponsec[a] = col;
                break;
            default:
                Debug.Log("Wrong Index for SaveColor: " + index);
                return;
        }
    }

     
    public void SaveType(int index, int num){

        int i = selectedchar % 10;
        int a = selectedchar;
        switch (index)
        {
            case 0:
                SwapStyleSM(smchar[8 + 37 * i], smchar[9 + 37 * i], smchar[10 + 37 * i], smchar[11 + 37 * i], smchar[7 + 37 * i], gamedata.hairtype[a], 0);//hair
                gamedata.hairtype[a] = num;
                break;
            case 1:
                SwapStyleSM(smchar[13 + 37 * i], smchar[14 + 37 * i], smchar[15 + 37 * i], smchar[16 + 37 * i], smchar[12 + 37 * i], gamedata.facialhairtype[a], 1);//facial
                gamedata.facialhairtype[a] = num;
                break;
            case 2:
                SwapStyleSM(smchar[18 + 37 * i], smchar[19 + 37 * i], smchar[20 + 37 * i], smchar[21 + 37 * i], smchar[17 + 37 * i], gamedata.shirttype[a], 2);//shirt
                gamedata.shirttype[a] = num;
                break;
            case 3:
                SwapStyleSM(smchar[22 + 37 * i], smchar[23 + 37 * i], smchar[24 + 37 * i], smchar[25 + 37 * i], smchar[6 + 37 * i], gamedata.pantstype[a], 3);//pants
                gamedata.pantstype[a] = num;
                break;
            case 4:
                SwapStyleSM(smchar[27 + 37 * i], smchar[28 + 37 * i], smchar[29 + 37 * i], smchar[30 + 37 * i], smchar[26 + 37 * i], gamedata.acctype[a], 4);//acc
                gamedata.acctype[a] = num;
                break;
            case 5:
                SwapStyleSM(smchar[31 + 37 * i], smchar[3 + 37 * i], smchar[4 + 37 * i], smchar[5 + 37 * i], smchar[6 + 37 * i], gamedata.classtype[a], 5);//class
                gamedata.classtype[a] = num;
                break;
            case 6:
                SwapOutlineSM(gamedata.outlinetype[a], i);
                gamedata.outlinetype[a] = num;
                break;
            case 7:
                gamedata.voicetype[a] = num;
                break;
            default:
                Debug.Log("Wrong Index for SaveType: " + index);
               break;
        }
    }

    public void SaveName(){
        gamedata.charname[selectedchar] = charname.text;
        ChangeChar(selectedchar);
    }

    public void SaveBio(){
        gamedata.charbio[selectedchar] = charbio.text;
        ChangeChar(selectedchar);
    }

    public void ChangeChar(int a){
        //This is to try and avoid characters overriding each other as the Update Function hasn't finished processing yet
        //This happens when rapidly clicking through characters
        if(stagger != 0){
            return;
        }
        stagger = 10;//We can make this higher if needed, but this seems to be enough that I can't replicate the already rare problem
        a += pagenum * 10;

        for(int i=0; i<6; i++){
            UpdateStyle(i);
        }
        for(int i=0; i<15; i++){
            UpdateColor(i);
        }
        SaveType(6, lgoutline.GetNum());
        SaveType(7, lgvoice.GetNum());
        if (gamedata.charname.Count == a){
            CreateDefaultSave();
            selectedchar = a;
        }
        else if (gamedata.charname.Count < a){
            Debug.Log("We are skipping a character slot - Count=" + gamedata.charname.Count + "; index=" + a);
            return;
        }
        else{
            selectedchar = a;
        }
        charname.text = gamedata.charname[a];
        lgname.text = gamedata.charname[a];
        charbio.text = gamedata.charbio[a];
        SaveGameData();
        UpdatePage(pagenum);
        UpdateSelected(selectedchar);
    }

    public void UpdateColor(int a){
        SaveColor(a, color[a].GetColor());
    }

    public void UpdateStyle(int a){
        SaveType(a, style[a].GetNum());
    }

    private int FloatToHex(float a){
        int i;
        for (i=0; a>15; i++){
            a -= 16;
        }
        return i;
    }

    private string IntToChar(int a){
        if (a >= 0 && a <= 15){
            switch (a){
                case 0:
                    return "0";
                case 1:
                    return "1";
                case 2:
                    return "2";
                case 3:
                    return "3";
                case 4:
                    return "4";
                case 5:
                    return "5";
                case 6:
                    return "6";
                case 7:
                    return "7";
                case 8:
                    return "8";
                case 9:
                    return "9";
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "?";
            }
        }
        else
        {
            return "&";
        }
    }

    public void ColorToHex(Color a, InputField val){
        float r = a.r*255;
        float g = a.g*255;
        float b = a.b*255;
        int r1 = FloatToHex(r);
        int g1 = FloatToHex(g);
        int b1 = FloatToHex(b);
        int r2 = (int)r - r1*16;
        int g2 = (int)g - g1*16;
        int b2 = (int)b - b1*16;
        val.text = IntToChar(r1) + IntToChar(r2) + IntToChar(g1) + IntToChar(g2) + IntToChar(b1) + IntToChar(b2);
    }

    public void LeaveScene(){
        ChangeChar(selectedchar);
        SceneManager.LoadScene("Campaign");
    }

    public void NextPage(){
        pagenum++;
        prevpagebutton.SetActive(true);
        nextpagebutton.SetActive(false);
        UpdatePage(pagenum);
    }

    public void PrevPage(){
        pagenum--;
        if(pagenum == 0){
            prevpagebutton.SetActive(false);
        }
        UpdatePage(pagenum);
    }

    public void ViewChar(){
        lgchardisp.SetActive(true);
        lgweapondisp.SetActive(false);
    }

    public void ViewWeapon(){
        lgchardisp.SetActive(false);
        lgweapondisp.SetActive(true);
    }

    [System.Serializable]
    public class GameData{
        public List<string> charname = new List<string>();
        public List<int> hairtype = new List<int>();//0
        public List<Color> hairprim = new List<Color>();//1
        public List<Color> hairsec = new List<Color>();//2
        public List<int> facialhairtype = new List<int>();//3
        public List<Color> facialhairprim = new List<Color>();//4
        public List<int> shirttype = new List<int>();//5
        public List<Color> shirtprim = new List<Color>();//6
        public List<Color> shirtsec = new List<Color>();//7
        public List<Color> shirtlogo = new List<Color>();//8
        public List<Color> shirttrim = new List<Color>();//9
        public List<int> pantstype = new List<int>();//10
        public List<Color> pantscolor = new List<Color>();//11
        public List<Color> shoeprim = new List<Color>();//12
        public List<Color> shoedec = new List<Color>();//13
        public List<int> acctype = new List<int>();//14
        public List<Color> accprim = new List<Color>();//15
        public List<Color> accsec = new List<Color>();//16
        public List<int> outlinetype = new List<int>();//17
        public List<Color> skincolor = new List<Color>();//18
        public List<int> classtype = new List<int>();//19
        public List<Color> weaponprim = new List<Color>();//20
        public List<Color> weaponsec = new List<Color>();//21
        public List<int> voicetype = new List<int>();//22

        public List<string> charbio = new List<string>();
        public List<int> status = new List<int>();
        public List<int> kills = new List<int>();
        public List<int> missions = new List<int>();
        public List<int> version = new List<int>();
    }
}
