using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject player; // 플레이어 프리팹
    public string decorationTag = "Decoration"; // 데코레이션 태그 설정

    public int currentFloor = 1; // 현재 층 정보 (1부터 시작)

    // 현재 층에 따라 맵 크기 지정
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    // 층별 프리팹 세트
    [Header("1층 프리팹")]
    public GameObject StartRoom_1;
    public GameObject BattleRoom_1;
    public GameObject TreasureRoom_1;
    public GameObject BossRoom_1;

    [Header("2층 프리팹")]
    public GameObject StartRoom_2;
    public GameObject BattleRoom_2;
    public GameObject TreasureRoom_2;
    public GameObject BossRoom_2;

    [Header("3층 프리팹")]
    public GameObject StartRoom_3;
    public GameObject BattleRoom_3;
    public GameObject TreasureRoom_3;
    public GameObject BossRoom_3;

    private Room[,] grid; // 2D 배열에 방 배치
    private List<Room> rooms = new List<Room>(); // 생성된 방 목록
    private System.Random rng = new System.Random(); // 난수 생성기

    private GameObject playerInstance; // 실제 씬에 존재하는 플레이어
    private Vector2Int currentRoomPos; // 현재 방 위치
    public string doorDirection; // 외부에서 설정할 문 방향

    private void Start()
    {
        GenerateMap(); // 게임 시작 시 맵 생성
    }

    // 다음층 생성 임시코드
    void Update()
    {
        // 예시: 특정 키를 눌러서 GoToNextFloor 호출 테스트
        if (Input.GetKeyDown(KeyCode.N))  // N 키를 눌렀을 때
        {
            GoToNextFloor();  // 1층 → 2층 → 3층 순차적으로 생성
        }
    }

    // 맵 생성 전체 로직
    public void GenerateMap()
    {
        int width = floorWidth[currentFloor - 1];   // 현재 층에 맞는 너비
        int height = floorHeight[currentFloor - 1]; // 현재 층에 맞는 높이

        grid = new Room[width, height];
        rooms.Clear();

        // 랜덤한 위치에 시작 방 생성
        Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        PlaceRoom(startPos, RoomType.Start);
        currentRoomPos = startPos; // 현재 방 위치 초기화

        // 층별 방 수 설정
        int battleCount = 0;
        int treasureCount = 0;

        if (currentFloor == 1)
        {
            battleCount = rng.Next(7, 11);
            treasureCount = rng.Next(1, 4);
        }
        else if (currentFloor == 2)
        {
            battleCount = rng.Next(7, 14);
            treasureCount = rng.Next(1, 5);
        }
        else if (currentFloor == 3)
        {
            battleCount = rng.Next(9, 17);
            treasureCount = rng.Next(2, 6);
        }

        // 전투방 랜덤 생성
        RandomRooms(RoomType.Battle, battleCount, width, height);

        // 보물방 랜덤 생성
        RandomRooms(RoomType.Treasure, treasureCount, width, height);

        // 보스방 1개 생성
        RandomRooms(RoomType.Boss, 1, width, height);

        // 모든 방 프리팹 스폰
        SpawnRooms();

        // 플레이어 스폰
        SpawnPlayer();

        // 데코 오브젝트들 랜덤 On/Off
        RandomizeDecorations();
    }

    // 방 정보(좌표, 타입)를 생성하고 등록
    void PlaceRoom(Vector2Int pos, RoomType type)
    {
        Room room = new Room { Position = pos, Type = type };
        grid[pos.x, pos.y] = room;
        rooms.Add(room);
    }

    // 지정된 타입의 방을 랜덤하게 원하는 개수만큼 배치
    void RandomRooms(RoomType type, int count, int width, int height)
    {
        int placed = 0;
        while (placed < count)
        {
            Room randomRoom = rooms[rng.Next(rooms.Count)]; // 기존 방 중 하나에서 확장

            foreach (Vector2Int dir in GetShuffledDirections()) // 상하좌우 랜덤 순서
            {
                Vector2Int nextPos = randomRoom.Position + dir;
                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null)
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // 다음 방으로 넘어감
                }
            }
        }
    }

    // 방향을 랜덤하게 섞어 반환
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

    // 좌표가 그리드 내에 있는지 확인
    bool IsInBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    // 방 타입에 따라 현재 층의 프리팹 반환
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

    // 프리팹을 인스턴스화하여 실제 방 오브젝트 생성
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Room room = grid[x, y];
                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 20, y * 20, 0); // 방 간 간격 유지
                    GameObject prefab = GetPrefab(room.Type);
                    GameObject roomObj = Instantiate(prefab, worldPos, Quaternion.identity, transform);

                    // 방 간 연결을 위한 문 설정
                    SetDoors(roomObj, x, y);
                }
            }
        }
    }

    // 플레이어를 시작방 위치에 소환
    public void SpawnPlayer()
    {
        GameObject go = GameObject.FindWithTag("StartRoom");
        if (go == null)
        {
            Debug.LogWarning("StartRoom 태그를 가진 오브젝트를 찾을 수 없습니다.");
            return;
        }

        Vector3 centerPos = go.transform.position + new Vector3(2.5f, 4.3f, 0f); // 위치 보정

        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        playerInstance = Instantiate(player, centerPos, Quaternion.identity);
    }

    // 문 상태 설정 (상하좌우 연결된 방 확인)
    void SetDoors(GameObject roomObj, int x, int y)
    {
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

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

    // 데코 오브젝트들 랜덤 On/Off
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

    // 다음층으로 이동 임시 코드
    public void GoToNextFloor()
    {
        if (currentFloor < 3)
        {
            // 현재 층의 모든 방(맵) 오브젝트 제거
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            currentFloor++;
            GenerateMap(); // 새로운 층 맵 생성

            GameObject go = GameObject.FindWithTag("StartRoom");
            if (go != null)
            {
                Vector3 centerPos = go.transform.position + new Vector3(2.5f, 4.3f, 0f); // 위치 보정
                player.transform.position = centerPos; // 플레이어를 StartRoom 위치로 이동
            }
            else
            {
                Debug.LogWarning("StartRoom 태그를 가진 오브젝트를 찾을 수 없습니다.");
            }
        }
    }
}
