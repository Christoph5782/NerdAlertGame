using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOBJLoaded : MonoBehaviour
{
    public static KeepOBJLoaded obj;

    private void Awake()
    {
        if(obj == null)
        {
            obj = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
