using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : mobileEntity
{
    [SerializeField] float speed = 2;
    [SerializeField] float jumpHeight = 2;
    public int remainingJumps = 1;
    [SerializeField] GameObject knifeObj;
    public static int knivesLeft = 10;

    public static Transform plyrTrfm;
    public bool onGround = true;
    int gravityLock;

    int dashTmr;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        plyrTrfm = trfm;
    }

    // Update is called once per frame
    void Update()
    {
        //checkMovement();
        if (Input.GetKeyDown(KeyCode.I) && knivesLeft > 0)
        {
            dash();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (dashTmr > 0)
        {
            dashTmr--;
            setRelativeXVelocity(50);
            if (dashTmr < 1)
            {
                enableGravity();
                setXVelocity(0);
            }
        } else
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.D))
                {
                    setFacingDir(facingLeft);
                    setXVelocity(-speed);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.A))
                {
                    setFacingDir(facingRight);
                    setXVelocity(speed);
                }
            }
        }
    }

    void checkMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float horizontalSpeed = x * speed;
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    void Jump()
    {
        if (!onGround)
        {
            if (knivesLeft < 1)
            {
                return;
            } else
            {
                throwKnife(Quaternion.Euler(0, 0, 0));
            }
        }
        float jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight * rb.gravityScale);
        // limit max jumping speed
        if (rb.velocity.y > 0f)
        {
            jumpVelocity = jumpVelocity - rb.velocity.y;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }

    void dash()
    {
        if (facingDir == facingLeft) throwKnife(Quaternion.Euler(0, 0, 90));
        else throwKnife(Quaternion.Euler(0, 0, -90));
        dashTmr = 8;
        disableGravity();
    }

    bool throwKnife(Quaternion angle)
    {
        if (knivesLeft < 1) return false;
        Instantiate(knifeObj, trfm.position, angle);
        knivesLeft--;
        return true;
    }

    void disableGravity()
    {
        if (gravityLock == 0) rb.gravityScale = 0;
        gravityLock++;
    }
    void enableGravity()
    {
        gravityLock--;
        if (gravityLock == 0) rb.gravityScale = 9.8f;
    }
}
