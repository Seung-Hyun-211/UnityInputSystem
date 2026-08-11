using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour, ISlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private int _slotNum;
    public int SlotNum => _slotNum;

    public event Action<int> OnClickSlot;
    void ISlot.SetSlot(int num)
    {
        _slotNum = num;
    }
    void ISlot.SetImage(Texture2D texture)
    {
        var rectTransform = GetComponent<RectTransform>();
        image.sprite = Sprite.Create(texture, rectTransform.rect, rectTransform.pivot);
    }
    void ISlot.SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void VisibleSetting(bool isVisible)
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickSlot?.Invoke(_slotNum);
        Debug.Log($"Pointer Click - {_slotNum}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer Enter - {_slotNum}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer Exit - {_slotNum}");
    }
}
