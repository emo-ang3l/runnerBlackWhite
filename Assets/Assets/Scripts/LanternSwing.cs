using UnityEngine;

public class LanternSwing : MonoBehaviour
{
    [Header("Настройки качания")]
    public float swingSpeed = 1.5f;     // Скорость качания (1-3 — нормально)
    public float swingAmount = 15f;     // Амплитуда (градусы: 10-20 — реалистично)
    public float startDelay = 0f;       // Задержка старта (опционально)

    private float startTime;

    void Start()
    {
        startTime = Time.time + startDelay;
    }

    void Update()
    {
        if (Time.time < startTime) return;

        // Плавное качание: Sin волна по rotation Z
        float swing = Mathf.Sin((Time.time - startTime) * swingSpeed) * swingAmount;
        transform.localRotation = Quaternion.Euler(0f, 0f, swing);
    }
}