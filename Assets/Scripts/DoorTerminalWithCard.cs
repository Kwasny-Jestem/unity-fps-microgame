using UnityEngine;

public class DoorTerminalWithCard : MonoBehaviour
{
    public SlidingDoor targetDoor;
    public float checkDistance = 3f;
    public string requiredCardID = "Red"; // wpisz w Inspectorze

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, checkDistance))
            {
                if (hit.transform == transform)
                {
                    PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
                    if (inventory.HasCard(requiredCardID))
                    {
                        Debug.Log("Otwieram drzwi z kart¹: " + requiredCardID);
                        targetDoor.OpenDoor();
                    }
                    else
                    {
                        Debug.Log("Brak odpowiedniej karty (" + requiredCardID + ").");
                    }
                }
            }
        }
    }
}
