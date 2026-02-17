using UnityEngine;

public class EnemyRandomEnabler : MonoBehaviour
{
    void Awake()   // или Start — без разницы
    {
        // Находим ВСЕ объекты с тегом Enemy внутри этой сцены/префаба
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            // 50% шанс выключить каждый враг
            enemy.SetActive(Random.value <= 0.79f);
            // ↑ true = включён, false = выключен
        }
    }
}