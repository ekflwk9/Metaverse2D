using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour
{
    [Header("���� ���� �� �̸� (Ű K)")]
    [SerializeField] private string sceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) GameManager.ChangeScene(sceneName);

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    string[] monster = { "Monster1", "Monster2" };
        //    Service.SpawnMonster(transform, monster, 5);
        //}

        if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager.gameEvent.Hit(GameManager.player.name, 20);
        }
    }
}
