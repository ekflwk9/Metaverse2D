using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadComponent : MonoBehaviour
{
    private void Awake()
    {
        //GameManager.effect.Load();
        GameManager.sound.Load();
        SceneManager.LoadScene("Start");
    }
}
