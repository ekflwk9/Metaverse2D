using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static bool stopGame = false;
    public static Player player { get; private set; }
    public static UiComponent ui { get; private set; }
    public static FadeComponent fade { get; private set; }
    public static SoundManager sound { get; private set; } = new SoundManager();
    public static EventManager gameEvent { get; private set; } = new EventManager();

    public static void SetComponent(MonoBehaviour _component)
    {
        //컴포넌트 / interface이벤트 할당 메서드
        if (_component is Player isPlayer) player = isPlayer;
        else if (_component is UiComponent isUi) ui = isUi;
        else if (_component is FadeComponent isFade) fade = isFade;

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

        //씬 전환 이벤트 발생 및 정리
        gameEvent.EndEvent();
        gameEvent.Reset();
        gameEvent.SetComponent(player);

        //플레이어 위치 재설정
        if (player != null) player.transform.position = Vector3.zero;

        //씬 로드
        SceneManager.LoadScene(_sceneName);
    }
}
