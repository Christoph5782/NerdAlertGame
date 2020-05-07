using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour
{

public Vector3 target;
public float speed;
public int distance;
private int turn = 0;
private int moveSpeed = 5;
private int move = 0;
private Vector3 position;
private tileStatus tilestatus;

    // Start is called before the first frame update
    void Start()
    {
      tilestatus = new tileStatus();
      speed = 1.0f;
      distance = 1;
      Debug.Log("Turn 0");
    }

    // Update is called once per frame
    void Update()
    {
      //Get Initial Position
      position = gameObject.transform.position;

      if(name == gameplay.getCharacter()){
          //moving Up
          if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            target = new Vector3(position.x, position.y+distance, position.z);
            if(!(tilestatus.getTileStatus((int)(target.x), (int)(target.y)) == 5)){
              //float step = speed * Time.deltaTime;
              float step = speed;
              transform.position = Vector3.MoveTowards(transform.position, target, step);
              checkTurn();
            }
          }
          else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
            target = new Vector3(position.x-distance, position.y, position.z);
            if(!(tilestatus.getTileStatus((int)(target.x), (int)(target.y)) == 5)){
              //float step = speed * Time.deltaTime;
              float step = speed;
              transform.position = Vector3.MoveTowards(transform.position, target, step);
              checkTurn();
            }
          }
          else if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            target = new Vector3(position.x, position.y-distance, position.z);
            if(!(tilestatus.getTileStatus((int)(target.x), (int)(target.y)) == 5)){
            //float step = speed * Time.deltaTime;
            float step = speed;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            checkTurn();
            }
          }
          else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
            target = new Vector3(position.x+distance, position.y, position.z);
            if(!(tilestatus.getTileStatus((int)(position.x+distance), (int)(position.y)) == 5)){

              //float step = speed * Time.deltaTime;
              float step = speed;
              transform.position = Vector3.MoveTowards(transform.position, target, step);
              checkTurn();
            }
            else{
              Debug.Log("FAILED");
            }
          }
        }
    }

    private void checkTurn(){
      move += 1;
      if(move == moveSpeed){
        move = 0;
        turn +=1;
        Debug.Log("Turn " + turn);
        gameplay.nextCharacter();
      }
    }

}
