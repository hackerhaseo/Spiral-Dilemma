using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    Rigidbody rb;

    int playerPlace = 1;
    public float playerMovementSpeed = -10;

    public Camera camera1;
    public Camera camera2;
    bool isCamera1;
    bool isCamera2;
    bool isPlatform1;

    public GameObject platform1;

    Vector3 jump;
    public float jumpForce = 5.0f;
    public bool isGrounded;

    public GameObject Key;
    bool isKey = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 6.0f, 0.0f);
        isCamera1 = true;
        isCamera2 = false;
    }
	
	// Update is called once per frame
	void Update () {
        PlayerMovement();
        PlayerJump();
        CameraMovement();

        if (isKey)
        {
            Key.transform.position = new Vector3(transform.position.x,
                transform.position.y + 1.775f, transform.position.z);
        }
    }

    void CameraMovement()
    {
        if (isCamera1)
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }
        else if (isCamera2)
        {
            camera1.enabled = false;
            camera2.enabled = true;
        }
    }

    void PlayerMovement()
    {
        //**1/4 VIEW**
        if (playerPlace == 1) {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + playerMovementSpeed *
                    Time.deltaTime, 
                    transform.position.y, 
                    transform.position.z);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - playerMovementSpeed *
                    Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
        }
        //**1/4 VIEW**

        //**2/4 VIEW**
        if (playerPlace == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    transform.position.z + playerMovementSpeed *
                    Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    transform.position.z - playerMovementSpeed *
                    Time.deltaTime);
            }
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (playerPlace == 1)
        {
            if (collision.gameObject.tag == "platform1")
            {
                isCamera1 = false;
                isCamera2 = true;
                playerPlace = 2;
                transform.position = new Vector3(-4.7f, 12.935f, 15.51781f);
                //transform.Rotate(0f, -180f, 0f);
            }
        }
        else if (playerPlace == 2 && isPlatform1)
        {
            if (collision.gameObject.tag == "platform1")
            {
                isCamera1 = true;
                isCamera2 = false;
                playerPlace = 1;
                isPlatform1 = false;
                transform.position = new Vector3(-4.67f, 12.935f, 15.92f);
                //transform.Rotate(0f, -180f, 0f);
            }
        }
        
        if (collision.gameObject.tag == "platform1save")
        {
            isPlatform1 = true;
        }

        if (collision.gameObject.tag == "key")
        {
            isKey = true;
        }

        if (collision.gameObject.tag == "terrain")
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "door")
        {
            if (isKey)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    void PlayerJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W)) && 
            isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            
        }
    }
}
