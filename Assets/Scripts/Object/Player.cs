using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 게임이 시작 되었을 때 발생시킬 이벤트에 대한 대리자 형식
/// </summary>
public delegate void OnGameStartedSignature();

/// <summary>
/// 점수가 변경되었을 경우 발생시킬 이벤트에 대한 대리자 형식
/// </summary>
/// <param name="currentScore">현재 점수가 전달됩니다.</param>
public delegate void OnScoreChangedSignature(int currentScore);

/// <summary>
/// 게임이 종료되었을 때 발생시킬 이벤트에 대한 대리자 형식
/// </summary>
public delegate void OnGameFinishedSignature();

/// <summary>
/// 플레이어가 조작하는 캐릭터에 들어가는 컴포넌트입니다.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("카메라")]
    public Camera m_Camera;

    [Header("점프 힘")]
    public float m_JumpPower;

    [Header("중력에 곱해질 승수")]
    public float m_GravityMultiplier;

    /// <summary>
    /// 캐릭터에 적용된 Y 속도입니다.
    /// </summary>
    private float _YVelocity;

    /// <summary>
    /// 게임이 시작되었음을 나타냅니다.
    /// </summary>
    private bool _IsGameStarted;

    /// <summary>
    /// 카메라 초기 오쏘그래픽 Size 를 저장해둘 변수입니다.
    /// </summary>
    private float _InitialCameraSize;

    /// <summary>
    /// SpriteRenderer 컴포넌트를 나타냅니다.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    /// <summary>
    /// 점수를 나타냅니다.
    /// </summary>
    private int _Score;

    /// <summary>
    /// 점수가 변경되었을 경우 발생하는 이벤트입니다.
    /// </summary>
    public event OnScoreChangedSignature onScoreChanged;

    /// <summary>
    /// 게임 시작을 알리는 이벤트입니다.
    /// </summary>
    public event OnGameStartedSignature onGameStarted;

    /// <summary>
    /// 게임 끝을 알리는 이벤트입니다.
    /// </summary>
    public event OnGameFinishedSignature onGameFinished;

    private void Awake()
    {
        // 카메라 초기 사이즈 설정
        _InitialCameraSize = m_Camera.orthographicSize;

        // GetComponentInChildren<T>();: 이 오브젝트부터 자식 오브젝트를 모두 확인하며
        // 지정한 컴포넌트를 찾아 반환하는 메서드
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        

        Time.fixedDeltaTime = 1 / 60.0f;
    }

    private void FixedUpdate()
    {
        // 중력 계산
        ApplyGravity();


        // 속도에 따라 캐릭터를 이동시킵니다.
        Move();
    }


    private void Update()
    {
        // 카메라 줌 아웃
        ZoomOutCamera();

        // 마우스 왼쪽 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            if(!_IsGameStarted)
            {
                _IsGameStarted = true;
                onGameStarted?.Invoke();
            }

            // Input : 입력에 대한 기능을 제공하는 클래스
            // GetMouseButton() : 마우스 버튼이 눌려있다면 True
            // GetMouseButtonDown() : 마우스 버튼을 누를 때 True
            // GetMouseButtonUp() : 마우스 버튼이 눌렸다 떼어졌을 때 True

            // 점프 시킵니다.
            Jump();

        }
    }

    /// <summary>
    /// 카메라 줌 아웃 메서드
    /// </summary>
    private void ZoomOutCamera()
    {
        m_Camera.orthographicSize = Mathf.Lerp(
            m_Camera.orthographicSize,
            _InitialCameraSize,
            2.0f * Time.deltaTime
            );

    }


    /// <summary>
    /// 속도에 따라 캐릭터를 이동시킵니다.
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.up * _YVelocity;
    }

    /// <summary>
    /// 중력을 적용시킵니다.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_IsGameStarted) return;

        // Edit -> ProjectStrings 창의 Physics2D 에 설정된 중력 Y 값을 얻습니다.
        // 이 값은 엔진에 설정된 중력값이며, 필요에 따라 사용자가 변경할 수 있습니다.
        float engineGravity = Mathf.Abs(Physics2D.gravity.y) * m_GravityMultiplier;

        // 하강 속도를 증가시킵니다.
        _YVelocity -= engineGravity;
    }

    /// <summary>
    /// 캐릭터를 점프 시킵니다.
    /// </summary>
    public void Jump()
    {
        _YVelocity = m_JumpPower;
    }

    /// <summary>
    /// 색상을 설정합니다
    /// </summary>
    /// <param name="newColor">설정시킬 색상을 전달합니다.</param>
    public void SetColor(Color newColor)
    {
        // 표시되는 색상을 설정합니다.
        _SpriteRenderer.color = newColor;
    }

    /// <summary>
    /// 라인을 통과했을 때 호출되는 메서드입니다.
    /// </summary>
    public void OnLinePassed()
    {
        // 점수 추가
        ++_Score;

        // 카메라 오쏘그래픽 사이즈 설정
        m_Camera.orthographicSize = 2.0f;

        onScoreChanged?.Invoke(_Score);
    }

    /// <summary>
    /// 게임 오버 시 호출되는 메서드입니다.
    /// </summary>
    public void OnGameOver()
    {
        // 게임 오버 이벤트 발생
        onGameFinished?.Invoke();
    }
}
