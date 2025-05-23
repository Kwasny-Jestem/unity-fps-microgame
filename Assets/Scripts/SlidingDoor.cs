using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(2f, 0f, 0f);
    public float openSpeed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPos, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }
}
