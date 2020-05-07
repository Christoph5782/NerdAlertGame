using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class gameplay {
    private static int currentCharacter = 0;
    private static string[] initiative = {"player1", "player2"};

    public static gameCharacter enemy1 = new gameCharacter("virus", 100, 1, 0, 1, 2, 3);
    public static gameCharacter player1 = new gameCharacter("player1", 100, 3, 0, 1, 4, 5);
    public static gameCharacter player2 = new gameCharacter("player2", 100, 3, 1, 2, 3, 5);



    public static void nextCharacter(){
        //Get next character and loop around when needed
        currentCharacter += 1;
        currentCharacter %= initiative.Length;
    }

    public static string getCharacter(){
      return initiative[currentCharacter];
    }

    public static gameCharacter getGameObject(){
        switch(initiative[currentCharacter]){
          case "player1":
            return player1;
          case "player2":
            return player2;
          default:
            return null;
        }
    }

    public static gameCharacter getEnemy(){
      return enemy1;
    }

}
