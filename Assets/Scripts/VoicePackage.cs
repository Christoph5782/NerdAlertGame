using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePackage : MonoBehaviour
{
    public AudioClip[] sprinting;
    public AudioClip[] flanking;

    public AudioClip[] beforeshot;
    public AudioClip[] hittarget;
    public AudioClip[] missedtarget;
    public AudioClip[] killedtarget;
    public AudioClip[] outofammo;
    public AudioClip[] reloading;

    public AudioClip[] missedagainst;
    public AudioClip[] hitagainst;
    public AudioClip[] downed;
    public AudioClip[] killed;
    public AudioClip[] teammatedowned;
    public AudioClip[] teammatekilled;
    public AudioClip[] reviving;

    public AudioClip[] runandgun;
    public AudioClip[] healing;
    public AudioClip[] firewall;
    public AudioClip[] firingrockets;
    public AudioClip[] shieldmelee;
    public AudioClip[] shieldcover;
    public AudioClip[] placedtrap;
    public AudioClip[] usingdrone;
    public AudioClip[] trapspotted;
    public AudioClip[] enemyspotted;
    public AudioClip[] machinehacked;
    public AudioClip[] sentrygunplaced;
    public AudioClip[] sentrygunkill;

    public AudioClip[] keylogged;
    public AudioClip[] clonesspawned;
    public AudioClip[] clonekilled;
    public AudioClip[] debuffed;
    public AudioClip[] hitbytrap;

    public AudioClip[] bot;
    public AudioClip[] keylogger;
    public AudioClip[] malware;
    public AudioClip[] scripting;
    public AudioClip[] bootsector;
    public AudioClip[] macro;

    public AudioClip[] objtakingdamageP;
    public AudioClip[] objprotectedP;
    public AudioClip[] objdestroyedP;
    public AudioClip[] newwaveS;
    public AudioClip[] finalwaveS;
    public AudioClip[] datatransfercompleteS;
    public AudioClip[] alltargetkiaE;
    public AudioClip[] objspottedD;
    public AudioClip[] objdestroyedD;
    public AudioClip[] troopsextracted;

    public AudioClip[] userafk;
    public AudioClip[] successwithhalfdead;
    public AudioClip[] noenemiesseen;
    public AudioClip[] teammatemissed75shot;
    public AudioClip[] teammatemissed90shot;
    public AudioClip[] teammatekillsenemy;

    public AudioClip[] startupprotect;
    public AudioClip[] startupsurvive;
    public AudioClip[] startupelimination;
    public AudioClip[] startupdemolition;

    public AudioClip[] bossintro;
    public AudioClip[] bosscomplete;
    public AudioClip tutorialbluff;


    public AudioClip GetAudioClip(AudioClip[] a) {
        if (a.Length == 0){
            return null;
        }
        int i = Random.Range(0, a.Length);
        return a[i];
    }

    public AudioClip Getsprinting()
    {
        return GetAudioClip(sprinting);
    }
    public AudioClip Getflanking()
    {
        return GetAudioClip(flanking);
    }

    public AudioClip Getbeforeshot()
    {
        return GetAudioClip(beforeshot);
    }
    public AudioClip Gethittarget()
    {
        return GetAudioClip(hittarget);
    }
    public AudioClip Getmissedtarget()
    {
        return GetAudioClip(missedtarget);
    }
    public AudioClip Getkilledtarget()
    {
        return GetAudioClip(killedtarget);
    }
    public AudioClip Getoutofammo()
    {
        return GetAudioClip(outofammo);
    }
    public AudioClip Getreloading()
    {
        return GetAudioClip(reloading);
    }


    public AudioClip Getmissedagainst()
    {
        return GetAudioClip(missedagainst);
    }
    public AudioClip Gethitagainst()
    {
        return GetAudioClip(hitagainst);
    }
    public AudioClip Getdowned()
    {
        return GetAudioClip(downed);
    }
    public AudioClip Getkilled()
    {
        return GetAudioClip(killed);
    }
    public AudioClip Getteammatedowned()
    {
        return GetAudioClip(teammatedowned);
    }
    public AudioClip Getteammatekilled()
    {
        return GetAudioClip(teammatekilled);
    }
    public AudioClip Getreviving()
    {
        return GetAudioClip(reviving);
    }


    public AudioClip Getrunandgun()
    {
        return GetAudioClip(runandgun);
    }
    public AudioClip Gethealing()
    {
        return GetAudioClip(healing);
    }
    public AudioClip Getfirewall()
    {
        return GetAudioClip(firewall);
    }
    public AudioClip Getfiringrockets()
    {
        return GetAudioClip(firingrockets);
    }
    public AudioClip Getshieldmelee()
    {
        return GetAudioClip(shieldmelee);
    }
    public AudioClip Getshieldcover()
    {
        return GetAudioClip(shieldcover);
    }
    public AudioClip Getplacedtrap()
    {
        return GetAudioClip(placedtrap);
    }
    public AudioClip Getusingdrone()
    {
        return GetAudioClip(usingdrone);
    }
    public AudioClip Gettrapspotted()
    {
        return GetAudioClip(trapspotted);
    }
    public AudioClip Getenemyspotted()
    {
        return GetAudioClip(enemyspotted);
    }
    public AudioClip Getmachinehacked()
    {
        return GetAudioClip(machinehacked);
    }
    public AudioClip Getsentrygunplaced()
    {
        return GetAudioClip(sentrygunplaced);
    }
    public AudioClip Getsentrygunkill()
    {
        return GetAudioClip(sentrygunkill);
    }


    public AudioClip Getkeylogged()
    {
        return GetAudioClip(keylogged);
    }
    public AudioClip Getclonesspawned()
    {
        return GetAudioClip(clonesspawned);
    }
    public AudioClip Getclonekilled()
    {
        return GetAudioClip(clonekilled);
    }
    public AudioClip Getdebuffed()
    {
        return GetAudioClip(debuffed);
    }
    public AudioClip Gethitbytrap()
    {
        return GetAudioClip(hitbytrap);
    }


    public AudioClip Getbot()
    {
        return GetAudioClip(bot);
    }
    public AudioClip Getkeylogger()
    {
        return GetAudioClip(keylogger);
    }
    public AudioClip Getmalware()
    {
        return GetAudioClip(malware);
    }
    public AudioClip Getscripting()
    {
        return GetAudioClip(scripting);
    }
    public AudioClip Getbootsector()
    {
        return GetAudioClip(bootsector);
    }
    public AudioClip Getmacro()
    {
        return GetAudioClip(macro);
    }


    public AudioClip GetobjtakingdamageP()
    {
        return GetAudioClip(objtakingdamageP);
    }
    public AudioClip GetobjprotectedP()
    {
        return GetAudioClip(objprotectedP);
    }
    public AudioClip GetobjdestroyedP()
    {
        return GetAudioClip(objdestroyedP);
    }
    public AudioClip GetnewwaveS()
    {
        return GetAudioClip(newwaveS);
    }
    public AudioClip GetfinalwaveS()
    {
        return GetAudioClip(finalwaveS);
    }
    public AudioClip GetdatatransfercompleteS()
    {
        return GetAudioClip(datatransfercompleteS);
    }
    public AudioClip GetalltargetkiaE()
    {
        return GetAudioClip(alltargetkiaE);
    }
    public AudioClip GetobjspottedD()
    {
        return GetAudioClip(objspottedD);
    }
    public AudioClip GetobjdestroyedD()
    {
        return GetAudioClip(objdestroyedD);
    }
    public AudioClip Gettroopsextracted()
    {
        return GetAudioClip(troopsextracted);
    }


    public AudioClip Getuserafk()
    {
        return GetAudioClip(userafk);
    }
    public AudioClip Getsuccesswithhalfdead()
    {
        return GetAudioClip(successwithhalfdead);
    }
    public AudioClip Getnoenemiesseen()
    {
        return GetAudioClip(noenemiesseen);
    }
    public AudioClip Getteammatemissed75shot()
    {
        return GetAudioClip(teammatemissed75shot);
    }
    public AudioClip Getteammatemissed90shot()
    {
        return GetAudioClip(teammatemissed90shot);
    }
    public AudioClip Getteammatekillsenemy()
    {
        return GetAudioClip(teammatekillsenemy);
    }


    public AudioClip Getstartupprotect()
    {
        return GetAudioClip(startupprotect);
    }
    public AudioClip Getstartupsurvive()
    {
        return GetAudioClip(startupsurvive);
    }
    public AudioClip Getstartupelimination()
    {
        return GetAudioClip(startupelimination);
    }
    public AudioClip Getstartupdemolition()
    {
        return GetAudioClip(startupdemolition);
    }


    public AudioClip Getbossintro(int s)
    {
        return bossintro[s];
    }
    public AudioClip Getbosscomplete(int s)
    {
        return bosscomplete[s];
    }
    public AudioClip Gettutorialbluff(int s)
    {
        return tutorialbluff;
    }

}
