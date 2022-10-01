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
        //trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - CamController.mainCam.ScreenToWorldPoint(Input.mousePosition).y, trfm.position.x - CamController.mainCam.ScreenToWorldPoint(Input.mousePosition).x) * Mathf.Rad2Deg + 90, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == thrown)
        {
            trfm.position += trfm.up * spd;
        } else if (status == returning)
        {
            trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - PlayerController.plyrTrfm.position.y, trfm.position.x - PlayerController.plyrTrfm.position.x) * Mathf.Rad2Deg + 180, Vector3.forward);
            trfm.position += trfm.up * spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //hit entity
        {
            col.GetComponent<HPEntity>().takeDamage(10, HPEntity.playerID);
        } else if (col.gameObject.layer == 6) //hit terrain
        {
            if (status == thrown)
            {
                status = embedded;
            }
        }
    }
}
