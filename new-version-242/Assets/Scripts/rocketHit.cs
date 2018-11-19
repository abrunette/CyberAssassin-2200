using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class rocketHit : MonoBehaviour
{

    public float weaponDamage;

    projectileController myPC;

    public GameObject explosionEffect;



    // Use this for initialization
    void Awake()
    {
        myPC = GetComponentInParent<projectileController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("shootable"))
        {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            //enemy health definition
            if (other.tag == "enemy")
            {
                enemyHealthController hurtEnemy = other.gameObject.GetComponent<enemyHealthController>();
                hurtEnemy.addDamage(weaponDamage);
            }
            // [RC] For destroying breakable objects
            else if (other.tag == "breakableObject")
                Destroy(other.gameObject);

        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("shootable"))
        {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            //enemy health definition
            if (other.tag == "enemy")
            {
                enemyHealthController hurtEnemy = other.gameObject.GetComponent<enemyHealthController>();
                hurtEnemy.addDamage(weaponDamage);
            }
            // [RC] For destroying breakable objects
            else if (other.tag == "breakableObject")
                Destroy(other);
        }

    }
   

}

        
