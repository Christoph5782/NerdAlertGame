using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private readonly string filename = "CharSaveData.json";
    private string filepath;
    private int count = 0;
    private bool down = true;

    public SpriteRenderer[] lgchar;
    public SwapStyle[] lgstyle;
    public GameObject enemies;

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
    void Start()
    {
        LoadGameData();
        UpdateLargeChar();
    }

    // Update is called once per frame
    void Update()
    {
        if (down){
            enemies.transform.Translate(-0.0004f, -0.001f, 0.0f);
            count++;
            if(count >= 1000)
            {
                down = false;
            }
        }
        else{
            enemies.transform.Translate(0.0004f, 0.001f, 0.0f);
            count--;
            if (count <= 0)
            {
                down = true;
            }
        }

    }

    private void UpdateLargeChar()
    {
        int i = Random.Range(0, gamedata.hairtype.Count);
        //print(i);

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
        if (s == 0)
        {
            hellacolor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            hellacolor = new Color(51f / 117f, 117f / 255f, 135f / 255f, 1.0f);
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

    public void ToCampaign(){
        SceneManager.LoadScene("Campaign");
    }
    public void ToEditor(){
        SceneManager.LoadScene("CharaterCreation");
    }
    public void ToCredits(){
        SceneManager.LoadScene("Credits");
    }
    public void ExitGame(){
        Application.Quit();//This is ingored in the editor so I have no idea if this works
    }

}
