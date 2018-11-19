using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //movement variables
    public float maxSpeed;
    bool facingRight;

    //jumping variables

    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    Rigidbody2D myRB;
    Animator myAnim;

    //for shooting the gun
    public Transform gunTip;
    public GameObject bullet;

    float fireRate = 0.5f;
    float nextFire = 0f;

    // Use this for initialization
    void Start(){
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        facingRight = true;
    }

     void Update()
    { 
           if (grounded && Input.GetAxis("Jump")>0) {
               grounded = false;
               myAnim.SetBool("isGrounded", grounded);
               myRB.AddForce(new Vector2(0, jumpHeight));
           }

        //player shooting

        if (Input.GetKeyDown("up")){
            fireRocket();
        }

    }
    // Update is called once per frame
    void FixedUpdate () {

       // check if we are grounded - if no we are falling

        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);
        //end check grounded section


        // move character section
        float move = Input.GetAxis("Horizontal");

        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        //check out whihc way character is facing then flip x cordinate
        if(move > 0 && !facingRight){
            flip();
        }else if(move< 0 && facingRight ){
            flip();
        }
	}

    void flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void fireRocket(){
        if(Time.time > nextFire){
            nextFire = Time.time + fireRate;
            if(facingRight){
                Instantiate(bullet, gunTip.position, Quaternion.Euler (new Vector3(0,0,0)));
            }
            else if (!facingRight){
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            }
        }
    }
}
