using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


/// <summary>
/// 플레이어가 조작하는 캐릭터에 들어가는 컴포넌트입니다.
/// </summary>
public class Player : MonoBehaviour
{
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
    /// SpriteRenderer 컴포넌트를 나타냅니다.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    private void Awake()
    {
        // GetComponentInChildren<T>();: 이 오브젝트부터 자식 오브젝트를 모두 확인하며
        // 지정한 컴포넌트를 찾아 반환하는 메서드
        _SpriteRenderer= GetComponentInChildren<SpriteRenderer>();
        

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
        // 마우스 왼쪽 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            _IsGameStarted = true;
            // Input : 입력에 대한 기능을 제공하는 클래스
            // GetMouseButton() : 마우스 버튼이 눌려있다면 True
            // GetMouseButtonDown() : 마우스 버튼을 누를 때 True
            // GetMouseButtonUp() : 마우스 버튼이 눌렸다 떼어졌을 때 True

            // 점프 시킵니다.
            Jump();

        }
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
}
