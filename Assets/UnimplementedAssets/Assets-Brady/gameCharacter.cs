using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCharacter {

    public HealthSystem healthSystem;
    private string name;
    private string status = "none";
    private string team;
    private int statusDuration;
    public int a1;
    public int a2;
    public int a3;
    public int a4;

    //Constructor
    public gameCharacter(string name, int health, int turns, int a1, int a2, int a3, int a4) {
      this.healthSystem = new HealthSystem(health, turns);
      this.name = name;
      this.a1 = a1;
      this.a2 = a2;
      this.a3 = a3;
      this.a4 = a4;
    }

    public string getName(){
      return name;
    }

    public string getStatus(){
      return status;
    }

    public void setStatus(string status, int turns){
      this.status = status;
      this.statusDuration = turns;

    }

    public void nextTurn(){
      //Check for active status
      if(statusDuration > 0){
        statusEffect();
        statusDuration--;

        //If statusDuration just hit 0, remove it
        if(statusDuration == 0){
          status = "none";
        }

      }

    }

    private void statusEffect(){
      switch(status){
        default:
          break;
        //fill in effects
      }

    }


}
