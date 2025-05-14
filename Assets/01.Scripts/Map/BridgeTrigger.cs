using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public string direction; // 이동 방향 ("Left", "Right", "Up", "Down" 중 하나)
    private MapManager mapManager; // 현재 맵 정보를 가지고 있는 MapManager 참조
    private Room nextRoom;
    
    private void Awake()
    {
        // MapManager를 씬에서 찾아서 연결
        mapManager = FindObjectOfType<MapManager>();

        // GameManager에서 이 컴포넌트를 등록해 관리하는 경우를 위한 처리
        GameManager.SetComponent(this);

    }
    private void Start()
    {
        // BoxCollider2D 가져오기
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

        // isTrigger가 false일 경우 true로 설정
        if (collider != null && !collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }

    private void FadeFunc()
    {
        var parent = this.transform.parent.parent;
        var parentName = parent.name;
        
        if (mapManager.battleRoomName.Contains(parentName))
        {
            var ranMonster = Random.Range(mapManager.currentFloor - 1, mapManager.currentFloor);

            Service.SpawnMonster(parent, mapManager.monsterName[ranMonster], mapManager.currentFloor * 5);
            mapManager.battleRoomName.Remove(parentName);
        }

        // 플레이어를 새 방의 위치로 이동
        GameManager.player.transform.position = nextRoom.RoomObject.transform.position;
        GameManager.fade.OnFade();
    }

    // 플레이어가 브리지 트리거에 닿았을 때 실행됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 플레이어가 아니면 무시
        if (!collision.CompareTag("Player")) return;

        // 방향 문자열에 따라 좌표 오프셋 설정
        Vector2Int offset = Vector2Int.zero;
        switch (direction)
        {
            case "Left":
                offset = Vector2Int.left;
                break;
            case "Right":
                offset = Vector2Int.right;
                break;
            case "Up":
                offset = Vector2Int.up;
                break;
            case "Down":
                offset = Vector2Int.down;
                break;
        }

        // 새로 이동할 방의 좌표 계산
        Vector2Int newRoomPos = mapManager.currentRoomPos + offset;

        // 맵 경계를 벗어나는지 확인
        int width = mapManager.grid.GetLength(0);
        int height = mapManager.grid.GetLength(1);
        if (newRoomPos.x >= 0 && newRoomPos.x < width && newRoomPos.y >= 0 && newRoomPos.y < height)
        {
            // 해당 좌표에 방이 존재하는지 확인
            nextRoom = mapManager.grid[newRoomPos.x, newRoomPos.y];
            if (nextRoom != null && nextRoom.RoomObject != null)
            {
                // 플레이어를 새 방의 위치로 이동
                //GameManager.player.transform.position = nextRoom.RoomObject.transform.position;
                GameManager.fade.OnFade(FadeFunc);

                // 현재 방 위치를 새 위치로 갱신
                mapManager.currentRoomPos = newRoomPos;

                // 디버그 로그 출력
                Service.Log($"방 이동 → {newRoomPos} ({nextRoom.Type})");
            }
            else
            {
                // 이동할 방이 존재하지 않을 때
                Service.Log("이동할 방이 없습니다.");
            }
        }
    }
}
