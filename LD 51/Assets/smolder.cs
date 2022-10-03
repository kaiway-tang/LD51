using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smolder : MonoBehaviour
{
    [SerializeField] ParticleSystem ptclSys;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform trfm;
    int tmr;
    private void Start()
    {
        trfm.Rotate(Vector3.forward * Random.Range(0,360));
        rb.velocity = trfm.up * 24;
    }
    private void FixedUpdate()
    {
        tmr++;
        if (tmr == 15) ptclSys.Stop();
        if (tmr == 115) Destroy(gameObject);
    }
}