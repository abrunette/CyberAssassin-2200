using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {

    public Text StopWatchText;
    private float startTime;
    private bool EndBool = false;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (EndBool)
            return;

        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString("d2");
        string seconds = (t % 60).ToString("f2");

        StopWatchText.text = minutes + ":" + seconds;
	}

    /*
    public void LevelEnd()
    {
        EndBool = true;
        StopWatchText.color = Color.yellow;
    }
    */
}
