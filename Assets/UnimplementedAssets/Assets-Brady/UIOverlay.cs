using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class UIOverlay : MonoBehaviour {
  private Tilemap tilemap;
  private Color greenColor = new Color(0.3f,0.7f,0.3f, 1f);
  private Color redColor = new Color(0.7f,0.3f,0.3f, 1f);
  private Color yellowColor = new Color(0.7f,0.7f,0.3f, 1f);
  private Color blueColor = new Color(0.3f,0.3f,0.7f, 1f);
  private Color defaultColor = new Color(1f,1f,1f, 0f);
  private Color setColor;
  public int highlightOn = 0;
  private int maxX;
  private int maxY;
  private tileStatus tilestatus;


    //Tile Status Definition
    //Red indicates hazard on tile
    //Yellow indicates normal tile
    //Green indicates cover

    public void Awake(){
      maxX = 16;
      maxY = 8;
      tilestatus = new tileStatus();
      Debug.Log("Started");
    }

    public void Update()
    {

        //Alternate between highlight and unhighlighting
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyUp(KeyCode.P))
        {
            if(highlightOn == 0){
                highlight();
                highlightOn = 1;
            }
            else{
                StopHighlight();
                highlightOn = 0;
            }
        }

    }

    public void highlight(){
      tilemap = GetComponent<Tilemap>();
      Debug.Log("Highlighting");

      //Replace with A*
      for(int x = -1; x <= maxX+3; x++){
          for(int y = -1; y <= maxY+1; y++){
            Vector3Int tile = new Vector3Int(x, y, 0);
            tilemap.SetTileFlags(tile, TileFlags.None);
            tilemap.SetColor(tile, checkColor(tilestatus.getTileStatus(x, y)));
          }
      }


    }


    public void StopHighlight(){
      tilemap = GetComponent<Tilemap>();
      Debug.Log("Unhighlighting");

      //Replace with A*


      for(int x = -1; x <= maxX+1; x++){
          for(int y = -1; y <= maxY+1; y++){
            Vector3Int tile = new Vector3Int(x, y, 0);

            tilemap.SetTileFlags(tile, TileFlags.None);
            tilemap.SetColor(tile, defaultColor);
          }
      }
    }


    public Color checkColor(int colorNumber){
        switch(colorNumber){
          case 1:
            return greenColor;
            break;
          case 2:
            return yellowColor;
            break;
          case 3:
            return redColor;
            break;
          case 4:
            return yellowColor;
            break;
          case 5:
            return blueColor;
            break;
          default:
            return defaultColor;
            break;
        }
    }

}
