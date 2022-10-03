using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemy : mobileEntity
{
    protected Transform plyrTrfm;
    [SerializeField] AIDestinationSetter destinationScr;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        plyrTrfm = PlayerController.plyrTrfm;
        destinationScr.target = plyrTrfm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
