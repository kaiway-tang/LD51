using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    [SerializeField] float spd;
    int status, age;
    const int thrown = 0, embedded = 1, returning = 2;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform trfm, rendTrfm, ptclTrfm;
    [SerializeField] SpriteRenderer sprRend;
    [SerializeField] Sprite[] sprites;
    [SerializeField] ParticleSystem ptclSys;
    [SerializeField] selfDest ptclScr;
    [SerializeField] BoxCollider2D boxcol;
    [SerializeField] attack attackScr;
    [SerializeField] GameObject arrowObj;
    AudioPlayer audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("SFXSource").GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        age++;
        if (age == 1) boxcol.enabled = true;
        if (status == thrown)
        {
            trfm.position += trfm.up * spd;
            if (age == 25)
            {
                trfm.localEulerAngles = new Vector3(0,0,180);
            }
        } else if (status == returning)
        {
            trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - PlayerController.plyrTrfm.position.y, trfm.position.x - PlayerController.plyrTrfm.position.x) * Mathf.Rad2Deg + 90, Vector3.forward);
            trfm.position += trfm.up * spd*.5f;
            rendTrfm.Rotate(Vector3.forward * 30);
        }
        if (manager.tenSecTimer == 1)
        {
            rendTrfm.Rotate(Vector3.forward * Random.Range(0,360));
            status = returning;
            sprRend.sprite = sprites[1];
            ptclSys.emissionRate = 70;
            attackScr.damage = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //hit entity
        {
            if (col.GetComponent<HPEntity>().ID == HPEntity.playerID && status != thrown)
            {
                pickUp();
            }
        } else if (col.gameObject.layer == 6) //hit terrain
        {
            if (status == thrown)
            {
                embed();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //hit entity
        {
            if (col.GetComponent<HPEntity>().ID == HPEntity.playerID && status != thrown && age > 25)
            {
                pickUp();
            }
        }
    }

    void pickUp()
    {
        PlayerController.self.pickUpKnife();
        ptclTrfm.parent = null;
        ptclSys.Stop();
        ptclScr.enabled = true;
        CamController.setTrauma(10);
        Destroy(arrowObj);
        Destroy(trfm.gameObject);
    }

    void embed()
    {
        audio.PlayEmbed();
        sprRend.sprite = sprites[0];
        status = embedded;
        ptclSys.emissionRate = 6;
        attackScr.damage = 0;
        CamController.setTrauma(15);
    }
}
