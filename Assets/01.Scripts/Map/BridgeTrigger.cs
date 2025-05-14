using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public string direction; // �̵� ���� ("Left", "Right", "Up", "Down" �� �ϳ�)
    private MapManager mapManager; // ���� �� ������ ������ �ִ� MapManager ����

    private void Awake()
    {
        // MapManager�� ������ ã�Ƽ� ����
        mapManager = FindObjectOfType<MapManager>();

        // GameManager���� �� ������Ʈ�� ����� �����ϴ� ��츦 ���� ó��
        GameManager.SetComponent(this);

    }
    private void Start()
    {
        // BoxCollider2D ��������
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

        // isTrigger�� false�� ��� true�� ����
        if (collider != null && !collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }

    // �÷��̾ �긮�� Ʈ���ſ� ����� �� �����
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Ʈ���� ������");

        // ���� ���ڿ��� ���� ��ǥ ������ ����
        Vector2Int offset = Vector2Int.zero;
        switch (direction)
        {
            case "Left":
                offset = Vector2Int.left;
                break;
            case "Right":
                offset = Vector2Int.right;
                break;
            case "Up":
                offset = Vector2Int.up;
                break;
            case "Down":
                offset = Vector2Int.down;
                break;
        }

        // ���� �̵��� ���� ��ǥ ���
        Vector2Int newRoomPos = mapManager.currentRoomPos + offset;

        // �� ��踦 ������� Ȯ��
        int width = mapManager.grid.GetLength(0);
        int height = mapManager.grid.GetLength(1);
        if (newRoomPos.x >= 0 && newRoomPos.x < width && newRoomPos.y >= 0 && newRoomPos.y < height)
        {
            // �ش� ��ǥ�� ���� �����ϴ��� Ȯ��
            Room nextRoom = mapManager.grid[newRoomPos.x, newRoomPos.y];
            if (nextRoom != null && nextRoom.RoomObject != null)
            {
                // �÷��̾ �� ���� ��ġ�� �̵�
                GameManager.player.transform.position = nextRoom.RoomObject.transform.position;

                // ���� �� ��ġ�� �� ��ġ�� ����
                mapManager.currentRoomPos = newRoomPos;

                // ����� �α� ���
                Service.Log($"�� �̵� �� {newRoomPos} ({nextRoom.Type})");
            }
            else
            {
                // �̵��� ���� �������� ���� ��
                Service.Log("�̵��� ���� �����ϴ�.");
            }
        }
    }
}
