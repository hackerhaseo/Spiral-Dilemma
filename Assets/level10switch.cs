using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level10switch : MonoBehaviour {

    public Material[] material;
    Renderer rend;

    public GameObject Gate1;

    // Use this for initialization
    void Start () {
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "crate")
        {
            rend.sharedMaterial = material[1];
            Gate1.transform.position = new Vector3(5.97f, 12.82f, 15.31f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            rend.sharedMaterial = material[0];
            Gate1.transform.position = new Vector3(17.66f, 12.6f, 11.34f);
        }
    }
}
