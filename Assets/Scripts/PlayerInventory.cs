using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public HashSet<string> accessCards = new HashSet<string>();

    public bool HasCard(string cardID)
    {
        return accessCards.Contains(cardID);
    }

    public void AddCard(string cardID)
    {
        accessCards.Add(cardID);
        Debug.Log("Zebrano kartê: " + cardID);
    }
}
