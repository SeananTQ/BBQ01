using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter2D");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter2D");
    }
}
