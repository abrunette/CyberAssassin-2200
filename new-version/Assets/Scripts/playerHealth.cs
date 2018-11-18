using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public float fullHealth;

    float currentHealth;

    public GameObject deathFX;

    playerController controlMovement;


	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
      controlMovement = GetComponent<playerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void addDamage(float damage){
        if(damage<= 0){
            return;
        }
        currentHealth-=damage;


        if(currentHealth <= 0 ){

            Debug.Log("In current health less then script");
            makeCharacterDead();


        }
    }

    public void makeCharacterDead(){
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        Debug.Log("In Make Dead");
    }
}
