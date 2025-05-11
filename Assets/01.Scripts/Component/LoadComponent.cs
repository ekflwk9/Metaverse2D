using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComponent : MonoBehaviour
{
    private void Awake()
    {
        //Loading씬 모든 데이터 할당 후 게임 시작
        GameManager.effect.Load();
        GameManager.sound.Load();
        SceneManager.LoadScene("Start");
    }
}
