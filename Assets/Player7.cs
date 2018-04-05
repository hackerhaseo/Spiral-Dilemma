using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player7 : MonoBehaviour
{

    Rigidbody rb;

    int playerPlace = 1;
    public float playerMovementSpeed = 3;

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    bool isCamera1;
    bool isCamera2;
    bool isCamera3;

    Vector3 jump;
    public float jumpForce = 5.0f;
    public bool isGrounded;

    public GameObject Key;
    bool isKey = false;

    bool colStatus = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 6.0f, 0.0f);
        isCamera1 = true;
        isCamera2 = false;
        isCamera3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        CameraMovement();

        if (isKey)
        {
            Key.transform.position = new Vector3(transform.position.x,
                transform.position.y + 1.775f, transform.position.z);
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

    void OnCollisionStay(Collision col)
    {
        isGrounded = true;
    }

    void CameraMovement()
    {
        if (isCamera1)
        {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
        else if (isCamera2)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }
        else if (isCamera3)
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
    }

    void PlayerMovement()
    {
        //**1/4 VIEW**
        if (playerPlace == 1)
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
        //**1/4 VIEW**

        //**2/4 VIEW**
        if (playerPlace == 2)
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
        //**2/4 VIEW**

        //**3/4 VIEW**
        if (playerPlace == 3)
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
        //**3/4 VIEW**
    }

    void OnCollisionEnter(Collision col)
    {
        if (!colStatus)
        {
            if (col.gameObject.tag == "switch1")
            {
                transform.position = new Vector3(17.89f, 50f, 7.21f);
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                playerPlace = 3;
            }
            if (col.gameObject.tag == "switch3")
            {
                transform.position = new Vector3(-5.15f, 39.5f, 7.19f);
                isCamera1 = false;
                isCamera2 = true;
                isCamera3 = false;
                playerPlace = 2;
            }
        }
        else
        {
            if (col.gameObject.tag == "switch4")
            {
                transform.position = new Vector3(17.89f, 50f, 7.21f);
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                playerPlace = 3;
            }
            if (col.gameObject.tag == "switch3")
            {
                transform.position = new Vector3(3.75f, 50f, 15.46431f);
                isCamera1 = true;
                isCamera2 = false;
                isCamera3 = false;
                playerPlace = 1;
            }
            if (col.gameObject.tag == "switch5")
            {
                transform.position = new Vector3(17.96f, 39.31f, 1.69f);
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                playerPlace = 3;
            }
            if (col.gameObject.tag == "switch7")
            {
                transform.position = new Vector3(-5.25f, 50.02f, 1.56f);
                isCamera1 = false;
                isCamera2 = true;
                isCamera3 = false;
                playerPlace = 2;
            }
            if (col.gameObject.tag == "switch9")
            {
                transform.position = new Vector3(9.12f, 39.3f, 15.46431f);
                isCamera1 = true;
                isCamera2 = false;
                isCamera3 = false;
                playerPlace = 1;
            }
        }


        ////////////////////
        if (col.gameObject.tag == "key")
        {
            isKey = true;
            colStatus = true;
        }

        if (col.gameObject.tag == "terrain" || col.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene(6);
        }

        if (col.gameObject.tag == "door")
        {
            if (isKey)
            {
                SceneManager.LoadScene(7);
            }
        }
    }

}
