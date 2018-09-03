using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private Vector3 initialTransform;
    private const float scrollOffset = -4.08f;
    public float scrollSpeed = 0.02f;

	// Use this for initialization
	void Start () {
        initialTransform = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position.y < scrollOffset) {
            transform.position = initialTransform;
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y - scrollSpeed, transform.position.z);
        }
	}
}
