using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

// ���� ������ �����ϴ� ������ (enum)
public enum RoomType
{
    Empty,    // ��ĭ 
    Start,    // ���� �� (�÷��̾� ���� ��ġ)
    Battle,   // ���� �� (�Ϲ� ����)
    Treasure, // ���� �� (��ų ȹ��)
    Boss      // ���� �� (���� ���� ��ǥ)
}

// Room Ŭ����: �� ���� �� ĭ ������ �����ϴ� ������ Ŭ����
public class Room
{
    public Vector2Int Position; // �׸������ ��ġ 
    public RoomType Type;       // �� ���� Ÿ��
    public GameObject RoomObject; // ������ �� ������Ʈ ����

    // �����¿� ���� ���� ���� Ȯ��, 
    public Dictionary<Vector2Int, bool> Connections = new Dictionary<Vector2Int, bool>
    {
        { Vector2Int.up, false },
        { Vector2Int.down, false },
        { Vector2Int.left, false },
        { Vector2Int.right, false }
    };

    public int RemainingEnemies;
    public bool IsCleared = false; // ������ Ŭ���� ���θ� �����ϴ� �÷��� �߰�

    public void SetEnemies(int count)
    {
        RemainingEnemies = count;
        IsCleared = false; // ���ο� ���� ���� �� Ŭ���� ���� �ʱ�ȭ
    }

    public void EnemyDefeated()
    {
        RemainingEnemies--;
        if (RemainingEnemies <= 0 && !IsCleared)
        {
            IsCleared = true;
            MapManager.Instance.ClearBattleRoom(this); // RoomManager ���� �ٸ� ����
        }
    }
}