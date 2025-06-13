using UnityEngine;

/// <summary>
/// Po naci�ni�ciu E na wycelowany obiekt otwiera wskazane drzwi typu SlidingDoor.
/// Mo�na przypisa� jedne lub wiele drzwi i zaznaczy�, czy akcja ma zadzia�a� tylko raz.
/// </summary>
public class InteractOpenDoor : MonoBehaviour
{
    [Header("Co ma si� otworzy�")]
    public SlidingDoor[] targetDoors;

    [Header("Ustawienia interakcji")]
    [Tooltip("Maksymalna odleg�o��, z kt�rej gracz mo�e aktywowa� obiekt")]
    public float interactDistance = 3f;
    [Tooltip("Je�li true, drugi raz nie zadzia�a")]
    public bool oneShot = true;

    bool m_Used;

    void Update()
    {
        if (m_Used && oneShot) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance) && hit.transform == transform)
            {
                foreach (var door in targetDoors)
                    door.OpenDoor();

                m_Used = true;
                Debug.Log($"InteractOpenDoor: otworzono {targetDoors.Length} drzwi.");
            }
        }
    }
}
