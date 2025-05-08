using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // ���� �� (1������ ����)
    public int currentFloor = 1;

    // 1�� = 5x3, 2�� = 5x4, 3�� = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject startRoom;
    public GameObject battelRoom;
    public GameObject treasureRoom;
    public GameObject bossRoom;

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

        //// 1. StartRoom ��ġ
        //Vector2Int startPos = new Vector2Int(rng.Next(width), rng.Next(height));
        //PlaceRoom(startPos, RoomType.Start);

        RandomRooms(RoomType.Start, 1, width, height);
        RandomRooms(RoomType.Boss, 1, width, height);

        // 3. �������� ������ �� �����ϰ� ��ġ 1���� ������ 7~10��, ������ 1~3��
        int battleRooms = rng.Next(7, 11);   
        int treasureRooms = rng.Next(1, 4);

        RandomRooms(RoomType.Battle, battleRooms, width, height);
        RandomRooms(RoomType.Treasure, battleRooms, width, height);



        // 4. ������ �� �� ���� ����

        // 5. �� ���� �� ���� ���� ������Ʈ 

        // 6. ���������� �� ���� 

        // 7. �÷��̾� StartRoom�� ����

    }
    //void PlaceRoom(Vector2Int pos, RoomType type)
    //{
    //    Room room = new Room { Position = pos, Type = type };
    //    grid[pos.x, pos.y] = room; // �迭�� ���
    //    rooms.Add(room); // ����Ʈ�� �߰�
    //}

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

}
