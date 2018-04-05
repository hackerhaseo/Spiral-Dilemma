using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player5 : MonoBehaviour {

    Rigidbody rb;

    int playerPlace = 1;
    public float playerMovementSpeed = 3;

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    bool isCamera1;
    bool isCamera2;
    bool isCamera3;
    bool isCamera4;
    bool isPlatform1;
    bool isPlatform2;
    bool isPlatform3;
    bool isPlatform4;

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
        isCamera3 = false;
        isCamera4 = false;
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
            camera3.enabled = false;
            camera4.enabled = false;
        }
        else if (isCamera2)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
            camera4.enabled = false;
        }
        else if (isCamera3)
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
            camera4.enabled = false;
        }
        else if (isCamera4)
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = false;
            camera4.enabled = true;
        }
    }

    void PlayerMovement()
    {
        //**1/4 VIEW**
        if (playerPlace == 1)
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
        //**1/4 VIEW**

        //**2/4 VIEW**
        if (playerPlace == 2)
        {
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
        //**2/4 VIEW**

        //**3/4 VIEW**
        if (playerPlace == 3)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    transform.position.z - playerMovementSpeed *
                    Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    transform.position.z + playerMovementSpeed *
                    Time.deltaTime);
            }
        }
        //**3/4 VIEW**

        //**4/4 VIEW**
        if (playerPlace == 4)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x - playerMovementSpeed *
                    Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x + playerMovementSpeed *
                    Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
        }
        //**4/4 VIEW**
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

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision col)
    {

        if (playerPlace == 1)
        {
            if (col.gameObject.tag == "platform1")
            {
                isCamera1 = false;
                isCamera2 = true;
                isCamera3 = false;
                isCamera4 = false;
                playerPlace = 2;
                transform.position = new Vector3(17.74f, 4.414998f, -7.35f);
            }
        }
        else if (playerPlace == 2 && isPlatform1)
        {
            //GOING BACK
            if (col.gameObject.tag == "platform1")
            {
                isCamera1 = true;
                isCamera2 = false;
                isCamera3 = false;
                isCamera4 = false;
                playerPlace = 1;
                isPlatform1 = false;
                transform.position = new Vector3(17.74f, 4.414998f, -7.35f);
            }
            else if (col.gameObject.tag == "platform2")
            {
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                isCamera4 = false;
                playerPlace = 3;
                transform.position = new Vector3(-4.95f, 4.332518f, -7.35f);
            }
        }
        else if (playerPlace == 3 && isPlatform2)
        {
            if (col.gameObject.tag == "platform2")
            {
                isCamera1 = false;
                isCamera2 = true;
                isCamera3 = false;
                isCamera4 = false;
                playerPlace = 2;
                isPlatform2 = false;
                transform.position = new Vector3(-4.95f, 4.332518f, -7.35f);
            }
            else if (col.gameObject.tag == "platform3")
            {
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = false;
                isCamera4 = true;
                playerPlace = 4;
                transform.position = new Vector3(-4.95f, 13.68f, 15.87f);
            }
        }
        else if (playerPlace == 4 && isPlatform3)
        {
            if (col.gameObject.tag == "platform3")
            {
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                isCamera4 = false;
                playerPlace = 3;
                isPlatform3 = false;
                transform.position = new Vector3(-4.95f, 13.68f, 15.87f);
            }
            else if (col.gameObject.tag == "platform4")
            {
                isCamera1 = true;
                isCamera2 = false;
                isCamera3 = false;
                isCamera4 = false;
                playerPlace = 1;
                transform.position = new Vector3(18.02f, 22.71252f, 15.87f);
            }
        }


        //PLATFORM SAVE
        if (col.gameObject.tag == "platform1save")
        {
            isPlatform1 = true;
        }
        if (col.gameObject.tag == "platform2save")
        {
            isPlatform2 = true;
        }
        if (col.gameObject.tag == "platform3save")
        {
            isPlatform3 = true;
        }

        //OTHERS

        if (col.gameObject.tag == "key")
        {
            isKey = true;
        }

        if (col.gameObject.tag == "terrain" || col.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene(4);
        }

        if (col.gameObject.tag == "door")
        {
            if (isKey)
            {
                SceneManager.LoadScene(5);
            }
        }
    }
}
