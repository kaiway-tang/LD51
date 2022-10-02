using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserShip : mobileEntity
{
    int tmr;
    [SerializeField] GameObject laser, telegraph;
    [SerializeField] SpriteRenderer strobe;
    [SerializeField] Color alpha;
    bool fadeIn, laserActive;
    new void Start()
    {
        base.Start();
        faceVect2(PlayerController.plyrTrfm.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmr++;
        if (tmr < 25 || tmr > 174)
        {
            if (tmr == 125)
            {
                laserActive = false;
                laser.SetActive(false);
                alpha.a = 0f;
                strobe.color = alpha;
            }
            trfm.position += trfm.up * 1f;
        } else
        {
            if (tmr % 25 == 0)
            {
                if (tmr % 50 == 0)
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
        }
    }
}
