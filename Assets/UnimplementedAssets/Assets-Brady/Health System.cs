using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem {

    private int healthMax;
    private int health;
    private int downedTurns;
    private bool alive = true;

    //Health is the character's max health, which they also start at
    //Turns is the number of turns a character can be in the downed state
    //Turns will likely be a small number like 3 for player characters and 0 for enemies.
    public HealthSystem(int health, int turns) {
      this.healthMax = health;
      this.health = health;
      this.downedTurns = turns;
    }

    public int GetHealth() {
      return health;
    }

    //Get percentage of total health, usually for use with HealthBar class
    public float GetHealthPercent(){
      return (float)health / healthMax;
    }

    //Lower health
    public void Damage(int damageAmount) {
      health -= damageAmount;
      if (health < 0){
        health = 0;
      }
    }

    //Raise health
    public void Heal(int healAmount) {
      health += healAmount;
      downedTurns = 3;
      if (health > healthMax){
        health = healthMax;
      }

    }

    //Call at the beginning of each turn to check if they are alive, dying, or dead
    public bool checkStatus() {
      if (health <= 0) {
        dying();
        //Possibly lock character's turn here, since they are either dying or dead
      }
        return alive;
    }

    //Checks if the character can stay downed for another turn
    //If they can, then use up that turn. Otherwise they die
    //The checkStatus function will then return that they are dead
    public void dying(){
      downedTurns--;
      if (downedTurns == 0) {
        alive = false;
        Debug.Log("This character has died");
      }
    }

}
