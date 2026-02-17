using UnityEngine;

public class GameManager : MonoBehaviour
{
    // вместо одного objects — массив разных префабов
    public GameObject[] sectionPrefabs;   // ← сюда перетащи 2–5 разных секций в инспекторе

    void Start()
    {
        if (sectionPrefabs == null || sectionPrefabs.Length == 0)
        {
            Debug.LogError("Не назначены префабы в sectionPrefabs!");
            return;
        }

        InvokeRepeating("SpawnObjects", 1f, 4.2f);
    }

    void SpawnObjects()
    {
        // выбираем случайный префаб из массива
        int randomIndex = Random.Range(0, sectionPrefabs.Length);
        GameObject chosen = sectionPrefabs[randomIndex];

        // спавним в той же позиции, что и раньше
        Instantiate(chosen, new Vector3(30f, 0f, 0f), Quaternion.identity);
    }
}