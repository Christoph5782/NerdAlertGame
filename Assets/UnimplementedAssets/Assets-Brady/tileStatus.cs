using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileStatus{

private static Dictionary<Vector3Int, int> tileStatusDictionary;
private int maxX = 16;
private int maxY = 8;
    // Tile status codes
    // 1 = yellow = normal
    // 2 = green = cover
    // 3 = hazard = red
    // 4 = another character, block movement
    // 5 = wall, block movement

    public tileStatus(){
        //Convert from number of tiles to 0-indexed

        tileStatusDictionary = new Dictionary<Vector3Int, int>();

        //Set all tiles to 1 by default
        for(int x = 0; x <= maxX; x++){
            for(int y = 0; y <= maxY; y++){
              tileStatusDictionary.Add(new Vector3Int(x, y, 0), 1);
            }
        }

        //Set outline to 5 to create a border
        for(int x = -1; x <= maxX+1; x++){
              tileStatusDictionary.Add(new Vector3Int(x, -1, 0), 5);
              tileStatusDictionary.Add(new Vector3Int(x, maxY+1, 0), 5);
        }

        for(int y = 0; y <= maxY; y++){
          tileStatusDictionary.Add(new Vector3Int(-1, y, 0), 5);
          tileStatusDictionary.Add(new Vector3Int(maxX+1, y, 0), 5);
        }

        //Specific Tiles
        tileStatusDictionary[new Vector3Int(3, 0, 0)] = 5;
        tileStatusDictionary[new Vector3Int(3, 1, 0)] = 5;
        tileStatusDictionary[new Vector3Int(3, 2, 0)] = 5;

        tileStatusDictionary[new Vector3Int(3, 3, 0)] = 2;
        tileStatusDictionary[new Vector3Int(3, 4, 0)] = 2;
        tileStatusDictionary[new Vector3Int(3, 5, 0)] = 2;

        tileStatusDictionary[new Vector3Int(3, 6, 0)] = 5;
        tileStatusDictionary[new Vector3Int(3, 7, 0)] = 5;
        tileStatusDictionary[new Vector3Int(3, 8, 0)] = 5;

        tileStatusDictionary[new Vector3Int(14, 6, 0)] = 3;
    }

    public int getTileStatus(int x, int y){
        try{
          return tileStatusDictionary[new Vector3Int(x, y, 0)];
        }
        catch{
          return 5;
        }
    }

    public void setTileStatus(int x, int y, int status){
        tileStatusDictionary[new Vector3Int(x, y, 0)] = status;
    }


}
