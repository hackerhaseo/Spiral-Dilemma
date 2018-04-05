using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Switch : MonoBehaviour {

    public Material[] material;
    Renderer rend;

    public GameObject Gate1;
    public GameObject Gate2;

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
            Gate1.transform.position = new Vector3(17.87697f, 14.86f, 7.706606f);
            Gate2.transform.position = new Vector3(-4.97f, 10.54f, 7.706606f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            rend.sharedMaterial = material[0];
            Gate1.transform.position = new Vector3(17.87697f, 10.54f, 7.706606f);
            Gate2.transform.position = new Vector3(-4.97f, 14.86f, 7.706606f);
        }
    }

    /*void OnCollisionExit(Collision collision)
    {
        rend.sharedMaterial = material[0];
        Gate1.transform.position = new Vector3(17.87697f, 10.54f, 7.706606f);
        Gate2.transform.position = new Vector3(-4.97f, 14.86f, 7.706606f);
    }*/
}
