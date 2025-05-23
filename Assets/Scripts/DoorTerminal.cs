using UnityEngine;

public class DoorTerminal : MonoBehaviour
{
    public SlidingDoor door;
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("E - Terminal klikniêty");
                    door.OpenDoor();
                }
            }
        }
    }
}
