using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

// 방의 종류를 구분하는 열거형 (enum)
public enum RoomType
{
    Empty,    // 빈칸 
    Start,    // 시작 방 (플레이어 시작 위치)
    Battle,   // 전투 방 (일반 전투)
    Treasure, // 보물 방 (스킬 획득)
    Boss      // 보스 방 (층의 최종 목표)
}

// Room 클래스: 맵 상의 한 칸 정보를 저장하는 데이터 클래스
public class Room
{
    public Vector2Int Position; // 그리드상의 위치 
    public RoomType Type;       // 이 방의 타입
    public GameObject RoomObject; // 생성된 방 오브젝트 저장

    // 상하좌우 방향 연결 여부 확인, 
    public Dictionary<Vector2Int, bool> Connections = new Dictionary<Vector2Int, bool>
    {
        { Vector2Int.up, false },
        { Vector2Int.down, false },
        { Vector2Int.left, false },
        { Vector2Int.right, false }
    };

    public int RemainingEnemies;
    public bool IsCleared = false; // 전투방 클리어 여부를 저장하는 플래그 추가

    public void SetEnemies(int count)
    {
        RemainingEnemies = count;
        IsCleared = false; // 새로운 전투 시작 시 클리어 상태 초기화
    }

    public void EnemyDefeated()
    {
        RemainingEnemies--;
        if (RemainingEnemies <= 0 && !IsCleared)
        {
            IsCleared = true;
            MapManager.Instance.ClearBattleRoom(this); // RoomManager 통해 다리 열기
        }
    }
}