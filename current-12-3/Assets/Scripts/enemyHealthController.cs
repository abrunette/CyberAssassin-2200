using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthController : MonoBehaviour {

    public float enemyMaxHealth;

    public int scoreValue = 10;


    float currentHealth;

    // Use this for initialization
    void Start()
    {

        currentHealth = enemyMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void addDamage(float damage)
    {
        currentHealth -= damage; 

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    void makeDead()
    {
        ScoreManager.score += scoreValue;
        Destroy(gameObject);
    }
}
