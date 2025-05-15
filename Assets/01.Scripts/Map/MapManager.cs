using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Dictionary<Room, int> roomEnemyCount = new Dictionary<Room, int>();
    private HashSet<Room> clearedRooms = new HashSet<Room>();

    public HashSet<string> battleRoomName = new HashSet<string>();
    public string[][] monsterName =
    {
        new string[] { "BringerOfDeath", "FireWorm"},
        new string[] { "Necromancer", "Shaman"},
        new string[] { "BringerOfDeath", "FireWorm", "Necromancer", "Shaman"},
    };

    public string decorationTag = "Decoration"; // ���ڷ��̼� �±� ����

    public int spawnCount;
    public int currentFloor = 1; // ���� �� ���� (1���� ����)

    // ���� ���� ���� �� ũ�� ����
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    // ���� ������ ��Ʈ
    [Header("1�� ������")]
    public GameObject StartRoom_1;
    public GameObject BattleRoom_1;
    public GameObject TreasureRoom_1;
    public GameObject BossRoom_1;

    [Header("2�� ������")]
    public GameObject StartRoom_2;
    public GameObject BattleRoom_2;
    public GameObject TreasureRoom_2;
    public GameObject BossRoom_2;

    [Header("3�� ������")]
    public GameObject StartRoom_3;
    public GameObject BattleRoom_3;
    public GameObject TreasureRoom_3;
    public GameObject BossRoom_3;

    public Room[,] grid; // 2D �迭�� �� ��ġ
    private List<Room> rooms = new List<Room>(); // ������ �� ���
    private System.Random rng = new System.Random(); // ���� ������

    private GameObject playerInstance; // ���� ���� �����ϴ� �÷��̾�
    public Vector2Int currentRoomPos; // ���� �� ��ġ
    public string bridgeDirection; // �ܺο��� ������ �� ����

    public int RemainingEnemies;
    public bool IsCleared = false; // ������ Ŭ���� ���θ� �����ϴ� �÷��� �߰�
    public Room currentRoom;


    private void Awake()
    {
        GameManager.gameEvent.Add(ManualClear);
        GameManager.SetComponent(this);

        GenerateMap(); // ���� ���� �� �� ����

        GameManager.gameEvent.Call("CardWindowOn");
    }

    // ������ ���� �ӽ��ڵ�
    //void Update()
    //{
    //    // ����: Ư�� Ű�� ������ GoToNextFloor ȣ�� �׽�Ʈ
    //    if (Input.GetKeyDown(KeyCode.N))  // N Ű�� ������ ��
    //    {
    //        GoToNextFloor();  // 1�� �� 2�� �� 3�� ���������� ����

    //        GameManager.gameEvent.Call("CardWindowOn");
    //    }

    //    // ����: Ư�� Ű�� ������ OpenBridge(GameObject roomObj) ȣ�� �׽�Ʈ
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        currentRoom = grid[currentRoomPos.x, currentRoomPos.y];
    //        if (currentRoom != null && currentRoom.RoomObject != null)
    //        {
    //            OpenBridge(currentRoom.RoomObject, currentRoom.Position.x, currentRoom.Position.y);
    //        }
    //    }
    //}

    // �� ���� ��ü ����
    public void GenerateMap()
    {
        int width = floorWidth[currentFloor - 1];   // ���� ���� �´� �ʺ�
        int height = floorHeight[currentFloor - 1]; // ���� ���� �´� ����

        grid = new Room[width, height];
        rooms.Clear();

        // ������ ��ġ�� ���� �� ����
        Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        PlaceRoom(startPos, RoomType.Start);
        currentRoomPos = startPos; // ���� �� ��ġ �ʱ�ȭ

        // ���� �� �� ����
        int battleCount = 0;
        int treasureCount = 0;

        if (currentFloor == 1)
        {
            battleCount = rng.Next(7, 11);
            treasureCount = rng.Next(1, 3);
        }
        else if (currentFloor == 2)
        {
            battleCount = rng.Next(7, 14);
            treasureCount = rng.Next(1, 3);
        }
        else if (currentFloor == 3)
        {
            battleCount = rng.Next(9, 17);
            treasureCount = rng.Next(1, 3);
        }

        // 1. ������ ���� ����
        RandomRooms(RoomType.Battle, battleCount, width, height);

        //Service.SpawnMonster

        // 2. ������ ���� ����
        RandomRooms(RoomType.Treasure, treasureCount, width, height);

        // 3. ������ 1�� ����
        RandomRooms(RoomType.Boss, 1, width, height);

        // 4. ��� �� ������ ����
        SpawnRooms();

        // 5. �÷��̾ ��ŸƮ�濡 ��ġ
        PlacePlayer();


        // 6. ���� ������Ʈ�� ���� On/Off
        RandomizeDecorations();
    }

    // �� ����(��ǥ, Ÿ��)�� �����ϰ� ���
    void PlaceRoom(Vector2Int pos, RoomType type)
    {
        Room room = new Room { Position = pos, Type = type };
        grid[pos.x, pos.y] = room;
        rooms.Add(room);
    }

    // ������ Ÿ���� ���� �����ϰ� ���ϴ� ������ŭ ��ġ
    void RandomRooms(RoomType type, int count, int width, int height)
    {
        int placed = 0;
        while (placed < count)
        {
            Room randomRoom = rooms[rng.Next(rooms.Count)]; // ���� �� �� �ϳ����� Ȯ��

            foreach (Vector2Int dir in GetShuffledDirections()) // �����¿� ���� ����
            {
                Vector2Int nextPos = randomRoom.Position + dir;
                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null)
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // ���� ������ �Ѿ
                }
            }
        }
    }

    // ������ �����ϰ� ���� ��ȯ
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

    // ��ǥ�� �׸��� ���� �ִ��� Ȯ��
    bool IsInBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    // �� Ÿ�Կ� ���� ���� ���� ������ ��ȯ
    GameObject GetPrefab(RoomType type)
    {
        switch (currentFloor)
        {
            case 1:
                return type switch
                {
                    RoomType.Start => StartRoom_1,
                    RoomType.Battle => BattleRoom_1,
                    RoomType.Treasure => TreasureRoom_1,
                    RoomType.Boss => BossRoom_1,
                    _ => null,
                };
            case 2:
                return type switch
                {
                    RoomType.Start => StartRoom_2,
                    RoomType.Battle => BattleRoom_2,
                    RoomType.Treasure => TreasureRoom_2,
                    RoomType.Boss => BossRoom_2,
                    _ => null,
                };
            case 3:
                return type switch
                {
                    RoomType.Start => StartRoom_3,
                    RoomType.Battle => BattleRoom_3,
                    RoomType.Treasure => TreasureRoom_3,
                    RoomType.Boss => BossRoom_3,
                    _ => null,
                };
            default:
                return null;
        }
    }

    // �������� �ν��Ͻ�ȭ�Ͽ� ���� �� ������Ʈ ����
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Room room = grid[x, y];
                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 20, y * 20, 0);
                    GameObject prefab = GetPrefab(room.Type);
                    GameObject roomObj = Instantiate(prefab, worldPos, Quaternion.identity, transform);
                    roomObj.name = $"{roomObj.name}{x}{y}";

                    if (prefab.name.Contains("BattleRoom"))
                    {
                        battleRoomName.Add(roomObj.name);
                    }

                    room.RoomObject = roomObj; // ������Ʈ ����
                    SetBridge(roomObj, x, y);
                }
            }
        }
    }

    // �� ���� ���� (�����¿� ����� �� Ȯ��)
    void SetBridge(GameObject roomObj, int x, int y)
    {
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

        Transform[] allBridges = roomObj.GetComponentsInChildren<Transform>(true);
        foreach (Transform bridge in allBridges)
        {
            if (!bridge.CompareTag("Bridge")) continue;

            bool hasNeighbor = false;
            if (bridge.name.Contains("Bridge_U")) hasNeighbor = hasUp;
            else if (bridge.name.Contains("Bridge_D")) hasNeighbor = hasDown;
            else if (bridge.name.Contains("Bridge_L")) hasNeighbor = hasLeft;
            else if (bridge.name.Contains("Bridge_R")) hasNeighbor = hasRight;

            bridge.gameObject.SetActive(true); // �ϴ� Bridge�� �׻� Ȱ��ȭ

            if (hasNeighbor)
            {
                // ����� ���
                // �ڽ� Bridge ������Ʈ ���α�
                Transform bridgeVisual = bridge.Find("Bridge");
                if (bridgeVisual != null) bridgeVisual.gameObject.SetActive(false);

                // �� �ѱ�
                Transform wall = bridge.Find("Wall");
                if (wall != null) wall.gameObject.SetActive(true);

                // �ݶ��̴� ����
                Collider2D col = bridge.GetComponent<Collider2D>();
                if (col != null) col.enabled = false;

                // �ڽ� �� Wall �±� ���� ������Ʈ�� ���ش�
                foreach (Transform child in bridge)
                {
                    if (child.CompareTag("Wall"))
                        child.gameObject.SetActive(false);
                }
            }
            else
            {
                // ������� ���� ���
                foreach (Transform child in bridge)
                {
                    if (child.CompareTag("Wall"))
                        child.gameObject.SetActive(true);  // Wall �±� �ڽĸ� ��
                    else
                        child.gameObject.SetActive(false); // �������� ��
                }
            }
        }
    }


    // �ٸ��� ���̰� �ϴ� �ڵ�
    public void OpenBridge(GameObject roomObj, int x, int y)
    {
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

        Transform[] allBridges = roomObj.GetComponentsInChildren<Transform>(true);
        foreach (Transform bridge in allBridges)
        {
            if (!bridge.CompareTag("Bridge")) continue;

            bool hasNeighbor = false;
            if (bridge.name.Contains("Bridge_U")) hasNeighbor = hasUp;
            else if (bridge.name.Contains("Bridge_D")) hasNeighbor = hasDown;
            else if (bridge.name.Contains("Bridge_L")) hasNeighbor = hasLeft;
            else if (bridge.name.Contains("Bridge_R")) hasNeighbor = hasRight;

            if (hasNeighbor)
            {
                // �ڽ� Bridge ������Ʈ �ѱ�
                Transform bridgeVisual = bridge.Find("Bridge");
                if (bridgeVisual != null)
                    bridgeVisual.gameObject.SetActive(true);

                // �ݶ��̴� �ѱ�
                Collider2D col = bridge.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = true;

                // �� ����
                Transform wall = bridge.Find("Wall");
                if (wall != null)
                    wall.gameObject.SetActive(false);

                // Wall �±� �ڽĵ� ���α�
                foreach (Transform child in bridge)
                {
                    if (child.CompareTag("Wall"))
                        child.gameObject.SetActive(false);
                }
            }
            else
            {
                // ������� ���� ���
                foreach (Transform child in bridge)
                {
                    if (child.CompareTag("Wall"))
                        child.gameObject.SetActive(true);  // Wall �±� �ڽĸ� ��
                    else
                        child.gameObject.SetActive(false); // �������� ��
                }
            }
        }
    }


    // ���� ������Ʈ�� ���� On/Off
    public void RandomizeDecorations()
    {
        GameObject[] decorations = GameObject.FindGameObjectsWithTag(decorationTag);
        if (decorations.Length == 0)
        {
            Debug.LogWarning("No decorations found with tag: " + decorationTag);
            return;
        }

        foreach (GameObject deco in decorations)
        {
            bool isActive = Random.value > 0.5f;
            deco.SetActive(isActive);
        }
    }

    // ���������� �̵� �ӽ� �ڵ�
    public void GoToNextFloor()
    {
        if (currentFloor < 3)
        {
            // ���� ���� ��� ��(��) ������Ʈ ����
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            currentFloor++;
            GenerateMap(); // ���ο� �� �� ����

            // rooms ����Ʈ���� ��ŸƮ���� ���� ã��
            foreach (Room room in rooms)
            {
                if (room.Type == RoomType.Start && room.RoomObject != null)
                {
                    Vector3 centerPos = room.RoomObject.transform.position; //+ new Vector3(2.5f, 4.3f, 0f);
                    GameManager.player.transform.position = centerPos;
                    return;
                }
            }
        }
    }

    // �÷��̾� ��ġ
    void PlacePlayer()
    {
        foreach (Room room in rooms)
        {
            if (room.Type == RoomType.Start && room.RoomObject != null)
            {
                Vector3 centerPos = room.RoomObject.transform.position; //+ new Vector3(1f, 2.3f, 0f);
                GameManager.player.transform.position = centerPos;
                return;
            }
        }

        Debug.LogWarning("��ŸƮ���� ã�� �� �����ϴ�.");
    }

    public void ManualClear()
    {
        currentRoom = grid[currentRoomPos.x, currentRoomPos.y];
        if (currentRoom != null && currentRoom.RoomObject != null)
        {
            OpenBridge(currentRoom.RoomObject, currentRoom.Position.x, currentRoom.Position.y);
        }
    }

    public void ClearBattleRoom(Room room)
    {
        currentRoom = grid[currentRoomPos.x, currentRoomPos.y];
        if (currentRoom != null && currentRoom.RoomObject != null)
        {
            OpenBridge(currentRoom.RoomObject, currentRoom.Position.x, currentRoom.Position.y);
        }
        clearedRooms.Add(room); // Ŭ���� ���� ���
    }
    // �� �� ����
    public void SetEnemies(Room room, int count)
    {
        roomEnemyCount[room] = count;
        clearedRooms.Remove(room);
    }

    // �� óġ �� ȣ��
    public void EnemyDefeated()
    {
        if (roomEnemyCount.ContainsKey(GameManager.map.currentRoom))
        {
            roomEnemyCount[currentRoom]--;

            if (roomEnemyCount[currentRoom] <= 0 && !clearedRooms.Contains(currentRoom))
            {
                ClearBattleRoom(currentRoom);
            }
        }
    }

    public void NextRoom()
    {
        GoToNextFloor();  // 1�� �� 2�� �� 3�� ���������� ����
        GameManager.gameEvent.Call("CardWindowOn");
    }
}