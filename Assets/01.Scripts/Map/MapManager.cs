using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject player;


    private void Start()
    {
        GenerateMap();
    }


    // ���� �� (1������ ����)
    public int currentFloor = 1;

    // 1�� = 5x3, 2�� = 5x4, 3�� = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject StartRoom;
    public GameObject BattleRoom;
    public GameObject TreasureRoom;
    public GameObject BossRoom;


    private Room[,] grid; // ���� 2D �迭
    private List<Room> rooms = new List<Room>(); // ���� ������ �� ����Ʈ
    private System.Random rng = new System.Random(); // ���� ����


    public void GenerateMap()
    {
        //���� ���� Width�� Height�� ��ġ ����
        int width = floorWidth[currentFloor - 1];
        int height = floorHeight[currentFloor - 1];

        grid = new Room[width, height];
        rooms.Clear();

        // 1. StartRoom ��ġ
        Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        PlaceRoom(startPos, RoomType.Start);

        RandomRooms(RoomType.Boss, 1, width, height);

        // 3. �������� ������ �� �����ϰ� ��ġ 1���� ������ 7~10��, ������ 1~3��
        int battleRooms = rng.Next(7, 11);
        int treasureRooms = rng.Next(1, 4);

        RandomRooms(RoomType.Battle, battleRooms, width, height);
        RandomRooms(RoomType.Treasure, treasureRooms, width, height);

        // 6. ���������� �� ���� 
        SpawnRooms();

        // 7. �÷��̾� StartRoom�� ����
        SpawnPlayer(startRoom);
    }
    void PlaceRoom(Vector2Int pos, RoomType type)
    {
        Room room = new Room { Position = pos, Type = type };
        grid[pos.x, pos.y] = room; // �迭�� ���
        rooms.Add(room); // ����Ʈ�� �߰�
    }

    void RandomRooms(RoomType type, int count, int width, int height)
    {
        int placed = 0;
        while (placed < count)
        {
            Room randomRoom = rooms[rng.Next(rooms.Count)]; // ���� �� �� ���� ����

            // ���� ���� ����
            foreach (Vector2Int dir in GetShuffledDirections())
            {
                Vector2Int nextPos = randomRoom.Position + dir;

                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null) // �� ���� ���̸鼭 ���� �� ĭ�̸� ��ġ
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // �� ĭ�� ��ġ�ϰ� ���� �ݺ�����
                }
            }
        }
    }

    List<Vector2Int> GetShuffledDirections()
    {
        List<Vector2Int> dirs = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        for (int i = 0; i < dirs.Count; i++)
        {
            int j = rng.Next(i, dirs.Count);
            Vector2Int temp = dirs[i];
            dirs[i] = dirs[j];
            dirs[j] = temp;
        }
        return dirs;
    }

    bool IsInBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    GameObject GetPrefab(RoomType type)
    {
        switch (type)
        {
            case RoomType.Start: return StartRoom;
            case RoomType.Battle: return BattleRoom;
            case RoomType.Treasure: return TreasureRoom;
            case RoomType.Boss: return BossRoom;
            default: return null;
        }
    }

    // ���������� �� ����
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Room room = grid[x, y];
                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 20, y * 20, 0); // 2�� �������� ��ġ
                    GameObject prefab = GetPrefab(room.Type);
                    GameObject roomObj = Instantiate(prefab, worldPos, Quaternion.identity, transform);

                    // �����¿� üũ �� �� On/Off
                    SetDoors(roomObj, x, y);
                }
            }
        }
    }

    // �÷��̾ StartRoom �߾ӿ� �����ϴ� �Լ�
    public void SpawnPlayer(GameObject startRoom)
    {
        GameObject go = GameObject.FindWithTag("StartRoom");

        // StartRoom�� �ױ׸� ���� �÷��̾��� ��ġ ����
        Vector3 centerPos = go.transform.position + new Vector3(2.8f, 3.9f, 0f);

        //GameManager.player.transform.position = centerPos;
        Instantiate(player, centerPos, Quaternion.identity);
    }

    void SetDoors(GameObject roomObj, int x, int y)
    {
        // �� ���⿡ ������ ���� �ִ��� �˻�
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

        // �� �ȿ��� �� ������ Door ������Ʈ ã��
        Transform doorUp = roomObj.transform.Find("Door_U");
        Transform doorDown = roomObj.transform.Find("Door_D");
        Transform doorLeft = roomObj.transform.Find("Door_L");
        Transform doorRight = roomObj.transform.Find("Door_R");

        // �� �� ������Ʈ�� ���� �� ���ο� ���� On/Off
        if (doorUp != null) doorUp.gameObject.SetActive(hasUp);
        if (doorDown != null) doorDown.gameObject.SetActive(hasDown);
        if (doorLeft != null) doorLeft.gameObject.SetActive(hasLeft);
        if (doorRight != null) doorRight.gameObject.SetActive(hasRight);
    }
}
