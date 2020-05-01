using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceToHit : MonoBehaviour
{
    private static readonly int basedropoffperdistance = 2;
    private static readonly int shortrangedropoff = 4;
    private static readonly int midrangedropoff = 9;
    private static readonly int longrangedropoff = 19;



    public static int GetChanceToHit(int weapon, int distance, int cover)
    {
        if(distance < 2 && weapon != 2) {//If point blank, everything but a sniper should always hti
            return 100;
        }

        int output = 100;
        switch (weapon){
            case 0://Shotgun
                output -= (basedropoffperdistance * distance);
                if(distance > shortrangedropoff){
                    output -= (5*basedropoffperdistance * distance-shortrangedropoff);
                }
                break;
            case 1://Assault
                output -= (2 * basedropoffperdistance * distance);
                if (distance > midrangedropoff){
                    output -= (2 * basedropoffperdistance * distance - midrangedropoff);
                }
                break;
            case 2://Sniper
                output += 10;
                output -= (basedropoffperdistance * distance);
                if (distance <= midrangedropoff)
                {
                    output -= (2 * basedropoffperdistance * midrangedropoff - distance);
                }
                if (distance > shortrangedropoff)
                {
                    output -= (5 * basedropoffperdistance * shortrangedropoff - distance);
                }
                else if (distance > longrangedropoff){
                    output -= (2 * basedropoffperdistance * distance - longrangedropoff);
                }
                break;
            case 3://LMG
                output -= (2 * basedropoffperdistance * distance);
                if (distance > midrangedropoff){
                    output -= (int)(2.5 * basedropoffperdistance * distance - midrangedropoff);
                }
                break;
            case 4://Pistol
                output -= (basedropoffperdistance * distance);
                if (distance > shortrangedropoff){
                    output -= (2 * basedropoffperdistance * distance - shortrangedropoff);
                }
                break;
            case 5://Shield
                return 100;
            default:
                print("There are only 6 weapons in the game");
                break;
        }

        if(output < 5)
        {
            output = 5;
        }

        return output;
    }
}
