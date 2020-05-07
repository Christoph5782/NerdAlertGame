using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class attackHandler : MonoBehaviour
{
    //Attacker always refers to self
    private gameCharacter attacker;
    private gameCharacter target;
    private Tilemap tilemap;
    private GameObject lastClicked;
    private Ray ray;
    private RaycastHit rayHit;
    public Slider UIHealthBar;

    //This is where the stats for all attacks are stored
    attack[] attackList = {
      new attack("Rocket Launcher", 14, 20, "none", 0),
      new attack("Grenade", 10, 14, "none", 0),
      new attack("Flamethrower", 6, 10, "burn", 3),
      new attack("Rifle", 4, 8, "none", 0),
      new attack("Pistol", 2, 6, "none", 0),
      new attack("Punch", 1, 2, "none", 0)

    };

    // Start is called before the first frame update
    void Start()
    {
      //Default enemy1
      //target = gameplay.enemy1;

      //Finds the target's healthbar
      UIHealthBar = GameObject.Find("Canvas/enemy1HP").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        attacker = gameplay.getGameObject();

        if(name == gameplay.getCharacter()){
        target = gameplay.getEnemy();

            if (Input.GetMouseButtonDown(0))
            {
              //Add get target by clicking on them

              target = gameplay.enemy1;
              UIHealthBar = GameObject.Find("Canvas/"+target.getName()+"HP").GetComponent<Slider>();

            }

            if (Input.GetKeyUp(KeyCode.Z) ){
              handleAttack(attacker.a1, attacker, target);
            }
            else if (Input.GetKeyUp(KeyCode.X) ){
              handleAttack(attacker.a2, attacker, target);
            }
            else if (Input.GetKeyUp(KeyCode.C) ){
              handleAttack(attacker.a3, attacker, target);
            }
            else if (Input.GetKeyUp(KeyCode.V) ){
              handleAttack(attacker.a4, attacker, target);
            }
        }
    }


    private void handleAttack(int id, gameCharacter attacker, gameCharacter target){
       if(checkValid()){

          System.Random rnd = new System.Random();
          int damage = rnd.Next(attackList[id].minDamage, attackList[id].maxDamage+1);
          target.healthSystem.Damage(damage);

          Debug.Log(attacker.getName() + " used " + attackList[id].name + " on " + target.getName() + " for " + damage);
          Debug.Log(target.getName()+" now has "+  target.healthSystem.GetHealth()+" hp");

          if(attackList[id].status != "none"){
    	       //maybe some sort of chance to resist
              target.setStatus(attackList[id].status,attackList[id].statusDuration);
              Debug.Log(attackList[id].name+" applied the "+attackList[id].status+" effect for "+attackList[id].statusDuration+" turns.");
          }

          UIHealthBar.value = target.healthSystem.GetHealthPercent();

          target.healthSystem.checkStatus();

          if(target.healthSystem.GetHealthPercent() == 0){
            //run win script
            Debug.Log("You Win!");


          }
       }

    }

    private bool checkValid(){
      //Check on distance and if they're an enemy maybe
      return true;
    }


}
