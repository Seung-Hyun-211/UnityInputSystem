using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Pointer Click - {transform.name}, {eventData.button}");
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer Enter - {transform.name}");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer Exit - {transform.name}");
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"Drag - {transform.name}");
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Begin Drag - {transform.name}");
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"Drag End - {transform.name}");
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"Drop - {transform.name}");
    }

}
