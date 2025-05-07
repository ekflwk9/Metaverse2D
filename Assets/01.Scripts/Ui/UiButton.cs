using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UiButton : MonoBehaviour,
IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected GameObject touchImage;
    protected TMP_Text button;

    protected virtual void Awake()
    {
        touchImage = Service.FindChild(this.transform, "Touch").gameObject;
        if (touchImage == null) Debug.Log($"{this.name}�� Tocuh������Ʈ�� ����");

        button = Service.FindChild(this.transform, "Text").GetComponent<TMP_Text>();
        if (button == null) Debug.Log($"{this.name}�� Text������Ʈ�� ����");
    }

    protected abstract void Click();

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.fade.onFade) Click();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.fade.onFade) touchImage.SetActive(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (touchImage.activeSelf) touchImage.SetActive(false);
    }
}
