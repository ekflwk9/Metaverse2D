using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : MonoBehaviour,
IItemEnter
{
    private GameObject itemInfo;

    private void Awake()
    {
        itemInfo = Service.FindChild(this.transform, "ItemInfo").gameObject;
        GameManager.SetComponent(this);
    }

    public void OnItem()
    {
        GameManager.player.OnPickUpAction();
        Service.Log("æ∆¿Ã≈€ »πµÊ");
        //GameManager.gameEvent.Call("CardUi");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) itemInfo.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) itemInfo.SetActive(false); 
    }
}
