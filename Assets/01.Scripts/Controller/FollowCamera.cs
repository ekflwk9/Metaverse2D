using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    // ī�޶� �̵��� �� �ִ� ���� �ּ� ��ǥ�� �ִ� ��ǥ (��� ����)
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // ī�޶��� ���� ����, ���� �ʺ� (ȭ�� ũ�� ����, Clamp ����)
    private float halfHeight;
    private float halfWidth;

    // �÷��̾ ó�� �����Ǿ����� ����
    private bool playerInitialized = false;

    // Start�� ���� ���� �� �� ���� ����
    private void Awake()
    {
        // Camera ������Ʈ ��������
        var cam = Service.FindChild(this.transform, "Main Camera").GetComponent<Camera>();

        // orthographicSize�� ȭ�� ���� ���� ���
        halfHeight = cam.orthographicSize;
        // aspect�� ȭ�� ���� �ʺ� ���
        halfWidth = halfHeight * cam.aspect;

        this.gameObject.SetActive(false);
        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update�� �� ������ ���� (�÷��̾� ���� Ȯ�� �� �ʱ�ȭ)
    void Update()
    {

        if (GameManager.player != null)
        {
            // �÷��̾� ��ġ ��ǥ ��������
            float desiredX = GameManager.player.transform.position.x;
            float desiredY = GameManager.player.transform.position.y;

            // ���� ��� ��� (ī�޶� �ܰ��� ����Ͽ� ���)
            float minX = minBounds.x + halfWidth;
            float maxX = maxBounds.x - halfWidth;
            float minY = minBounds.y + halfHeight;
            float maxY = maxBounds.y - halfHeight;

            // �÷��̾ ��� �ȿ� �ִ��� üũ
            bool isPlayerInsideBounds = desiredX >= minBounds.x && desiredX <= maxBounds.x &&
                                        desiredY >= minBounds.y && desiredY <= maxBounds.y;

            if (isPlayerInsideBounds)
            {
                // �ε巴�� ����
                transform.position = Vector3.Lerp(transform.position, GameManager.player.transform.position, 0.05f);
                //transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            }
            else
            {
                // �÷��̾ ��踦 ����� ��
                // ī�޶� �÷��̾� ��ġ�� ��� �̵�
                transform.position = new Vector3(desiredX, desiredY, transform.position.z);

                // ��赵 �÷��̾� ��ġ�� �߽����� ������
                CenterBoundsOn(new Vector2(desiredX, desiredY));

            }
        }
    }

    // CenterBoundsOn: �־��� �߽� ��ǥ�� ��踦 �������ϴ� �Լ�
    private void CenterBoundsOn(Vector2 center)
    {
        // ��� �ʺ�, ���� ���
        float width = maxBounds.x - minBounds.x;
        float height = maxBounds.y - minBounds.y;

        // �߽� ��ǥ �������� minBounds, maxBounds �缳��
        minBounds = new Vector2(center.x - width / 2, center.y - height / 2);
        maxBounds = new Vector2(center.x + width / 2, center.y + height / 2);
    }

    // OnDrawGizmos: �� �信�� ��踦 �ð������� ǥ�� (����׿�)
    void OnDrawGizmos()
    {
        // Gizmos ������ �ʷϻ����� ����
        Gizmos.color = Color.green;

        // ��� �߽� ��ǥ ���
        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) / 2,
            (minBounds.y + maxBounds.y) / 2,
            0
        );

        // ��� ũ�� ���
        Vector3 size = new Vector3(
            (maxBounds.x - minBounds.x),
            (maxBounds.y - minBounds.y),
            0
        );

        // WireCube�� ��� �ڽ� �׸���
        Gizmos.DrawWireCube(center, size);
    }
}
