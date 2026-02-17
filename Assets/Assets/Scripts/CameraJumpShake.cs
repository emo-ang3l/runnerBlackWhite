using UnityEngine;

public class CameraJumpShake : MonoBehaviour
{
    [Header("Настройки дёрганья")]
    public float shakeDuration = 0.25f;     // сколько секунд трясёт
    public float shakeStrength = 0.12f;     // насколько сильно (0.08–0.2 обычно норм)
    public float decaySpeed = 8f;           // как быстро затухает

    private Vector3 originalPos;
    private float shakeTimeRemaining = 0f;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float strength = shakeStrength * (shakeTimeRemaining / shakeDuration);

            // случайное смещение по x и y
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            if (shakeTimeRemaining <= 0)
            {
                transform.localPosition = originalPos; // возвращаем точно на место
            }
        }
    }

    // Вызывается, когда игрок прыгает
    public void TriggerJumpShake()
    {
        shakeTimeRemaining = shakeDuration;
    }
}