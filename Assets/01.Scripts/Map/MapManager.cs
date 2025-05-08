using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // 현재 층 (1층부터 시작)
    public int currentFloor = 1;

    // 1층 = 5x3, 2층 = 5x4, 3층 = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject startRoom;
    public GameObject battelRoom;
    public GameObject treasureRoom;
    public GameObject bossRoom;

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

        //// 1. StartRoom 배치
        //Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        //PlaceRoom(startPos, RoomType.Start);

        RandomRooms(RoomType.Start, 1, width, height);
        RandomRooms(RoomType.Boss, 1, width, height);

        // 3. 랜덤으로 나머지 방 생성하고 배치 1층은 전투방 7~10개, 보물방 1~3개
        int battleRooms = rng.Next(7, 11);   
        int treasureRooms = rng.Next(1, 4);

        RandomRooms(RoomType.Battle, battleRooms, width, height);
        RandomRooms(RoomType.Treasure, battleRooms, width, height);



        // 4. 연결이 안 된 방을 제거

        // 5. 각 방의 문 연결 상태 업데이트 

        // 6. 프리팹으로 방 생성 

        // 7. 플레이어 StartRoom에 스폰

    }
    //void PlaceRoom(Vector2Int pos, RoomType type)
    //{
    //    Room room = new Room { Position = pos, Type = type };
    //    grid[pos.x, pos.y] = room; // 배열에 등록
    //    rooms.Add(room); // 리스트에 추가
    //}

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

}
