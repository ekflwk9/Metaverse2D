using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireWormProjectile : MonoBehaviour
{
    private int damageAmountWhichPlayerWillGetFromThisFireWormMonsterByProjectileOfFireBallMadeByFireWorm;
    private string targetNameIsPlayerNameWhichWhoYouArePlayingCharacterWithWhiteHeadHeIsUnStopableCharacterWithMostPowerfulAttackPower;
    private Rigidbody2D _rigid;

    public void SetBullet(string _targetName, int _dmg)
    {
        _rigid = GetComponent<Rigidbody2D>();
        targetNameIsPlayerNameWhichWhoYouArePlayingCharacterWithWhiteHeadHeIsUnStopableCharacterWithMostPowerfulAttackPower = _targetName;
        damageAmountWhichPlayerWillGetFromThisFireWormMonsterByProjectileOfFireBallMadeByFireWorm = _dmg;

        this.gameObject.SetActive(false);
    }

    public void Fire(Vector3 _thisPos, Vector3 _target)
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        this.transform.position = _thisPos;
        var direction = _target - _thisPos;
        _rigid.velocity = direction.normalized * 4f;
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
        if (collision.gameObject.CompareTag(targetNameIsPlayerNameWhichWhoYouArePlayingCharacterWithWhiteHeadHeIsUnStopableCharacterWithMostPowerfulAttackPower))
        {
            GameManager.gameEvent.Hit(collision.gameObject.name, damageAmountWhichPlayerWillGetFromThisFireWormMonsterByProjectileOfFireBallMadeByFireWorm);
            this.gameObject.SetActive(false);
        }
    }
}
