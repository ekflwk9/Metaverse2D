using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameObject child;

    private void Awake()
    {
        //������Ʈ �Ŵ����� ���
        GameManager.SetComponent(this);

        //���� ȣ��
        GameManager.sound.OnEffect("PlayerHit");

        //����� ȣ��
        GameManager.sound.OnMusic(null);

        //���
        GameManager.gameEvent.Add(TestEventFunc);

        //ȣ��
        GameManager.gameEvent.Call("ObjectNameTestEventFunc");
    }

    private void TestEventFunc()
    {

    }

    public void Event()
    {
        //���̵� ��
        GameManager.fade.OnFade(EventFunc, 0.5f);
    }

    private void EventFunc()
    {
        //���̵� �ƿ�
        SceneManager.LoadScene("asd");
        GameManager.fade.OnFade(0.5f);
    }
}