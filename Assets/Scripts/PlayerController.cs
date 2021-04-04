using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private bool onGround;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        onGround = true;
        canJump = true;

    }

    void OnMove(InputValue movementValue)
    {
    	Vector2 movementVector = movementValue.Get<Vector2>();

    	movementX = movementVector.x;
    	movementY = movementVector.y;
    }

    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
    	Vector3 movement = new Vector3(movementX, 0.0f, movementY);

    	rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        
    }

    // jumping code 

    void OnJump(InputValue jumpValue){
        if(onGround == true)
        {
            onGround = false;
            //rb.velocity = new Vector3( 0f, 10f, 0f);
            Vector3 jump = new Vector3(0f, 5f, 0f);
            rb.AddForce(jump, ForceMode.Impulse);
        }else if(canJump == true)
        {
            canJump = false;
            Vector3 jump = new Vector3(0f, 5f, 0f);
            rb.AddForce(jump, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground")){
            // if it's on the ground, reset the ability to double jump
            onGround = true;
            canJump = true;
        }
    }



}
