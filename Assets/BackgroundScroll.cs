using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private Vector3 initialTransform;

	// Use this for initialization
	void Start () {
        initialTransform = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position.y < -4.08f) {
            transform.position = initialTransform;
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
        }
	}
}
