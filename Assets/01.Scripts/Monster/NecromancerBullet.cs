using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class NecromancerBullet : MonoBehaviour
{
    private int dmg;
    private string targetName;
    private Rigidbody2D rigid;

    /// <summary>
    /// 히트 처리할 타겟의 오브젝트 이름
    /// </summary>
    /// <param name="_targetName"></param>
    public void SetBullet(string _targetName, int _dmg)
    {
        rigid = GetComponent<Rigidbody2D>();
        targetName = _targetName;
        dmg = _dmg;

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// _target에 총알을 발사함
    /// </summary>
    /// <param name="_thisPos"></param>
    /// <param name="_target"></param>
    public void Fire(Vector3 _thisPos, Vector3 _target)
    {
        if (!this.gameObject.activeSelf) this.gameObject.SetActive(true);
        this.transform.position = _thisPos;

        var direction = _target - _thisPos;
        rigid.velocity = direction.normalized * 6f;
    }

    public void SetRotate(Vector3 _thisPos, Vector3 _target)
    {
        var direction = _target - _thisPos;
        var atan = math.atan2(direction.y, direction.x);
        var deg = math.degrees(atan);

        this.transform.rotation = Quaternion.Euler(0, 0, deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetName))
        {
            GameManager.gameEvent.Hit(collision.gameObject.name, dmg);
            this.gameObject.SetActive(false);
        }
    }

}
