using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public static int tenSecTimer;
    void Start()
    {
        tenSecTimer = 500;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tenSecTimer--;
        if (tenSecTimer < 1)
        {
            tenSecTimer = 500;
        }
    }
}
