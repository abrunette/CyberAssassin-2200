using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public Transform target;
    public float smoothing;

    Vector3 offset;

    public int lowYOffset = 12;

    float lowY;



	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
        lowY = transform.position.y - lowYOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        if(transform.position.y <lowY) {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
	}
}
