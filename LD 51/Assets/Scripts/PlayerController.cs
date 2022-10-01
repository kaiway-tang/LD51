using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] int maxAirJumps = 1;
    int currentJumpNumber = 0; 
    [SerializeField] GameObject knifeObj;
    int knivesLeft;
    Rigidbody2D body;

    public static Transform trfm;

    bool onGround = true;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
    }

    void checkMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float horizontalSpeed = x * speed;
        body.velocity = new Vector2(horizontalSpeed, body.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    void Jump()
    {
        if(currentJumpNumber > maxAirJumps || !onGround)
        {
            return;
        }
        float jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight * body.gravityScale);
        // limit max jumping speed
        if (body.velocity.y > 0f)
        {
            jumpVelocity = jumpVelocity - body.velocity.y;
        }
        body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        currentJumpNumber++;
    }

    void throwKnife()
    {
        Instantiate(knifeObj, trfm.position, trfm.rotation);
    }
}
