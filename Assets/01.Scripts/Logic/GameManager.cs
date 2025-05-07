using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static bool stopGame = false;
    public static Player player { get; private set; }
    public static FadeComponent fade { get; private set; }
    public static SoundManager sound { get; private set; } = new SoundManager();
    public static EventManager gameEvent { get; private set; } = new EventManager();

    public static void SetComponent(MonoBehaviour _component)
    {
        if (_component is Player isPlayer) player = isPlayer;
        if (_component is FadeComponent isFade) fade = isFade;

        sound.SetComponent(_component);
        gameEvent.SetComponent(_component);
    }

    public static void ChangeScene(string _sceneName)
    {
        if (_sceneName == "Loading")
        {
            Debug.Log("\"Loading\"은 가면 안되는 씬입니다.");
            return;
        }

        gameEvent.Reset();
        gameEvent.SetComponent(player);
        if(player != null) player.transform.position = Vector3.zero;

        SceneManager.LoadScene(_sceneName);
    }
}
