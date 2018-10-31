using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public float timeLeft = 120;
    public int playerScoreCount = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;

	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time:\t" + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score:\t" + (int)playerScoreCount);
        //Debug.Log(timeLeft);
        if (timeLeft <= 0.1)
        {
            Debug.Log("Player has run out of time.\n");
            SceneManager.LoadScene("gameSceneRC");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelEnd")
        {
            Debug.Log("Reached end of level");
            CountScore();
        }
        if(collision.gameObject.tag == "Coin")
        {
            Debug.Log("Collected Coin");
            playerScoreCount += 10;
        }
    }

    void CountScore()
    {
        playerScoreCount += (int)(timeLeft * 10);
        Debug.Log(playerScoreCount);
    }
}
