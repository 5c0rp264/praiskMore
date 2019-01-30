using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GroundController : MonoBehaviour
{

    // Variables
    public float speed = 1.0f;
    [Range(0.0f, 1.0f)]
    public float airControl = 0.5f;
    public float jumpSpeed = 5.0f;
    public float dashSpeed = 30.0f;
    public float dashCooldown = 5.0f;
    public float sensitivity = 3.0f;
    public bool bCursorLock = true;
    public Transform xRotator;
    public Transform yRotator;

    // Internals
    protected bool bCursorLocked = false;
    protected float drag;
    protected float nextDash = 0.0f;
    protected int requireJump = 0;

    // Components
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public new Collider collider;
    [HideInInspector]
    public new Camera camera;
    
	private void Start ()
    {
        camera = Camera.main;

        if (!camera)
        {
            throw new System.Exception("No main camera");
        }
        
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        drag = rb.drag;
    }

    private void Update()
    {
        UpdateCursor();

        if ((!bCursorLock && Input.GetMouseButton(0)) || bCursorLocked)
        {
            RotateCamera();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 10 frames allowed for jump
            requireJump = 10;
        }

        if (requireJump > 0)
        {
            if (true)
            {
                requireJump = 0;
                Jump();
            }
        }

        if (requireJump > 0)
        {
            requireJump--;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDash)
        {
            if (Time.time >= nextDash)
            {
                Dash();
                nextDash = Time.time + dashCooldown;
            }
        }
    }

    private void FixedUpdate ()
    {
        if (true)
        {
            rb.drag = drag;

            Move();
        }
        else
        {
            rb.drag = drag/5.0f;
            Fly();
        }

    }

    protected void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        var dir = yRotator.forward * z + yRotator.right * x;

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }

        rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    protected void Fly()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        var dir = yRotator.forward * z + yRotator.right * x;

        rb.AddForce(dir.normalized * speed * airControl, ForceMode.Impulse);
    }

    protected void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    protected void Dash()
    {
        rb.velocity = xRotator.forward * dashSpeed;
        //rb.AddForce(xRotator.forward * dashSpeed, ForceMode.Impulse);
    }

    protected void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        Quaternion xQuaternion = Quaternion.AngleAxis(mouseY, Vector3.left);
        Quaternion yQuaternion = Quaternion.AngleAxis(mouseX, Vector3.up);

        // apply y rotation on the camera
        yRotator.localRotation *= yQuaternion;
        // apply x rotation on this object
        xRotator.localRotation *= xQuaternion;
    }

    protected void UpdateCursor()
    {
        // unlock the cursor when the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bCursorLocked = false;
        }
        // lock the cursor when the user click on the game if the option is activated
        else if (Input.GetMouseButtonDown(0) && bCursorLock)
        {
            bCursorLocked = true;
        }

        // update cursor state and visibility
        if (bCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
