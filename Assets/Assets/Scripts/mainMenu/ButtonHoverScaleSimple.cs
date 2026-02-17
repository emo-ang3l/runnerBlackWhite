using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ButtonHoverScaleSimple : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] [Range(1f, 2f)] private float targetScale = 1.15f;
    [SerializeField] private float speed = 8f;

    private Vector3 startScale;
    private Vector3 targetScaleValue;

    void Awake()
    {
        startScale = transform.localScale;
        targetScaleValue = startScale;
        Debug.Log($"Скрипт готов на {gameObject.name}. Начальный масштаб: {startScale}", this);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScaleValue, Time.unscaledDeltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("🖱️ NAVOD! Масштаб изменится!", this);
        targetScaleValue = startScale * targetScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("🖱️ УХОДИТ! Возврат масштаба", this);
        targetScaleValue = startScale;
    }
}