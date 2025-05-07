using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameObject child;

    private void Awake()
    {
        //컴포넌트 매니저에 등록
        GameManager.SetComponent(this);

        //사운드 호출
        GameManager.sound.OnEffect("PlayerHit");

        //배경음 호출
        GameManager.sound.OnMusic(null);

        //등록
        GameManager.gameEvent.Add(TestEventFunc);

        //호출
        GameManager.gameEvent.Call("ObjectNameTestEventFunc");
    }

    private void TestEventFunc()
    {

    }

    public void Event()
    {
        //페이드 인
        GameManager.fade.OnFade(EventFunc, 0.5f);
    }

    private void EventFunc()
    {
        //페이드 아웃
        SceneManager.LoadScene("asd");
        GameManager.fade.OnFade(0.5f);
    }
}