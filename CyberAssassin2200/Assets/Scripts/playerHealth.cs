﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {


    //HUD variables

    public Slider healthSlider;

    //calculat health variables

    public float fullHealth;
    public float FallingDeathHeight;

    float currentHealth;

    public GameObject deathFX;
    public GameObject hurtFX;

    playerController controlMovement;


	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        controlMovement = GetComponent<playerController>();

        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < FallingDeathHeight)
            makeCharacterDead();
	}


    public void addDamage(float damage){
        if(damage<= 0){
            return;
        }
        currentHealth = currentHealth - damage;
        healthSlider.value = currentHealth;
        Instantiate(hurtFX, transform.position, transform.rotation);


        if (currentHealth <= 0 ){

            Debug.Log("In current health less then script");
            makeCharacterDead();


        }
    }


    public void addHealth(float health)
    {
        if (health <= 0)
        {
            return;
        }
     
        if (currentHealth <= fullHealth)
        {

            currentHealth = currentHealth + health;
            healthSlider.value = currentHealth;

        }
    }

    public void makeCharacterDead()
    {
        Debug.Log("In Make Dead");
        Instantiate(deathFX, transform.position, transform.rotation); // Play the death animation

        // Have the UIManager begin moving the player to the MainMenu
        GameObject UIM = GameObject.FindWithTag("UIManager");
        UIM.gameObject.GetComponent<UIManager>().PlayerDiedUIM();

        Destroy(gameObject);
        
    }
}
