using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 2.0f;
    [SerializeField] float walkSpeed = 8.0f;
    [SerializeField] float walkSpeedFast = 16.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    //[SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    //float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private bool looking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        Cursor.visible = true;
        if (looking) UpdateMouseLook();
        UpdateMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopLooking();
        }
    }




    void UpdateMouseLook()
    {
        //Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        //cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        // transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity;
        newRotationY = ClampAngler(newRotationY);
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
    }

    float ClampAngler(float angle)
    {
        if (angle > 270f)
            return Mathf.Clamp(angle, 285.0f, 360.0f);
        else
            return Mathf.Clamp(angle, -1.0f, 90.0f);
    }

    void UpdateMovement()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.walkSpeedFast : this.walkSpeed;

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

    }

    void OnDisable()
    {
        StopLooking();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
