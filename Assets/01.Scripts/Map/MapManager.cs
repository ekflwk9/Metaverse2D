using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject player;
    // 현재 층 (1층부터 시작)
    public int currentFloor = 1;

    // 1층 = 5x3, 2층 = 5x4, 3층 = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject startRoom;
    public GameObject battleRoom;
    public GameObject treasureRoom;
    public GameObject bossRoom;

    private Room[,] grid; // 맵의 2D 배열
    private List<Room> rooms = new List<Room>(); // 현재 생성된 방 리스트
    private System.Random rng = new System.Random(); // 랜덤 생성

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

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
        RandomRooms(RoomType.Treasure, battleRooms, width, height);


        // 4. 프리팹 배치 
        SpawnRooms();

        // 5. 플레이어 StartRoom에 스폰
        SpawnPlayer(startPos);
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

            // 랜덤 방향 섞어서 시도
            foreach (Vector2Int dir in GetShuffledDirections())
            {
                Vector2Int nextPos = randomRoom.Position + dir; // 새로운 위치를 계산
                
                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null) // 맵 범위 안이면서 아직 빈 칸이면 배치
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // 한 칸만 배치하고 다음 반복으로
                }
            }
        }
    }

    // 주어진 좌표가 맵 내에 있는지 확인하는 함수
    bool IsInBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    // 방향 리스트 랜덤 섞기 (방 배치 방향 무작위화용)
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

    //RoomType에 맞는 GameObject 프리팹을 반환
    GameObject GetPrefab(RoomType type)
    {
        switch (type)
        {
            case RoomType.Start: 
                return startRoom;
            case RoomType.Battle: 
                return battleRoom;
            case RoomType.Treasure: 
                return treasureRoom;
            case RoomType.Boss: 
                return bossRoom;
            default: return null;
        }
    }

    // 프리팹 배치
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; x<= grid.GetLength(1); y++)
            {
                Room room = grid[x, y]; // grid 배열에서 (x, y) 위치에 있는 Room 가져오기

                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 2, y * 2, 0); // 2배 간격으로 배치 (나중에 프리팹 크기에 따라 수정)
                    GameObject prefab = GetPrefab(room.Type);
                }
            }
        }
    }

    // 플레이어를 StartRoom에 스폰
    void SpawnPlayer(Vector2Int startPos)
    {
        Vector3 spawnPos = new Vector3(startPos.x * 2, startPos.y * 2, 0);
        Instantiate(player, spawnPos, Quaternion.identity);
    }
}
