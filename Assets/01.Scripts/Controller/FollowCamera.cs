using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 카메라가 이동할 수 있는 맵의 최소 좌표와 최대 좌표 (경계 영역)
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // 카메라의 절반 높이, 절반 너비 (화면 크기 절반, Clamp 계산용)
    private float halfHeight;
    private float halfWidth;

    // 플레이어가 처음 생성되었는지 여부
    private bool playerInitialized = false;

    // Start는 게임 시작 시 한 번만 실행
    void Start()
    {
        // Camera 컴포넌트 가져오기
        var cam = Service.FindChild(this.transform, "Main Camera").GetComponent<Camera>();

        // orthographicSize로 화면 절반 높이 계산
        halfHeight = cam.orthographicSize;
        // aspect로 화면 절반 너비 계산
        halfWidth = halfHeight * cam.aspect;

        this.gameObject.SetActive(false);
        GameManager.gameEvent.Add(On, true);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update는 매 프레임 실행 (플레이어 존재 확인 및 초기화)
    void Update()
    {
        if (!playerInitialized && GameManager.player != null)
        {
            // 카메라를 플레이어 위치로 즉시 이동 [수정됨]
            transform.position = new Vector3(GameManager.player.transform.position.x,
                                             GameManager.player.transform.position.y,
                                             transform.position.z);

            // 플레이어 위치를 기준으로 경계 재설정
            CenterBoundsOn(GameManager.player.transform.position);
            playerInitialized = true;
        }

        if (GameManager.player != null)
        {
            // 플레이어 위치 좌표 가져오기
            float desiredX = GameManager.player.transform.position.x;
            float desiredY = GameManager.player.transform.position.y;

            // 현재 경계 계산 (카메라 외곽을 고려하여 계산)
            float minX = minBounds.x + halfWidth;
            float maxX = maxBounds.x - halfWidth;
            float minY = minBounds.y + halfHeight;
            float maxY = maxBounds.y - halfHeight;

            // 플레이어가 경계 안에 있는지 체크
            bool isPlayerInsideBounds = desiredX >= minBounds.x && desiredX <= maxBounds.x &&
                                        desiredY >= minBounds.y && desiredY <= maxBounds.y;

            if (isPlayerInsideBounds)
            {
                // 플레이어가 경계 안에 있을 때
                // 카메라는 경계 내에서만 움직임 (Clamp로 제한)
                float clampedX = Mathf.Clamp(desiredX, minX, maxX);
                float clampedY = Mathf.Clamp(desiredY, minY, maxY);

                transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            }
            else
            {
                // 플레이어가 경계를 벗어났을 때
                // 카메라를 플레이어 위치로 즉시 이동
                transform.position = new Vector3(desiredX, desiredY, transform.position.z);

                // 경계도 플레이어 위치를 중심으로 재조정
                CenterBoundsOn(new Vector2(desiredX, desiredY));
            }
        }
    }

    // CenterBoundsOn: 주어진 중심 좌표로 경계를 재조정하는 함수
    private void CenterBoundsOn(Vector2 center)
    {
        // 경계 너비, 높이 계산
        float width = maxBounds.x - minBounds.x;
        float height = maxBounds.y - minBounds.y;

        // 중심 좌표 기준으로 minBounds, maxBounds 재설정
        minBounds = new Vector2(center.x - width / 2, center.y - height / 2);
        maxBounds = new Vector2(center.x + width / 2, center.y + height / 2);
    }

    // OnDrawGizmos: 씬 뷰에서 경계를 시각적으로 표시 (디버그용)
    void OnDrawGizmos()
    {
        // Gizmos 색상을 초록색으로 설정
        Gizmos.color = Color.green;

        // 경계 중심 좌표 계산
        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) / 2,
            (minBounds.y + maxBounds.y) / 2,
            0
        );

        // 경계 크기 계산
        Vector3 size = new Vector3(
            (maxBounds.x - minBounds.x),
            (maxBounds.y - minBounds.y),
            0
        );

        // WireCube로 경계 박스 그리기
        Gizmos.DrawWireCube(center, size);
    }

    private void On() => this.gameObject.SetActive(true); 
}
