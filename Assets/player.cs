using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Camera camera5;
    public Camera camera6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "camera1")
        {
            camera1.gameObject.active = true;
            camera2.gameObject.active = false;
        }
        else if (collision.gameObject.tag == "camera2")
        {
            camera1.gameObject.active = false;
            camera2.gameObject.active = true;
        }
    }
}
