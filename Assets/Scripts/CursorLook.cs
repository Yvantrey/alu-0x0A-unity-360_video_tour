using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CursorLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  
    public Transform playerCamera;         
    public float rayDistance = 100f;       

    private Button currentButton;
    private Camera cachedCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (playerCamera != null)
            cachedCamera = playerCamera.GetComponent<Camera>();
        
        if (cachedCamera == null)
            Debug.LogError("Camera not found on playerCamera. Ensure CameraRig has a Camera component attached.");
    }

    void Update()
    {
        
        HandleMouseLook();

       
        HandleMouseInteraction();
    }

    
    public void HandleMouseLook()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

   
    public void HandleMouseInteraction()
    {
        if (cachedCamera == null)
            return;
            
        Ray ray = cachedCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Button button = hit.collider.GetComponent<Button>();

            if (button != null)
            {
                if (button != currentButton)
                {
                    HighlightButtonVisuals(button);
                    currentButton = button;
                }

                if (Input.GetMouseButtonDown(0))
                    button.onClick.Invoke();
            }
            else
            {
                currentButton = null;
            }
        }
        else
        {
            currentButton = null;
        }
    }

    public void HighlightButtonVisuals(Button button)
    {
        Debug.Log("Changing button color to highlight.");
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.yellow;  
        button.colors = colorBlock;
    }
}
