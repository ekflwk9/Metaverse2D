using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComponent : MonoBehaviour
{
    private void Awake()
    {
        //Loading�� ��� ������ �Ҵ� �� ���� ����
        GameManager.effect.Load();
        GameManager.sound.Load();
        SceneManager.LoadScene("Start");
    }
}
