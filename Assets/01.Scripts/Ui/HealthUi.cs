using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUi : MonoBehaviour
{
    public Vector3 healthScale;
    public Transform healthBarTrans;

    private void Awake()
    {
        // transform ���� �����ϰ� �ｺ�� ������Ʈ ã�Ƽ� �����ϱ� transform.Find
        healthBarTrans = transform.Find("Health");
        // Vector3 ���� �����ؼ� Ʈ�����������̸�.localScale�� �����ϱ�
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
