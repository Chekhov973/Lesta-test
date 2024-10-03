using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 1f, 0);
    }
}
