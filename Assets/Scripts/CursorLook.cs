using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        if (cachedCamera == null) return;

        Button hitButton = GetButtonUnderCursor();

        if (hitButton != null)
        {
            if (hitButton != currentButton)
            {
                HighlightButtonVisuals(hitButton);
                currentButton = hitButton;
            }

            if (Input.GetMouseButtonDown(0))
                hitButton.onClick.Invoke();
        }
        else
        {
            currentButton = null;
        }
    }

    private Button GetButtonUnderCursor()
    {
        // Try UI raycasting first (World Space Canvas buttons)
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Button btn = result.gameObject.GetComponent<Button>();
            if (btn != null) return btn;

            // Also check parent in case the raycast hit a child (e.g. Text/Image inside button)
            btn = result.gameObject.GetComponentInParent<Button>();
            if (btn != null) return btn;
        }

        // Fallback: physics raycast for 3D collider-based buttons
        Ray ray = cachedCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Button btn = hit.collider.GetComponent<Button>();
            if (btn != null) return btn;
        }

        return null;
    }

    public void HighlightButtonVisuals(Button button)
    {
        Debug.Log("Changing button color to highlight.");
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.yellow;
        button.colors = colorBlock;
    }
}
