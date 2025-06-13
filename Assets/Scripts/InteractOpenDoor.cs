using UnityEngine;

/// <summary>
/// Po naciœniêciu E na wycelowany obiekt otwiera wskazane drzwi typu SlidingDoor.
/// Mo¿na przypisaæ jedne lub wiele drzwi i zaznaczyæ, czy akcja ma zadzia³aæ tylko raz.
/// </summary>
public class InteractOpenDoor : MonoBehaviour
{
    [Header("Co ma siê otworzyæ")]
    public SlidingDoor[] targetDoors;

    [Header("Ustawienia interakcji")]
    [Tooltip("Maksymalna odleg³oœæ, z której gracz mo¿e aktywowaæ obiekt")]
    public float interactDistance = 3f;
    [Tooltip("Jeœli true, drugi raz nie zadzia³a")]
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
