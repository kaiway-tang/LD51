using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public int damage, entityID;
    [SerializeField] Transform trfm;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            if (trfm) col.GetComponent<HPEntity>().takeDamage(damage, entityID, trfm);
            else col.GetComponent<HPEntity>().takeDamage(damage, entityID);
        }
    }

}
