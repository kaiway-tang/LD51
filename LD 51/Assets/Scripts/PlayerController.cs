using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : mobileEntity
{
    [SerializeField] float speed = 2;
    [SerializeField] float jumpHeight = 2;
    public int remainingJumps = 1;
    [SerializeField] GameObject knifeObj;
    int knivesLeft = 10;

    public static Transform plyrTrfm;
    public bool onGround = true;
    bool velocityReset;
    int gravityLock;

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (facingDir == facingLeft) Instantiate(knifeObj, trfm.position, Quaternion.Euler(0, 0, 90));
            else Instantiate(knifeObj, trfm.position, Quaternion.Euler(0, 0, -90));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.D))
            {
                setFacingDir(facingLeft);
                setXVelocity(-speed);
                velocityReset = false;
            }
        } else if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A))
            {
                setFacingDir(facingRight);
                setXVelocity(speed);
                velocityReset = false;
            }
        }
        else
        {
            if (!velocityReset)
            {
                //setXVelocity(0);
                velocityReset = true;
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
                Instantiate(knifeObj, trfm.position, Quaternion.Euler(0, 0, 0));
            }
        }
        float jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight * rb.gravityScale);
        // limit max jumping speed
        if (rb.velocity.y > 0f)
        {
            jumpVelocity = jumpVelocity - rb.velocity.y;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        knivesLeft--;
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
