using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomberPlane : mobileEntity
{
    [SerializeField] GameObject bomb;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        faceVect2(PlayerController.plyrTrfm.position);
        StartCoroutine(placeBomb());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trfm.position += trfm.up * .5f;
    }

    IEnumerator placeBomb()
    {
        while (true)
        {
            Instantiate(bomb, trfm.position, Quaternion.identity);
            yield return new WaitForSeconds(.3f);
        }
    }
}
