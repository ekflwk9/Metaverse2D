using UnityEngine;

public class CamComponent : MonoBehaviour
{
    private Animator anim;
    private Transform target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameManager.player.transform;
    }

    private void Update()
    {
        //게임 플레이 중일 경우에만
        if (!GameManager.stopGame)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.05f);
        }
    }
}
