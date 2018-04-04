using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    Rigidbody rb;
    BoxCollider bc;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(rb);
            Destroy(bc);
        }
    }
}
