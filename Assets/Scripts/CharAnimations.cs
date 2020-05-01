using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{

    public SpriteRenderer outline;
    public SpriteRenderer shoeprim;
    public SpriteRenderer shoedec;
    public SpriteRenderer pantskeep;
    private int timer = 20;
    private bool run = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (run){
            timer--;
            if (timer <= 0)
            {
                timer = 20;
                outline.flipX = !outline.flipX;
                shoeprim.flipX = !shoeprim.flipX;
                shoedec.flipX = !shoedec.flipX;
                pantskeep.flipX = !pantskeep.flipX;
            }
        }
    }

    public void RunAnimation(){
        timer = 20;
        run = true;
    }
    public void StopAnimation(){
        run = false;
        outline.flipX = false;
        shoeprim.flipX = false;
        shoedec.flipX = false;
        pantskeep.flipX = false;
    }
}
