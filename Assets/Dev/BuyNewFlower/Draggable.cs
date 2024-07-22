using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // полупрозрачный при перетаскивании
        canvasGroup.blocksRaycasts = false; // чтобы позволить бросить объект
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // позиция мыши
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // вернуть непрозрачность
        canvasGroup.blocksRaycasts = true; // блокировать лучи снова

        // Вернуть в исходное положение, если не перетащили в зону
        if (!IsInDropZone())
        {
            transform.position = originalPosition;
        }
    }

    private bool IsInDropZone()
    {
        // Проверка, находится ли объект в зоне
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        var isCollider = hit.collider != null;
        var tagDropZone = hit.collider.CompareTag("DropZone");
        
        Debug.LogError($"collider  {isCollider} tag drop zone {tagDropZone}, name {hit.collider.gameObject.name} ");
        if (hit.collider != null && hit.collider.CompareTag("DropZone"))
        {
            // Проверить пересечение с другими объектами в зоне
            foreach (Transform child in hit.collider.transform)
            {
                if (child != transform && IsOverlapping(child))
                {
                    return false;
                }
            }
            // Прикрепить объект к зоне
            transform.SetParent(hit.collider.transform);
            return true;
        }
        return false;
    }

    private bool IsOverlapping(Transform other)
    {
        RectTransform rect1 = GetComponent<RectTransform>();
        RectTransform rect2 = other.GetComponent<RectTransform>();
        
        var overlap = RectTransformExtensions.Overlaps(rect1, rect2);
        return overlap;
    }
}