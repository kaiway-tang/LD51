using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : mobileEntity
{
    [SerializeField] float speed = 2;
    [SerializeField] float jumpHeight = 2;
    public int remainingJumps = 1;
    [SerializeField] GameObject knifeObj;
    [SerializeField] Transform nearestEnemy, camTargetTrfm;

    public static int knivesLeft = 9;
    [SerializeField] SpriteRenderer knivesRend;
    [SerializeField] Sprite[] knivesSprites;

    public static Transform plyrTrfm;
    public bool onGround = true;
    int gravityLock;

    int dashTmr, skyfallTmr, annihilateTmr, ascentTmr;

    public static PlayerController self;
    Vector2 vect2;
    Vector3 vect3;

    private void Awake()
    {
        self = GetComponent<PlayerController>();
    }

    new void Start()
    {
        base.Start();
        plyrTrfm = trfm;
    }

    // Update is called once per frame
    void Update()
    {
        //checkMovement();
        if (Input.GetKeyDown(KeyCode.U) && knivesLeft > 0 && gravityLock < 1) dash();
        if (Input.GetKeyDown(KeyCode.I) && knivesLeft > 2 && gravityLock < 1) annihilate();
        if (Input.GetKeyDown(KeyCode.O) && knivesLeft > 4 && gravityLock < 1) skyfall();
        if (Input.GetKeyDown(KeyCode.W) && knivesLeft > 1 && gravityLock < 1) ascent();

        if (Input.GetMouseButtonDown(0) && knivesLeft > 0 && gravityLock < 1) aimedDash();
        if (Input.GetMouseButtonDown(1) && knivesLeft > 2 && gravityLock < 1) aimedAnnihilate();

        if (Input.GetKeyDown(KeyCode.Space) && gravityLock < 1)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (dashTmr > 0)
        {
            dashTmr--;
            //setRelativeXVelocity(50);
            setAimedVelocity(50);
            if (dashTmr < 1)
            {
                enableGravity();
                setXVelocity(0);
            }
        }
        if (skyfallTmr > 0)
        {
            if (skyfallTmr > 25)
            {
                vect2.x = 0; vect2.y = 11f;
                rb.velocity = vect2;
            } else
            {
                if (skyfallTmr == 25)
                {
                    vect3.x = 0; vect3.y = 2;
                    throwKnife(Quaternion.Euler(0, 0, 180), vect3);
                }
                if (skyfallTmr == 20)
                {
                    vect3.x = 1; vect3.y = 2;
                    throwKnife(Quaternion.Euler(0, 0, 180), vect3);
                    vect3.x = -1; vect3.y = 2;
                    throwKnife(Quaternion.Euler(0, 0, 180), vect3);
                    CamController.self.hardLockOn();
                }
                if (skyfallTmr == 15)
                {
                    vect3.x = 2; vect3.y = 2;
                    throwKnife(Quaternion.Euler(0, 0, 180), vect3);
                    vect3.x = -2; vect3.y = 2;
                    throwKnife(Quaternion.Euler(0, 0, 180), vect3);
                }
            }
            if (skyfallTmr < 15 && !onGround)
            {
                vect2.x = 0; vect2.y = -70;
                rb.velocity = vect2;
            }
            else
            {
                if (skyfallTmr == 15)
                {
                    CamController.self.hardLockOff();
                    enableGravity();
                    vect3.x = 1.5f; vect3.y = 1;
                    camTargetTrfm.localPosition = vect3;
                }
                skyfallTmr--;
            }
            if (skyfallTmr == 0)
            {
                
            }
        }
        if (annihilateTmr > 0)
        {
            annihilateTmr--;
            if (annihilateTmr == 0)
            {
                enableGravity();
            }
        }
        if (ascentTmr > 0)
        {
            ascentTmr--;
            vect2.x = 0; vect2.y = 30;
            rb.velocity = vect2;
            if (ascentTmr == 5) throwKnife(Quaternion.Euler(0,0,0));
            if (ascentTmr == 0)
            {
                throwKnife(Quaternion.Euler(0, 0, 0));
                enableGravity();
            }
        }

        if (gravityLock < 1)
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
                throwKnife(Quaternion.Euler(0, 0, 180));
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

    void aimedDash()
    {
        throwKnife(faceMouse());
        dashTmr = 8;
        disableGravity();
    }

    void ascent()
    {
        disableGravity();
        ascentTmr = 16;
    }

    void skyfall()
    {
        disableGravity();
        skyfallTmr = 40;
        camTargetTrfm.localPosition = Vector3.zero;
    }

    void annihilate()
    {
        if (nearestEnemy)
        {

        } else
        {
            if (facingDir == facingLeft)
            {
                throwKnife(Quaternion.Euler(0, 0, 90));
                throwKnife(Quaternion.Euler(0, 0, 110));
                throwKnife(Quaternion.Euler(0, 0, 70));
            }
            else
            {
                throwKnife(Quaternion.Euler(0, 0, -90));
                throwKnife(Quaternion.Euler(0, 0, -110));
                throwKnife(Quaternion.Euler(0, 0, -70));
            }
        }
        annihilateTmr = 10;
        setRelativeXVelocity(-60);
        disableGravity();
    }

    void aimedAnnihilate()
    {
        throwKnife(faceMouse());
        throwKnife(faceMouse() * Quaternion.Euler(0, 0, 10));
        throwKnife(faceMouse() * Quaternion.Euler(0, 0, -10));

        annihilateTmr = 10;
        setAimedVelocity(-60);
        disableGravity();
    }

    bool throwKnife(Quaternion angle)
    {
        if (knivesLeft < 1) return false;
        Instantiate(knifeObj, trfm.position, angle);
        knivesLeft--;
        updateKnifeHUD();
        return true;
    }

    bool throwKnife(Quaternion angle, Vector3 offset)
    {
        if (knivesLeft < 1) return false;
        Instantiate(knifeObj, trfm.position + offset, angle);
        knivesLeft--;
        updateKnifeHUD();
        return true;
    }
    public void pickUpKnife()
    {
        knivesLeft++;
        updateKnifeHUD();
    }

    void updateKnifeHUD()
    {
        if (knivesLeft == 0) knivesRend.sprite = null;
        else knivesRend.sprite = knivesSprites[knivesLeft - 1];
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
    Quaternion faceMouse()
    {
        return Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - CamController.mainCam.ScreenToWorldPoint(Input.mousePosition).y, trfm.position.x - CamController.mainCam.ScreenToWorldPoint(Input.mousePosition).x) * Mathf.Rad2Deg + 90, Vector3.forward);
    }

    void setAimedVelocity(int amount)
    {
        rb.velocity = (CamController.mainCam.ScreenToWorldPoint(Input.mousePosition) - trfm.position).normalized * amount;
    }
}
