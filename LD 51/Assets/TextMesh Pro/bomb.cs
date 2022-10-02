using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    int blowTmr = 150, blinkTmr;
    [SerializeField] GameObject expl;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform trfm;
    bool blinkOn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (blinkTmr > 0)
        {
            blinkTmr--;
        } else
        {
            blinkTmr = blowTmr / 10;
            if (blinkTmr < 1) blinkTmr = 1;
            if (blinkOn) rend.sprite = sprites[0];
            else rend.sprite = sprites[1];
            blinkOn = !blinkOn;
        }
        if (blowTmr > 0)
        {
            blowTmr--;
        } else
        {
            Instantiate(expl, trfm.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
