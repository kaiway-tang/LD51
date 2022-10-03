using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserShip : mobileEntity
{
    int tmr = 25;
    int turretDelay = 3;
    [SerializeField] GameObject laser, telegraph;
    [SerializeField] SpriteRenderer strobe;
    [SerializeField] Color alpha;
    bool fadeIn, laserActive;
    [SerializeField] bool isTurret;

    new void Start()
    {
        base.Start();
        faceVect2(PlayerController.plyrTrfm.position);
        telegraph.SetActive(false);
        alpha.a = 0f;
        strobe.color = alpha;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmr++;
        if ((tmr < 30 || tmr > 209) && !isTurret)
        {
            if (tmr == 210)
            {
                laserActive = false;
                laser.SetActive(false);
                alpha.a = 0f;
                strobe.color = alpha;
            }
            trfm.position += trfm.up * 1f;
        } else if (!isTurret)
        {
            if (tmr % 30 == 0)
            {
                if (tmr % 60 == 0)
                {
                    telegraph.SetActive(false);
                    laserActive = true;
                    laser.SetActive(true);
                }
                else
                {
                    faceVect2(PlayerController.plyrTrfm.position);
                    telegraph.SetActive(true);
                    laserActive = false;
                    laser.SetActive(false);

                    alpha.a = 0f;
                    strobe.color = alpha;
                }
            }

            if (laserActive)
            {
                if (fadeIn)
                {
                    alpha.a += .33f;
                    strobe.color = alpha;
                    if (alpha.a >= .95f) fadeIn = false;
                }
                else
                {
                    alpha.a -= .33f;
                    strobe.color = alpha;
                    if (alpha.a <= .05f) fadeIn = true;
                }
            }
        } else
        {
            if (tmr % 200 == 100)
            {
                faceVect2(PlayerController.plyrTrfm.position);
                telegraph.SetActive(true);
                laserActive = false;
                laser.SetActive(false);

                alpha.a = 0f;
                strobe.color = alpha;
                
            } else if (tmr % 200 == 125)
            {
                telegraph.SetActive(false);
                laserActive = true;
                laser.SetActive(true);
            } else if (tmr % 200 == 150)
            {
                laserActive = false;
                laser.SetActive(false);
                alpha.a = 0f;
                strobe.color = alpha;
            }
          
        }

            if (laserActive)
            {
                if (fadeIn)
                {
                    alpha.a += .33f;
                    strobe.color = alpha;
                    if (alpha.a >= .95f) fadeIn = false;
                }
                else
                {
                    alpha.a -= .33f;
                    strobe.color = alpha;
                    if (alpha.a <= .05f) fadeIn = true;
                }
            }
        
    }
}
