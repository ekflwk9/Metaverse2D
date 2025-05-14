using UnityEngine;

public class DeadWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
        this.gameObject.SetActive(false);
    }

    private void On() => this.gameObject.SetActive(true);

    //�ִϸ��̼� ȣ�� �޼���
    private void EndDeadWindow()
    {
        this.gameObject.SetActive(false);
        GameManager.fade.OnFade(FadeFunc);
    }

    private void FadeFunc()
    {
        GameManager.fade.OnFade();
        GameManager.ChangeScene("Start");
        GameManager.player.transform.position = Vector3.one * 20;
        GameManager.player.gameObject.SetActive(true);
        GameManager.stopGame = true;
    }
}
