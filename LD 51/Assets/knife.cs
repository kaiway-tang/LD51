using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    [SerializeField] int spd;
    int status;
    const int inFlight = 0, embedded = 1;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform trfm;
    [SerializeField] SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == inFlight)
        {
            trfm.position += trfm.up * spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (status == inFlight)
        {
            status = embedded;

        }
    }
}
