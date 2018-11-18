using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
<<<<<<< HEAD
    private Animator animator;
=======
    //private Animator animator;
>>>>>>> 485Midterm-AB-1

	// Use this for initialization
	void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
        animator = GetComponent<Animator>();
=======
        //animator = GetComponent<Animator>();
>>>>>>> 485Midterm-AB-1
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

<<<<<<< HEAD
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
=======
        /*bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
>>>>>>> 485Midterm-AB-1
        if(flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
<<<<<<< HEAD
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
=======
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);*/
>>>>>>> 485Midterm-AB-1

        targetVelocity = move * maxSpeed;
    }
}
