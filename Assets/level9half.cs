using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level9half : MonoBehaviour {

    Rigidbody rb;
    BoxCollider bc;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
