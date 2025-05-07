using UnityEngine;

public class FadeComponent : MonoBehaviour
{
    public bool onFade { get; private set; }

    private Animator anim;
    private Func fadeFunc;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.SetComponent(this);
    }

    public void OnFade(Func _fadeFunc, float _fadeSpeed = 1f)
    {
        onFade = true;
        fadeFunc = _fadeFunc;

        anim.SetFloat("Speed", _fadeSpeed);
        anim.Play("FadeIn", 0, 0);
    }

    public void OnFade(float _fadeSpeed = 1f)
    {
        onFade = false;

        anim.SetFloat("Speed", _fadeSpeed);
        anim.Play("FadeOut", 0, 0);
    }

    private void EndFade()
    {
        //애니메이션 호출 메서드
        if (fadeFunc != null)
        {
            fadeFunc();
            fadeFunc = null;
        }
    }
}
