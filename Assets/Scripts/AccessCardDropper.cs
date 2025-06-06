using Unity.FPS.Game;          // Health
using UnityEngine;

namespace Unity.FPS.AI
{
    /// <summary>
    /// Zrzuca kart� dost�pu, gdy przypisany przeciwnik umrze.
    /// Nie wymaga eventu w klasie Health � sam sprawdza stan �ycia.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class AccessCardDropper : MonoBehaviour
    {
        [Header("Ustaw w Inspectorze")]
        [Tooltip("Prefab karty (musi mie� komponent AccessCard)")]
        public GameObject AccessCardPrefab;

        [Tooltip("ID karty, kt�re ma trafi� do PlayerInventory")]
        public string CardID = "Red";

        [Tooltip("Przesuni�cie miejsca zrzutu wzgl�dem pozycji wroga")]
        public Vector3 DropOffset = new Vector3(0, 0.5f, 0);

        Health m_Health;
        bool m_Dropped;   // �eby karta nie wypada�a kilka razy

        void Awake()
        {
            m_Health = GetComponent<Health>();
        }

        void Update()
        {
            // czy przeciwnik w�a�nie umar�?
            if (!m_Dropped && m_Health.CurrentHealth <= 0f)
            {
                SpawnCard();
                m_Dropped = true;
            }
        }

        void OnDestroy()
        {
            // na wypadek, gdyby obiekt zosta� zniszczony w tej samej klatce,
            // a Update() nie zd��y� si� wykona�
            if (!m_Dropped && m_Health.CurrentHealth <= 0f)
            {
                SpawnCard();
            }
        }

        void SpawnCard()
        {
            if (!AccessCardPrefab)
            {
                Debug.LogWarning($"Brak przypisanego prefabu karty na obiekcie {name}");
                return;
            }

            // 1) tworzymy kart�
            GameObject cardGO = Instantiate(
                AccessCardPrefab,
                transform.position + DropOffset,
                Quaternion.identity);

            // 2) ustawiamy odpowiednie ID
            AccessCard card = cardGO.GetComponent<AccessCard>();
            if (card)
                card.cardID = CardID;
            else
                Debug.LogWarning("Prefab karty nie ma komponentu AccessCard!");
        }
    }
}
