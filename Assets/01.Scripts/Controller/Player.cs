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
        //컴포넌트 등록
        GameManager.SetComponent(this);

        //사운드 호출
        GameManager.sound.OnEffect("PlayerHit");

        //배경음 호출
        GameManager.sound.OnMusic(null);

        //이벤트 등록
        GameManager.gameEvent.Add(Event);

        //이벤트 호출
        GameManager.gameEvent.Call("ObjectNameTestEventFunc");

        //해당 오브젝트 => 하위 오브젝트 중 "Controller"라는 자식 오브젝트를 찾아옴
        var child = Service.FindChild(this.transform, "Controller");

        //Resources폴더 => Item 폴더 => Weapon이라는 파일을 불러옴
        var resource = Service.FindResource("Item", "Weapon");
    }

    public void Event()
    {
        //페이드 인 => 페이드 인 종료시 이벤트 함수 호출
        GameManager.fade.OnFade(EventFunc, 0.5f);
    }

    private void EventFunc()
    {
        //씬 전환시
        GameManager.ChangeScene("NextSceneName");

        //매개변수 메서드 없을 경우 페이드 아웃 재생
        GameManager.fade.OnFade(0.5f);
    }
}