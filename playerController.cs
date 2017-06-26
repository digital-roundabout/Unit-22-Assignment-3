using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public float maxSpeed = 10f;  //Stores the horizontal speed
    bool lookingRight = true;     //Stores boolean for if the character is facing right or not
    Animator anim;                //Helps control animation mechanisms

    bool grounded = false;         //checks if the character is on the "ground"
    public Transform groundCheck;  //fuck knows what this does
    float groundRadius = 0.2f;     //radius from the collider that is checked for the "ground"
    public LayerMask whatIsGround; //adds a layer mask so that things can be added to it
    public float jumpForce = 250f; //controlls the jump force

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();  //Gets all the animation information for the object
	}
	
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKeyDown(KeyCode.W)) { //Makes sure that the W key is pressed and the character is grounded before letting them jump
            anim.SetBool("Grounded", false);                                  //Sets Grounded to false
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));  //adds the jumpforce to the Y axis
        }
	}

    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); // stores a boolean value based on weather the overlap circle is groundRadius distance from anything that is in the whatIsGround mask.
        anim.SetBool("Grounded", grounded); //sets Grounded as grounded to set off the jumping animation

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y); // sets vSpeed as the actual speed of the character 

        float move = Input.GetAxis("Horizontal");   //Sets the A,D to horizontal and vertical movement
        Debug.Log(move);                            //Logs 'move' into the console everytime a key is pressed

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);  //Creates a new vector with x & y components and applies the value to the rigidbody properties it at every frame

        anim.SetFloat("speed", Mathf.Abs(move));     //sets the speed value in the animator

        if (move > 0 && !lookingRight) //if movement is greater than 0 and lookingRight is false then it flips the sprite
            Flip();
        else if (move < 0 && lookingRight) //if movement is less than 0 and lookingRight is true then it flips the sprite
            Flip();
    }

    void Flip() {
        lookingRight = !lookingRight;               //makes lookingRight False
        Vector3 theScale = transform.localScale;    //calculates the local scale
        theScale.x *= -1;                           //inverts the object
        transform.localScale = theScale;            //initialises the new direction against the object
    }
}
