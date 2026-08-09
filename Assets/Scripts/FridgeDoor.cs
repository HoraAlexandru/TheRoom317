using UnityEngine;

public class RefrigeratorDoor : MonoBehaviour
{
    public Transform door;
    public float openAngle = 90f;
    public float closedAngle = 0f;
    public float animationSpeed = 3f;
    public float interactionDistance = 3f; // Distanța maximă
    
    private float targetAngle = 0f;
    private bool isOpen = false;
    private bool isAnimating = false;

    void Update()
    {
        // Verifică E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isAnimating)
            {
                // Raycast de la cameră
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
                {
                    // Verifică dacă ai lovit ușa frigiderului
                    if (hit.collider.gameObject == gameObject || hit.transform.parent == transform)
                    {
                        ToggleDoor();
                    }
                }
            }
        }

        // Animația ușii
        if (isAnimating)
        {
            float currentAngle = door.localEulerAngles.y;
            
            if (currentAngle > 180f)
                currentAngle -= 360f;

            float newAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * animationSpeed);
            door.localRotation = Quaternion.Euler(0, newAngle, 0);

            if (Mathf.Abs(newAngle - targetAngle) < 1f)
            {
                door.localRotation = Quaternion.Euler(0, 0, newAngle);
                isAnimating = false;
            }
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen;
        targetAngle = isOpen ? openAngle : closedAngle;
        isAnimating = true;
    }
}