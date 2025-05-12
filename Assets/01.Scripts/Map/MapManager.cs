using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject player;
    public string decorationTag = "Decoration";

    // ���� �� (1������ ����)
    public int currentFloor = 1;

    private void Start()
    {
        GenerateMap();
    }



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

        // 2. ���� ������, ������ �� ����
        int battleRooms = 0;
        int treasureRooms = 0;

        if (currentFloor == 1)
        {
            battleRooms = rng.Next(7, 11);
            treasureRooms = rng.Next(1, 4);
        }
        else if (currentFloor == 2)
        {
            battleRooms = rng.Next(7, 14);
            treasureRooms = rng.Next(1, 5);
        }
        else if (currentFloor == 3)
        {
            battleRooms = rng.Next(9, 17);
            treasureRooms = rng.Next(2, 6);
        }

        // 3. BattleRoom ��ġ
        RandomRooms(RoomType.Battle, battleRooms, width, height);

        // 4. TreasureRoom ��ġ
        RandomRooms(RoomType.Treasure, treasureRooms, width, height);

        // 5. BossRoom ��ġ
        RandomRooms(RoomType.Boss, 1, width, height);

        // 6. ���������� �� ����  
        SpawnRooms();

        // 7. �÷��̾� StartRoom�� ����
        SpawnPlayer(startRoom);

        // 8. ���ڷ��̼� ����
        RandomizeDecorations();
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
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

        // ��� Door �±� ������Ʈ�� ã�Ƽ� ó��
        Transform[] allDoors = roomObj.GetComponentsInChildren<Transform>(true);
        foreach (Transform door in allDoors)
        {
            if (door.CompareTag("Door"))
            {
                if (door.name.Contains("Door_U")) door.gameObject.SetActive(hasUp);
                else if (door.name.Contains("Door_D")) door.gameObject.SetActive(hasDown);
                else if (door.name.Contains("Door_L")) door.gameObject.SetActive(hasLeft);
                else if (door.name.Contains("Door_R")) door.gameObject.SetActive(hasRight);
            }
        }
    }

    public void RandomizeDecorations()
    {
        // Tag�� "Decoration"�� ��� ������Ʈ�� ã��
        GameObject[] decorations = GameObject.FindGameObjectsWithTag(decorationTag);

        // �迭�� ����ִ��� üũ
        if (decorations.Length == 0)
        {
            Debug.LogWarning("No decorations found with tag: " + decorationTag);
            return;
        }

        // �� ������Ʈ�� ���� �������� on/off ����
        foreach (GameObject deco in decorations)
        {
            bool isActive = Random.value > 0.5f;  // 50% Ȯ���� true / false
            deco.SetActive(isActive);
        }
    }
}
