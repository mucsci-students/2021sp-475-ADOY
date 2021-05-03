using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    public float maxVelocity;
    public float acceleration;
    
    public AudioSource walkSound;
    public AudioSource eggSound;

    float walkingVelocity, sprintingVelocity;
    float walkingAcceleration, sprintingAcceleration;
    public float sprintDurability;
    public Image runningman;

    bool resettingSprint;
    bool runningMid;
    bool sprinted;

    Rigidbody rb;
   
    const float epsilon = 0.00001f;
    const float maxSprintTime = 5.0f;

    IEnumerator currMidSprintReset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        walkingVelocity = maxVelocity;
        sprintingVelocity = maxVelocity * 2.0f;

        walkingAcceleration = acceleration;
        sprintingAcceleration = acceleration * 2.0f;
        walkSound.Play();
        walkSound.Pause();

        sprintDurability = maxSprintTime;
        resettingSprint = false;
        runningMid = false;
        sprinted = false;

        
    }

    void FixedUpdate()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        float zDirection = Input.GetAxisRaw("Vertical");
        Vector3 worldAcceleration = acceleration * transform.TransformDirection(new Vector3(xDirection, 0, zDirection));

        if (xDirection == 0 && zDirection == 0)
            walkSound.Pause();

        if (Input.GetButton("Sprint") && !resettingSprint)
        {
            walkSound.pitch = 1.7f;
            acceleration = sprintingAcceleration;
            maxVelocity = sprintingVelocity;
            sprinted = true;
        }
        else
        {
            walkSound.pitch = 1.0f;
            acceleration = walkingAcceleration;
            maxVelocity = walkingVelocity;

            if (!resettingSprint && sprinted)
            {
                if (runningMid)
                    StopCoroutine("MidResetSprint");
                StartCoroutine("MidResetSprint");
            }
            
            sprinted = false;
        }

        if (xDirection != 0 || zDirection != 0)
        {
            rb.AddForce(worldAcceleration.x, 0, worldAcceleration.z, ForceMode.VelocityChange);
            walkSound.UnPause();
        }
        else if (rb.velocity.magnitude > epsilon)
            rb.velocity *= 0.8f;
        else
            rb.velocity = Vector3.zero;

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

        if (Input.GetButton("Sprint") && (xDirection != 0 || zDirection != 0))
        {
            if (sprintDurability > 0)
                sprintDurability -= Time.deltaTime;
            else
                sprintDurability = Mathf.Clamp(sprintDurability, 0, maxSprintTime);
        }

        if (sprintDurability < 0 && !resettingSprint)
        {
            if (runningMid)
                StopCoroutine("MidResetSprint");
            StartCoroutine("ResetSprint");
        }

        print(sprintDurability);
        Color tempColor = runningman.color;
        float te = sprintDurability / maxSprintTime;
        tempColor.a = te;
        runningman.color = tempColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Egg"))
        {
            Destroy(other.gameObject);
            ++gameManager.eggCount;
            print(gameManager.eggCount);
            eggSound.Play();
        }
        else if (other.CompareTag("Yoda"))
            GameObject.Find("LevelChanger").GetComponent<LevelManager>().ReloadLevel();
            //print("Collided with Yoda");
    }

    IEnumerator ResetSprint()
    {
        resettingSprint = true;
        yield return new WaitForSeconds(5.0f);
        sprintDurability = maxSprintTime;
        resettingSprint = false;
    }

    IEnumerator MidResetSprint()
    {
        runningMid = true;
        yield return new WaitForSeconds(5.0f);
        sprintDurability = maxSprintTime;
        runningMid = false;
    }
}
