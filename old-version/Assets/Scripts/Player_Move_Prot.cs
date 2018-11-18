using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prot : MonoBehaviour {

    public int playerSpeed = 10;
    private bool facingRight = true; // True == Right, False == Left
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;

    // Bullet
    public GameObject projectile;
    public Transform projectileSpawn;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove () {
        // Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButton("Fire1"))
        {
            FireOrb();
        }
        // Animations
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
        // Player direction
        if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        // Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

    void Jump ()
    {
        // Jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void FlipPlayer ()
    {
        facingRight = !facingRight;
        Vector2 temp_localScale = gameObject.transform.localScale;
        temp_localScale.x *= -1;
        transform.localScale = temp_localScale;
    }

    // As a note, should proably put constraints on how often you can shoot.
    void FireOrb()
    {
        // public Transform projectileSpawn;
        projectileSpawn = gameObject.transform;

        // Create bullet from the prefab
        var bullet = (GameObject)Instantiate(
                projectile,
                projectileSpawn.position,
                projectileSpawn.rotation);

        // Move the bullet
        int playerDirection = 10; // 
        if (facingRight == false)
            playerDirection = -10;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirection, 0);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 0.5f);
    }

    // Could also use a ray technique
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Player has collided with " + collision.collider.name);
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }
}
