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


    // 현재 층 (1층부터 시작)
    public int currentFloor = 1;

    // 1층 = 5x3, 2층 = 5x4, 3층 = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject StartRoom;
    public GameObject BattleRoom;
    public GameObject TreasureRoom;
    public GameObject BossRoom;


    private Room[,] grid; // 맵의 2D 배열
    private List<Room> rooms = new List<Room>(); // 현재 생성된 방 리스트
    private System.Random rng = new System.Random(); // 랜덤 생성


    public void GenerateMap()
    {
        //층에 따라 Width와 Height의 배치 설정
        int width = floorWidth[currentFloor - 1];
        int height = floorHeight[currentFloor - 1];

        grid = new Room[width, height];
        rooms.Clear();

        // 1. StartRoom 배치
        Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        PlaceRoom(startPos, RoomType.Start);

        RandomRooms(RoomType.Boss, 1, width, height);

        // 3. 랜덤으로 나머지 방 생성하고 배치 1층은 전투방 7~10개, 보물방 1~3개
        int battleRooms = rng.Next(7, 11);
        int treasureRooms = rng.Next(1, 4);

        RandomRooms(RoomType.Battle, battleRooms, width, height);
        RandomRooms(RoomType.Treasure, treasureRooms, width, height);

        // 6. 프리팹으로 방 생성 
        SpawnRooms();

        // 7. 플레이어 StartRoom에 스폰
        SpawnPlayer(startRoom);
    }
    void PlaceRoom(Vector2Int pos, RoomType type)
    {
        Room room = new Room { Position = pos, Type = type };
        grid[pos.x, pos.y] = room; // 배열에 등록
        rooms.Add(room); // 리스트에 추가
    }

    void RandomRooms(RoomType type, int count, int width, int height)
    {
        int placed = 0;
        while (placed < count)
        {
            Room randomRoom = rooms[rng.Next(rooms.Count)]; // 기존 방 중 랜덤 선택

            // 랜덤 방향 섞기
            foreach (Vector2Int dir in GetShuffledDirections())
            {
                Vector2Int nextPos = randomRoom.Position + dir;

                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null) // 맵 범위 안이면서 아직 빈 칸이면 배치
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // 한 칸만 배치하고 다음 반복으로
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

    // 프리팹으로 방 생성
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Room room = grid[x, y];
                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 20, y * 20, 0); // 2배 간격으로 배치
                    GameObject prefab = GetPrefab(room.Type);
                    GameObject roomObj = Instantiate(prefab, worldPos, Quaternion.identity, transform);

                    // 상하좌우 체크 후 문 On/Off
                    SetDoors(roomObj, x, y);
                }
            }
        }
    }

    // 플레이어를 StartRoom 중앙에 생성하는 함수
    public void SpawnPlayer(GameObject startRoom)
    {
        GameObject go = GameObject.FindWithTag("StartRoom");

        // StartRoom의 테그를 따라서 플레이어의 위치 생성
        Vector3 centerPos = go.transform.position + new Vector3(2.8f, 3.9f, 0f);

        //GameManager.player.transform.position = centerPos;
        Instantiate(player, centerPos, Quaternion.identity);
    }

    void SetDoors(GameObject roomObj, int x, int y)
    {
        // 각 방향에 인접한 방이 있는지 검사
        bool hasUp = IsInBounds(new Vector2Int(x, y + 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y + 1] != null;
        bool hasDown = IsInBounds(new Vector2Int(x, y - 1), grid.GetLength(0), grid.GetLength(1)) && grid[x, y - 1] != null;
        bool hasLeft = IsInBounds(new Vector2Int(x - 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x - 1, y] != null;
        bool hasRight = IsInBounds(new Vector2Int(x + 1, y), grid.GetLength(0), grid.GetLength(1)) && grid[x + 1, y] != null;

        // 방 안에서 각 방향의 Door 오브젝트 찾기
        Transform doorUp = roomObj.transform.Find("Door_U");
        Transform doorDown = roomObj.transform.Find("Door_D");
        Transform doorLeft = roomObj.transform.Find("Door_L");
        Transform doorRight = roomObj.transform.Find("Door_R");

        // 각 문 오브젝트를 인접 방 여부에 따라 On/Off
        if (doorUp != null) doorUp.gameObject.SetActive(hasUp);
        if (doorDown != null) doorDown.gameObject.SetActive(hasDown);
        if (doorLeft != null) doorLeft.gameObject.SetActive(hasLeft);
        if (doorRight != null) doorRight.gameObject.SetActive(hasRight);
    }
}
