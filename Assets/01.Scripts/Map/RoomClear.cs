using UnityEditor.EditorTools;
using UnityEngine;

public class RoomClear : MonoBehaviour
{
    // 현재 방의 좌표
    public Vector2Int currentRoomPos;
    // 전체 맵의 방 정보 배열
    public Room[,] grid;
    public MapManager mapManager;

    // 방 클리어 시도 함수: 방 타입에 따라 클리어 조건 확인 후 행동 수행
    public void TryClearRoom()
    {
        // 현재 방 정보를 가져옴
        Room currentRoom = grid[currentRoomPos.x, currentRoomPos.y];

        // 방이 존재하지 않거나 오브젝트가 없으면 리턴
        if (currentRoom == null || currentRoom.RoomObject == null)
            return;

        // 방 타입에 따라 클리어 조건 분기 처리
        switch (currentRoom.Type)
        {
            // 전투 방: 남은 몬스터 수가 0이면 클리어
            case RoomType.Battle:
                if (MonstersClear(currentRoom))
                {
                    // 문 열기 또는 다리 연결 등 클리어 처리
                    mapManager.OpenBridge(currentRoom.RoomObject, currentRoom.Position.x, currentRoom.Position.y);
                }
                break;

            // 보스 방: 보스 몬스터가 모두 처치되었을 경우
            //case RoomType.Boss:
            //    if (BossClear(currentRoom))
            //    {
            //        // 다음 층으로 이동
            //        mapManager.GoToNextFloor();

            //        GameManager.gameEvent.Call("CarWindowOn");
            //    }
            //    break;
            default:
                break;
        }
    }

    // 전투 방 클리어 조건: 남은 일반 몬스터 수 확인
    private bool MonstersClear(Room room)
    {
        // 몬스터 수가 0 이하이면 클리어 조건 충족
        return room.RemainingEnemies <= 0;
    }

    // 보스 방 클리어 조건: 보스 처치 여부 확인
    //private bool BossClear(Room room)
    //{
    //    // 보스가 죽었는지 여부 (혹은 남은 수 확인 가능)
    //    return room.BossDefeated;  // 또는 room.RemainingBosses <= 0;
    //}
}
