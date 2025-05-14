using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : MonoBehaviour,
IItemEnter
{
    private bool isGet;
    private GameObject itemInfo;

    private void Awake()
    {
        itemInfo = Service.FindChild(this.transform, "ItemInfo").gameObject;
        GameManager.SetComponent(this);
    }

    public void OnItem()
    {
        this.gameObject.SetActive(false);
        //GameManager.player.OnPickUpAction();
        GameManager.gameEvent.Call("CardWindowOn");
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
