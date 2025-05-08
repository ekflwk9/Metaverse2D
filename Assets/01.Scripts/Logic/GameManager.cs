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
        //������Ʈ / interface�̺�Ʈ �Ҵ� �޼���
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
            Debug.Log("\"Loading\"�� ���� �ȵǴ� ���Դϴ�.");
            return;
        }

        //�� ��ȯ �̺�Ʈ �߻� �� ����
        gameEvent.EndEvent();
        gameEvent.Reset();
        gameEvent.SetComponent(player);

        //�÷��̾� ��ġ �缳��
        if (player != null) player.transform.position = Vector3.zero;

        //�� �ε�
        SceneManager.LoadScene(_sceneName);
    }
}
