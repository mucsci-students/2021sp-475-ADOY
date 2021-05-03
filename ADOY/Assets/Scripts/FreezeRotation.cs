using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Vector3 playerPos = player.GetComponent<Rigidbody>().position;
        transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z);
    }
}
