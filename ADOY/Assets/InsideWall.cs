using UnityEngine;

public class InsideWall : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
