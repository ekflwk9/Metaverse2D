using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UiButton : MonoBehaviour,
IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected GameObject touchImage;

    protected virtual void Awake()
    {
        touchImage = Service.FindChild(this.transform, "Touch").gameObject;
        if (touchImage == null) Service.Log($"{this.name}에 Tocuh오브젝트가 없음");
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
