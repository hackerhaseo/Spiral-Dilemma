using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{

    Rigidbody rb;

    int playerPlace = 1;
    public float playerMovementSpeed = -3;

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    bool isCamera1;
    bool isCamera2;
    bool isCamera3;
    bool isPlatform1;
    bool isPlatform2;

    public GameObject platform1;
    public GameObject platform2;

    Vector3 jump;
    public float jumpForce = 5.0f;
    public bool isGrounded;

    public GameObject Key;
    bool isKey = false;

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
                isCamera3 = false;
                playerPlace = 2;
                transform.position = new Vector3(17.86f, 6.89657f, 15.46099f);
                //transform.Rotate(0f, -180f, 0f);
            }
        }
        else if (playerPlace == 2 && isPlatform1)
        {
            if (collision.gameObject.tag == "platform1")
            {
                isCamera1 = true;
                isCamera2 = false;
                isCamera3 = false;
                playerPlace = 1;
                isPlatform1 = false;
                transform.position = new Vector3(18.50001f, 6.89657f, 15.46099f);
                //transform.Rotate(0f, -180f, 0f);
            }
            else if (collision.gameObject.tag == "platform2")
            {
                isCamera1 = false;
                isCamera2 = false;
                isCamera3 = true;
                playerPlace = 3;
                transform.position = new Vector3(-5.04f, 9.516571f, 15.73f);
            }
        }
        else if (playerPlace == 3 && isPlatform2)
        {
            if (collision.gameObject.tag == "platform2")
            {
                isCamera1 = false;
                isCamera2 = true;
                isCamera3 = false;
                playerPlace = 2;
                isPlatform2 = false;
                transform.position = new Vector3(-5.02f, 9.516571f, 15.32009f);
                //transform.Rotate(0f, -180f, 0f);
            }
        }

        if (collision.gameObject.tag == "platform1save")
        {
            isPlatform1 = true;
        }
        if (collision.gameObject.tag == "platform2save")
        {
            isPlatform2 = true;
        }

        if (collision.gameObject.tag == "key")
        {
            Debug.Log("sample!!!");
            isKey = true;
        }

        if (collision.gameObject.tag == "terrain" || collision.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag == "door")
        {
            if (isKey)
            {
                SceneManager.LoadScene(2);
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
