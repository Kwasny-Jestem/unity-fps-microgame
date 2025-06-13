using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Po naci�ni�ciu E na wycelowany obiekt instancjuje wskazane prefabry w wybranych punktach.
/// Obs�uguje wiele prefab�w (r�ni wrogowie) i wiele punkt�w spawn.
/// </summary>
public class InteractSpawnEnemies : MonoBehaviour
{
    [Header("Co spawnujemy")]
    [Tooltip("Prefab(y) wrog�w (np. Zombie, Robot, Boss). Ka�dy mo�e by� inny.")]
    public GameObject[] enemyPrefabs;

    [Header("Gdzie spawnujemy")]
    [Tooltip("Punkty w scenie (najlepiej puste GameObject-y)")]
    public Transform[] spawnPoints;

    [Header("Ustawienia logiki")]
    [Tooltip("Je�li true, drugi raz nie zadzia�a")]
    public bool oneShot = true;
    [Tooltip("Losuj prefab dla ka�dego punktu (true) czy u�yj pierwszego z listy (false)?")]
    public bool randomizePrefabPerPoint = true;
    [Tooltip("Czy teleportowa� istniej�cego wroga, je�li ju� stoi w punkcie?")]
    public bool ignoreOccupiedPoints = true;

    [Header("Interakcja")]
    public float interactDistance = 3f;

    bool m_Used;

    void Update()
    {
        if (m_Used && oneShot) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance) && hit.transform == transform)
            {
                SpawnEnemies();
                m_Used = true;
            }
        }
    }

    void SpawnEnemies()
    {
        if (spawnPoints.Length == 0 || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning($"{name}: brak przypisanych spawnPoints lub enemyPrefabs.");
            return;
        }

        List<Transform> occupied = new List<Transform>();

        foreach (Transform point in spawnPoints)
        {
            if (ignoreOccupiedPoints)
            {
                Collider[] hits = Physics.OverlapSphere(point.position, 0.5f);
                if (hits.Length > 0)
                {
                    occupied.Add(point);
                    continue;
                }
            }

            GameObject prefab = enemyPrefabs[randomizePrefabPerPoint
                                    ? Random.Range(0, enemyPrefabs.Length)
                                    : 0];

            Instantiate(prefab, point.position, point.rotation);
        }

        Debug.Log($"InteractSpawnEnemies: zrespawnowano {(spawnPoints.Length - occupied.Count)} wrog�w.");
    }
}
