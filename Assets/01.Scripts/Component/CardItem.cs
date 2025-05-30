using UnityEngine;

public class CardItem : MonoBehaviour,
IItemEnter
{
    private bool isGet;
    private GameObject itemInfo;

    private void Awake()
    {
        itemInfo = Service.FindChild(this.transform, "ItemInfo").gameObject;
        this.gameObject.name = this.transform.parent.name;
        GameManager.SetComponent(this);
    }

    public void OnItem()
    {
        this.gameObject.SetActive(false);
        GameManager.gameEvent.Call("CardWindowOn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.sound.OnEffect("TouchItem");
            itemInfo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) itemInfo.SetActive(false);
    }
}
