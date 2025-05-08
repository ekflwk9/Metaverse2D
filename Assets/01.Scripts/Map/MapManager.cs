using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject player;
    // ���� �� (1������ ����)
    public int currentFloor = 1;

    // 1�� = 5x3, 2�� = 5x4, 3�� = 5x5
    private int[] floorWidth = { 5, 5, 5 };
    private int[] floorHeight = { 3, 4, 5 };

    public GameObject startRoom;
    public GameObject battleRoom;
    public GameObject treasureRoom;
    public GameObject bossRoom;

    private Room[,] grid; // ���� 2D �迭
    private List<Room> rooms = new List<Room>(); // ���� ������ �� ����Ʈ
    private System.Random rng = new System.Random(); // ���� ����

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

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
        RandomRooms(RoomType.Treasure, battleRooms, width, height);


        // 4. ������ ��ġ 
        SpawnRooms();

        // 5. �÷��̾� StartRoom�� ����
        SpawnPlayer(startPos);
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

            // ���� ���� ��� �õ�
            foreach (Vector2Int dir in GetShuffledDirections())
            {
                Vector2Int nextPos = randomRoom.Position + dir; // ���ο� ��ġ�� ���
                
                if (IsInBounds(nextPos, width, height) && grid[nextPos.x, nextPos.y] == null) // �� ���� ���̸鼭 ���� �� ĭ�̸� ��ġ
                {
                    PlaceRoom(nextPos, type);
                    placed++;
                    break; // �� ĭ�� ��ġ�ϰ� ���� �ݺ�����
                }
            }
        }
    }

    // �־��� ��ǥ�� �� ���� �ִ��� Ȯ���ϴ� �Լ�
    bool IsInBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    // ���� ����Ʈ ���� ���� (�� ��ġ ���� ������ȭ��)
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

    //RoomType�� �´� GameObject �������� ��ȯ
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

    // ������ ��ġ
    void SpawnRooms()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; x<= grid.GetLength(1); y++)
            {
                Room room = grid[x, y]; // grid �迭���� (x, y) ��ġ�� �ִ� Room ��������

                if (room != null)
                {
                    Vector3 worldPos = new Vector3(x * 2, y * 2, 0); // 2�� �������� ��ġ (���߿� ������ ũ�⿡ ���� ����)
                    GameObject prefab = GetPrefab(room.Type);
                }
            }
        }
    }

    // �÷��̾ StartRoom�� ����
    void SpawnPlayer(Vector2Int startPos)
    {
        Vector3 spawnPos = new Vector3(startPos.x * 2, startPos.y * 2, 0);
        Instantiate(player, spawnPos, Quaternion.identity);
    }
}
