using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour
{
    [Header("가고 싶은 씬 이름 (키 K)")]
    [SerializeField] private string sceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) GameManager.ChangeScene(sceneName);

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    string[] monster = { "Monster1", "Monster2" };
        //    Service.SpawnMonster(transform, monster, 5);
        //}

        if (Input.GetKeyDown(KeyCode.V))
        {
            var spawnPos = this.transform.position;
            GameManager.effect.Show(spawnPos, "Blood");
            GameManager.effect.FloorBlood(spawnPos);
        }

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    var spawnPos = this.transform.position;
        //    GameManager.effect.Damage(spawnPos, 1512);
        //}
    }
}
