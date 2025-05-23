using UnityEngine;

public class AccessCard : MonoBehaviour
{
    public string cardID = "Red"; // przypisujesz w Inspectorze

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coœ wesz³o w trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddCard(cardID);
                Debug.Log("Zebrano kartê: " + cardID);
                gameObject.SetActive(false);
            }
        }
    }
}
