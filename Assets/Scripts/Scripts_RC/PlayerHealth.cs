using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    //public int health;
    //public bool hasDied;

	// Use this for initialization
	void Start () {
        //hasDied = false;
	}
	
    void Update () {
        /*
        if (gameObject.transform.transform.position.y < -5)
        {
            hasDied = true;
        }
        if (hasDied == true)
        {
            StartCoroutine("Die");
        }
        */
        if(gameObject.transform.transform.position.y < -5)
            Die();
    }

    void Die()
    {
        Debug.Log("Player has died.\n");
        SceneManager.LoadScene("gameSceneRC");
    }
    IEnumerator PlayerDie()
    {
        /*
        Debug.Log("Player has fallen.\n");
        yield return new WaitForSeconds(2);
        Debug.Log("Player has died.\n");
        // */

        SceneManager.LoadScene("gameSceneRC");
        yield return null;
    }
}
