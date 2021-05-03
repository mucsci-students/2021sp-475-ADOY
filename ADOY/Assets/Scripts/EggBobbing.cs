using UnityEngine;

public class EggBobbing : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 newPos = transform.position;
        float delta = bobbingFunction(Time.time);
        newPos.y += delta;
        transform.position = newPos;
    }

    float bobbingFunction(float time)
    {
        return Mathf.Sin(2.0f * time) / 256.0f;
    }
}
