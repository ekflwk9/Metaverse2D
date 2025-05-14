using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public string direction; // �̵� ���� ("Left", "Right", "Up", "Down" �� �ϳ�)
    private MapManager mapManager; // ���� �� ������ ������ �ִ� MapManager ����
    private Room nextRoom;
    private int spawnedMonsterCount = 0;

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

    public int SpawnMonster(Transform _mapPos, string[] _monsterNameArray, int _maxSpawnCount)
    {
        if (_maxSpawnCount < 3) Service.Log($"{_maxSpawnCount} ���� �ִ� ��ġ�� 3���� ������ ���� �����ϴ�.");

        //������ ���� ����
        GameObject[] monster = new GameObject[_monsterNameArray.Length];

        for (int i = 0; i < _monsterNameArray.Length; i++)
        {
            monster[i] = Service.FindResource("Enemy", _monsterNameArray[i]);
        }

        //���� �迭��ŭ ���� ���� �� ����
        var ranSpawnCount = Random.Range(3, _maxSpawnCount);
        var ranMonsterType = Random.Range(0, _monsterNameArray.Length);
        var isRandom = ranMonsterType == 0 ? false : true;

        for (int i = 0; i < ranSpawnCount; i++)
        {
            //���� Ÿ�� ����
            if (isRandom) ranMonsterType = Random.Range(0, _monsterNameArray.Length);
            else ranMonsterType = 0;

            var monsterPos = _mapPos.transform.position;
            monsterPos.x = Random.Range(0, 5);
            monsterPos.y = Random.Range(0, 5);

            var spawnMonster = MonoBehaviour.Instantiate(monster[ranMonsterType]);

            GameManager.map.spawnCount++;
            spawnMonster.name = $"{monster[ranMonsterType].name}_{GameManager.map.spawnCount}";
            spawnMonster.GetComponent<Monster>().SetMonster();

            spawnMonster.transform.position = _mapPos.transform.position + monsterPos;
        }

        return ranSpawnCount;
    }

    private void FadeFunc()
    {
        // ���� ������ Ȯ��
        if (mapManager.battleRoomName.Contains(nextRoom.RoomObject.name))
        {
            // ���� ���� �´� ���� �ε����� �������� ����
            var ranMonster = Random.Range(mapManager.currentFloor - 1, mapManager.currentFloor);

            // ���� �� ���
            int enemyCount = SpawnMonster
                (nextRoom.RoomObject.transform,
                mapManager.monsterName[ranMonster],
                mapManager.currentFloor * 5);

            // ���� ī��Ʈ ����
            spawnedMonsterCount++;

            GameManager.map.currentRoom = nextRoom;

            //Service.Log($"[Room] ���� �濡 ������ ���� ��: {enemyCount}");
            // Room Ŭ������ ���� �� ���
            GameManager.map.SetEnemies(nextRoom, enemyCount);

            // ���� �� ����Ʈ���� ���� (�ߺ� ���� ����)
            mapManager.battleRoomName.Remove(nextRoom.RoomObject.name);
        }

        // �÷��̾� ��ġ �̵�
        GameManager.player.transform.position = nextRoom.RoomObject.transform.position;

        // ���̵� ȿ�� ����
        GameManager.fade.OnFade();
    }


    // �÷��̾ �긮�� Ʈ���ſ� ����� �� �����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �÷��̾ �ƴϸ� ����
        if (!collision.CompareTag("Player")) return;

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
            nextRoom = mapManager.grid[newRoomPos.x, newRoomPos.y];
            if (nextRoom != null && nextRoom.RoomObject != null)
            {
                // �÷��̾ �� ���� ��ġ�� �̵�
                //GameManager.player.transform.position = nextRoom.RoomObject.transform.position;
                GameManager.fade.OnFade(FadeFunc);

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
