using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Example()
    {
        //������Ʈ ���
        GameManager.SetComponent(this);

        //���� ȣ��
        GameManager.sound.OnEffect("PlayerHit");

        //����� ȣ��
        GameManager.sound.OnMusic(null);

        //�̺�Ʈ ���
        GameManager.gameEvent.Add(Event);

        //�̺�Ʈ ȣ��
        GameManager.gameEvent.Call("ObjectNameTestEventFunc");

        //�ش� ������Ʈ => ���� ������Ʈ �� "Controller"��� �ڽ� ������Ʈ�� ã�ƿ�
        var child = Service.FindChild(this.transform, "Controller");

        //Resources���� => Item ���� => Weapon�̶�� ������ �ҷ���
        var resource = Service.FindResource("Item", "Weapon");
    }

    public void Event()
    {
        //���̵� �� => ���̵� �� ����� �̺�Ʈ �Լ� ȣ��
        GameManager.fade.OnFade(EventFunc, 0.5f);
    }

    private void EventFunc()
    {
        //�� ��ȯ��
        GameManager.ChangeScene("NextSceneName");

        //�Ű����� �޼��� ���� ��� ���̵� �ƿ� ���
        GameManager.fade.OnFade(0.5f);
    }
}