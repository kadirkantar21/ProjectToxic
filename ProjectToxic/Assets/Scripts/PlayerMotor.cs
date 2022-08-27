using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    //Components
    Rigidbody2D rb;


    // Movement Values
    float velocity;
    float horizontalInput;
    [SerializeField]
    float acceleration;
    [SerializeField]
    private float friction;
    [SerializeField]
    private float maxSpeed;

    //Jumping
    bool isGrounded = false;

    [SerializeField]
    private float jumpHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        velocity = DesiredHorizontalVelocity(velocity);
        Jump();
    }
    

    private void FixedUpdate()
    {
        rb.position += new Vector2(velocity * Time.fixedDeltaTime, 0);
    }

    float DesiredHorizontalVelocity(float desiredHorizontalVelocity)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime * acceleration;
        

        desiredHorizontalVelocity += horizontalInput * Time.deltaTime;

        if (horizontalInput == 0)
        {
            desiredHorizontalVelocity = Mathf.MoveTowards(desiredHorizontalVelocity, 0, friction * Time.deltaTime);


        }

        desiredHorizontalVelocity = Mathf.Clamp(desiredHorizontalVelocity, -maxSpeed, maxSpeed);
        print(desiredHorizontalVelocity);
        return desiredHorizontalVelocity;
    }

    void Jump()
    {
        Vector3 a = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
        RaycastHit2D hit = Physics2D.Raycast(a,Vector3.down,.6f);
        isGrounded = hit.collider != null ? true : false;


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpHeight,ForceMode2D.Impulse);
            
        }
    }
}
