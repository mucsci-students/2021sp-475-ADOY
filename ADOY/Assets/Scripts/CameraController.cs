using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerPrefab;

    [Header("Options")]
    [Range(1, 5)]
    public float sensitivity = 1;
    public bool bobbingEnabled = true;

    bool canMove, goingUp;
    
    float rotX, rotY;
    float distanceBobbed;
    float bobbingDelta;

    Vector3 positionOffset;

    const float maxBobbing = 0.1f;
    const float epsilon = 0.00001f;

    void Start()
    {
        EnableMovement();

        rotX = transform.localEulerAngles.x;
        rotY = playerPrefab.transform.eulerAngles.y;
        distanceBobbed = 0.0f;
        bobbingDelta = 0.0063f;

        canMove = true;

        positionOffset = new Vector3(0.0f, transform.localPosition.y, 0.0f);
    }

    void Update()
    {
        if (canMove)
        {
            float mouseDeltaY = sensitivity * Input.GetAxis("Mouse Y");
            float mouseDeltaX = sensitivity * Input.GetAxis("Mouse X");
            rotX = Mathf.Clamp(rotX - mouseDeltaY, -90.0f, 90.0f);
            rotY += mouseDeltaX;
            transform.localEulerAngles = new Vector3(rotX, 0, 0);
        }

        playerPrefab.transform.eulerAngles = new Vector3(0, rotY, 0);
        transform.localPosition = new Vector3(0, positionOffset.y, 0);
    }

    void FixedUpdate()
    {
        // Head bobbing
        if (bobbingEnabled)
        {
            // Do bobbing if the player is moving
            if (playerPrefab.GetComponent<Rigidbody>().velocity.magnitude > 0.001f &&
                (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                float sprintingDelta = bobbingDelta * (Input.GetAxisRaw("Sprint") != 0 ? 1.6f : 1.0f);
                if (goingUp)
                {
                    positionOffset.y += sprintingDelta;
                    distanceBobbed += sprintingDelta;

                    if (maxBobbing - distanceBobbed < epsilon)
                        goingUp = false;
                }
                else
                {
                    positionOffset.y -= sprintingDelta;
                    distanceBobbed -= sprintingDelta;

                    if (distanceBobbed < epsilon)
                        goingUp = true;
                }
            }
            // otherwise if the player isn't moving and the player was
            // previously bobbing, reset the position to close 0.
            else if (distanceBobbed > epsilon)
            {
                float sprintingDelta = bobbingDelta * (Input.GetAxisRaw("Sprint") != 0 ? 2.8f : 1.6f);
                positionOffset.y -= sprintingDelta;
                distanceBobbed -= sprintingDelta;

                if (distanceBobbed < epsilon)
                {
                    goingUp = true;
                    distanceBobbed = 0;
                }
            }
        }
    }

    public void EnableMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
    }

    public void DisableMovement()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canMove = false;
    }
}
