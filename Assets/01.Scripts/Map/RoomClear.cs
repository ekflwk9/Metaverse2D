using UnityEditor.EditorTools;
using UnityEngine;

public class RoomClear : MonoBehaviour
{
    // ���� ���� ��ǥ
    public Vector2Int currentRoomPos;
    // ��ü ���� �� ���� �迭
    public Room[,] grid;
    public MapManager mapManager;

    // �� Ŭ���� �õ� �Լ�: �� Ÿ�Կ� ���� Ŭ���� ���� Ȯ�� �� �ൿ ����
    public void TryClearRoom()
    {
        // ���� �� ������ ������
        Room currentRoom = grid[currentRoomPos.x, currentRoomPos.y];

        // ���� �������� �ʰų� ������Ʈ�� ������ ����
        if (currentRoom == null || currentRoom.RoomObject == null)
            return;

        // �� Ÿ�Կ� ���� Ŭ���� ���� �б� ó��
        switch (currentRoom.Type)
        {
            // ���� ��: ���� ���� ���� 0�̸� Ŭ����
            case RoomType.Battle:
                if (MonstersClear(currentRoom))
                {
                    // �� ���� �Ǵ� �ٸ� ���� �� Ŭ���� ó��
                    mapManager.OpenBridge(currentRoom.RoomObject, currentRoom.Position.x, currentRoom.Position.y);
                }
                break;

            // ���� ��: ���� ���Ͱ� ��� óġ�Ǿ��� ���
            //case RoomType.Boss:
            //    if (BossClear(currentRoom))
            //    {
            //        // ���� ������ �̵�
            //        mapManager.GoToNextFloor();

            //        GameManager.gameEvent.Call("CarWindowOn");
            //    }
            //    break;
            default:
                break;
        }
    }

    // ���� �� Ŭ���� ����: ���� �Ϲ� ���� �� Ȯ��
    private bool MonstersClear(Room room)
    {
        // ���� ���� 0 �����̸� Ŭ���� ���� ����
        return room.RemainingEnemies <= 0;
    }

    // ���� �� Ŭ���� ����: ���� óġ ���� Ȯ��
    //private bool BossClear(Room room)
    //{
    //    // ������ �׾����� ���� (Ȥ�� ���� �� Ȯ�� ����)
    //    return room.BossDefeated;  // �Ǵ� room.RemainingBosses <= 0;
    //}
}
