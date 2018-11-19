using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

    public float damage;
    //public float damageRate;
    public float pushBackForce;


    //float nextDamage;




	// Use this for initialization
	void Start () {
        //nextDamage = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.tag == "Player"){
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addDamage(damage);
            //nextDamage = Time.time + damageRate;

         

            Debug.Log("in here");
            pushBack(other.transform);
        }
    }

    void pushBack(Transform pushedObject){

        Vector2 pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
        pushDirection*=pushBackForce;

        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);

    }
}
