using Unity.VisualScripting;
using UnityEngine;

public abstract class UserInterface : MonoBehaviour
{
    [SerializeField]
    UIType type;
    [SerializeField]
    Canvas canvas;
    protected virtual void Awake()
    {
        if (!UIController.Instance.Subscribe(type, this))
        {
#if UNITY_EDITOR
            Debug.LogWarning("이미 동일한 타입의 UI가 등록되어 있음");
#endif
            Destroy(this.gameObject);
        }

        if (type == UIType.NotSelected)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"UI 타입 설정 안됨 - {transform.name}");
        }
#endif
        Toggle(false);
    }

    public void Toggle(bool open)
    {
        canvas.enabled = open;
    }
}
