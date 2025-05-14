using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour,
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
        this.gameObject.SetActive(false);

        var bossResource = Service.FindResource("Enemy", "Boss");
        var boss = Instantiate(bossResource);

        boss.name = bossResource.name;

        boss.GetComponent<Monster>().SetMonster();
        boss.transform.position = this.transform.position;
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
