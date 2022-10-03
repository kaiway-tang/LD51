using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] CircleCollider2D cirCol;
    int tmr;
    void Start()
    {
        CamController.setTrauma(20);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmr++;
        if (tmr > 10) cirCol.enabled = false;
    }
}
