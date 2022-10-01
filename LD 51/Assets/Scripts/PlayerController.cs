using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float jumpHeight = 2;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
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
        float jumpVelocity = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight * body.gravityScale);
        body.velocity = new Vector2(body.velocity.x, jumpVelocity);
    }
}
