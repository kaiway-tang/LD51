using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRing : MonoBehaviour
{
    [SerializeField] int tmr;
    [SerializeField] Transform trfm;
    [SerializeField] Vector3 shrink;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color alpha;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmr++;
        rend.color -= alpha;
        trfm.localScale -= shrink;
        if (tmr == 10)
        {
            trfm.localScale = Vector3.one;
            rend.color = new Color(1,1,1,.6f);
            trfm.position = PlayerController.plyrTrfm.position;
            tmr = 0;
        }
    }
}
