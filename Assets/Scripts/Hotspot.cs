using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;
    public float distanceFromCamera = 5f; // Adjust distance from camera

    void Start()
    {
        cam = Camera.main.transform;
        
        if (cam == null)
        {
            Debug.LogError("Main camera not found. Ensure camera is tagged as 'MainCamera'.");
            return;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;
        
        transform.LookAt(cam);
        transform.Rotate(0, 180, 0);
        
        // Position hotspot in front of camera if needed
        // transform.position = cam.position + cam.forward * distanceFromCamera;
    }
}
