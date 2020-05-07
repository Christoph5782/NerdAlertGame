using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack {

  public string name;
  public int minDamage;
  public int maxDamage;
  public string status;
  public int statusDuration;

   public attack(string name, int min, int max, string status, int duration){
      this.name = name;
      this.minDamage = min;
      this.maxDamage = max;
      this.status = status;
      this.statusDuration = duration;
    }

    public string getName(){
        return name;
    }
    
    public string getStatus(){
        return status;
    }
    public int getStatusDuration(){
        return statusDuration;
    }
}
