using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingProjectile : MonoBehaviour {

    // Variables
    Rigidbody2D ProjectileRigidbody;
    public float FlightSpeed;
    public float Damage;
    public float TimeAlive;

    private void Awake()
    {
        Debug.Log("Projectile Awake");

        Destroy(gameObject, TimeAlive);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Projectile Start");

        ProjectileRigidbody = GetComponent<Rigidbody2D>();

        ProjectileRigidbody.AddForce(new Vector2(-1, 0) * -FlightSpeed, ForceMode2D.Impulse);

        /*

        if (transform.localRotation.z > 0)
        {
            ProjectileRigidbody.AddForce(new Vector2(-1, 0) * -FlightSpeed, ForceMode2D.Impulse);
        }

        else ProjectileRigidbody.AddForce(new Vector2(1, 0) * -FlightSpeed, ForceMode2D.Impulse);
        */
    }
	
	// Update is called once per frame
	void Update () {
        ProjectileRigidbody.velocity = new Vector2(-FlightSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Projectile hit player");
            playerHealth hurtEnemy = collision.gameObject.GetComponent<playerHealth>();
            hurtEnemy.addDamage(Damage);
            Destroy(gameObject);
        }
    }
}
