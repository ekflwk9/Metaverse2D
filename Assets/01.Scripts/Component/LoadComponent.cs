using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComponent : MonoBehaviour
{
    private void Awake()
    {
        //Loading씬 모든 데이터 할당 후 게임 시작
        SpawnSkill();
        GameManager.sprite.Load();
        GameManager.effect.Load();
        GameManager.sound.Load();
        SceneManager.LoadScene("Start");
    }

    private void SpawnSkill()
    {
        var allSkill = Resources.LoadAll<GameObject>("Skill");

        for (int i = 0; i < allSkill.Length; i++)
        {
            var skill = Instantiate(allSkill[i]);
            skill.name = allSkill[i].name;

            skill.GetComponent<BaseSkill>().SetSkill();
            skill.gameObject.SetActive(false);
        }
    }
}