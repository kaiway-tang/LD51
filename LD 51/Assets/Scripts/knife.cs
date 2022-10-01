using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    [SerializeField] float spd;
    int status;
    const int thrown = 0, embedded = 1, returning = 2;
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
        if (status == thrown)
        {
            trfm.position += trfm.up * spd;
        } else if (status == returning)
        {
            trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - PlayerController.trfm.position.y, trfm.position.x - PlayerController.trfm.position.x) * Mathf.Rad2Deg + 180, Vector3.forward);
            trfm.position += trfm.up * spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6) //hit entity
        {
            col.GetComponent<HPEntity>().takeDamage(10);
        } else if (col.gameObject.layer == 8) //hit terrain
        {
            if (status == thrown)
            {
                status = embedded;
            }
        }
    }
}
