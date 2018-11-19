using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartHealth : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Health")
        {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addHealth(health);

            Debug.Log("in health");
            //after it gicves health destroy
            Destroy(gameObject);
        }
    }
}
