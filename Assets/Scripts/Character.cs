using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    // Variables to adjust movement speed and jump height
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float crouchHeight = 1f;

    // Variables to check if the character is grounded and crouching
    private bool isGrounded;
    private bool isCrouching;

    // Rigidbody component for movement
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if character is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.5f);

        // Crouching input
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            transform.localScale = new Vector3(1f, crouchHeight, 1f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (isCrouching)
        {
            movement *= walkSpeed * 0.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= runSpeed;
        }
        else
        {
            movement *= walkSpeed;
        }

        movement = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * movement;
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }
}

