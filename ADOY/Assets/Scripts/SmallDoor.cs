using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoor : MonoBehaviour
{
    public Animator animator;
    public Collider m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("DoorClose");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Collider.enabled = false;
            animator.Play("DoorOpen");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Collider.enabled = true;
            animator.Play("DoorClose");
        }
    }
    
}
