using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileEntity : HPEntity
{
    protected bool facingDir;
    protected const bool facingLeft = true, facingRight = false;
    protected Transform trfm;
    protected Rigidbody2D rb;
    [SerializeField] Vector3 scale;

    Vector2 vect2;

    protected void Start()
    {
        trfm = transform;
        rb = GetComponent<Rigidbody2D>();
        scale = trfm.localScale;
    }

    public void setFacingDir(bool dir)
    {
        if (facingDir == dir) return;
        facingDir = dir;
        if (facingDir == facingLeft)
        {
            scale.x = -Mathf.Abs(scale.x);
            trfm.localScale = scale;
        } else
        {
            scale.x = Mathf.Abs(scale.x);
            trfm.localScale = scale;
        }
    }

    protected void setXVelocity(float value) //set x velocity only (leaves y unchanged)
    {
        vect2.x = value;
        vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }

    protected void setRelativeXVelocity(float value) //set x velocity in the direction entity is facing
    {
        if (facingDir == facingLeft) value *= -1;
        setXVelocity(value);
    }

    protected void faceVect2(float x, float y)
    {
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - y, trfm.position.x - x) * Mathf.Rad2Deg + 90, Vector3.forward);
    }
    protected void faceVect2(Vector2 target)
    {
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - target.y, trfm.position.x - target.x) * Mathf.Rad2Deg + 90, Vector3.forward);
    }
}
