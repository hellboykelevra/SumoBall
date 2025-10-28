using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 200f;
    public float jumpForce = 500f;
    public int player_number = 1;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.isGrounded = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal;
        float moveVertical;
       
        if (this.player_number == 1){
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else {
            moveHorizontal = Input.GetAxis("Horizontal_ArrowKeys");
            moveVertical = Input.GetAxis("Vertical_ArrowKeys");
        }
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);
    }

    // Use Update for input detection
    void Update()
    {
        if (this.player_number == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Debug.LogWarning("PLAYER 1 JUMP");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Keypad0) && isGrounded)
            {
                Debug.LogWarning("PLAYER 2 JUMP");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            Debug.LogWarning("GROUNDED");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            Debug.LogWarning("NOT GROUNDED");
            isGrounded = false;
        }
    }
}