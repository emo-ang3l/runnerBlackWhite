using UnityEngine;

public class TreeSway : MonoBehaviour
{
    [Header("Настройки покачивания")]
    [Tooltip("Максимальный угол наклона влево/вправо (в градусах)")]
    [Range(0.5f, 12f)]
    public float maxAngle = 4f;

    [Tooltip("Скорость качания (чем больше — тем быстрее)")]
    [Range(0.3f, 3f)]
    public float speed = 1f;

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * maxAngle;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}