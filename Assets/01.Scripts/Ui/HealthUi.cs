using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUi : MonoBehaviour
{
    public Vector3 healthScale;
    public Transform healthBarTrans;

    private void Awake()
    {
        // transform 변수 선언하고 헬스바 오브젝트 찾아서 지정하기 transform.Find
        healthBarTrans = transform.Find("Health");
        // Vector3 변수 선언해서 트랜스폼변수이름.localScale로 지정하기
        healthScale = healthBarTrans.localScale;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            healthScale.x = 1f;
            healthBarTrans.localScale = healthScale;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            healthScale.x = -1f;
            healthBarTrans.localScale = healthScale;
        }
    }
}
