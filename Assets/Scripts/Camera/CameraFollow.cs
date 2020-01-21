using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 10f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCameraPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, 
            targetCameraPos, smoothing * Time.deltaTime);
    }

}