using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject CircleFull_Gameobject;
    public bool CanInteract = true;
    [SerializeField]public float interactDistance = 10f;

    void Update()
    {
        // Dacă interacțiunea este dezactivată, ascundem ținta și oprim execuția
        if (!CanInteract)
        {
            CircleFull_Gameobject.SetActive(false);
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);

        // Verificăm dacă raza lovește ceva ȘI dacă obiectul are tag-ul "Interact"
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance) && hit.collider.CompareTag("Interact"))
        {
            CircleFull_Gameobject.SetActive(true);
        }
        else
        {
            // Se execută dacă nu lovim nimic sau dacă obiectul nu are tag-ul "Interact"
            CircleFull_Gameobject.SetActive(false);
        }
    }
}
