using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    float rotX;
    float rotY;

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public int jumpfactor = 4;
    public float speed = 3.2f;
    Vector3 movement;
    public CharacterController ctrl;

    //podnoszenie
    float ActionDistance = 3.0f;
    public bool grabbingSomething = false;
    public GameObject hand;
    public GameObject grabbedObject;

    public Image Fader; 
    public bool isAlive = true;
    public Camera PlayerCamera;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            MouseMovement();
            KeyboardMovement();
            Use();
        }
    }

    public void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotX -= mouseY * mouseSensitivity * Time.deltaTime;
        rotY += mouseX * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    public void KeyboardMovement()
    {
        if (!ctrl.isGrounded)
        {
            movement.y += Physics.gravity.y * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && ctrl.isGrounded)
        {
            Jump(new Vector3(movement.x, jumpfactor, movement.z));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3.2f;
        }
        if (ctrl.isGrounded)
        {
            movement.z = Input.GetAxis("Vertical") * speed * transform.forward.z - Input.GetAxis("Horizontal") * speed * transform.forward.x;
            movement.x = Input.GetAxis("Vertical") * speed * transform.forward.x + Input.GetAxis("Horizontal") * speed * transform.forward.z;
        }

        ctrl.Move(movement * Time.deltaTime);
    }

    public void Jump(Vector3 JumpDirection)
    {
        movement = JumpDirection;
    }

    public void Use()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * ActionDistance, Color.red);
            if (grabbingSomething == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, ActionDistance))
                {
                    //jesli trafisz w kostke
                    if (hit.transform.gameObject.GetComponent<Grippable>())
                    {
                        grabbedObject = hit.transform.gameObject;
                        grabbingSomething = true;
                        grabbedObject.BroadcastMessage("Use", hand, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        hit.transform.gameObject.BroadcastMessage("Use", null, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (grabbingSomething == true)
            {
                grabbedObject.BroadcastMessage("Use", hand, SendMessageOptions.DontRequireReceiver);
                grabbingSomething = false;
                grabbedObject = null;
            }
        }
    }

    public void Kill()
    {
        isAlive = false;
        body.isKinematic = false;
        body.drag = 3;
        body.angularDrag = 3;
        body.useGravity = true;
        Debug.Log("Przegrales!");
        this.GetComponentInChildren<Gun>().enabled = false;
        Fader.GetComponent<Animator>().Play("fader_black");
    }

    public void ChangeAlive(bool tAlive)
    {
        isAlive = tAlive;
    }
}
