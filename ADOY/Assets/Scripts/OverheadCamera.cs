using UnityEngine;

public class OverheadCamera : MonoBehaviour
{
    public GameObject playerPrefab;
    void FixedUpdate()
    {
        transform.position = playerPrefab.transform.position; 
    }
}
