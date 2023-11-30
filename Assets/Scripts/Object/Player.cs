using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어가 조작하는 캐릭터에 들어가는 컴포넌트입니다.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("점프 힘")]
    public float m_JumpPower;

    /// <summary>
    /// 캐릭터에 적용된 Y 속도입니다.
    /// </summary>
    private float _YVelocity;

    /// <summary>
    /// 게임이 시작되었음을 나타냅니다.
    /// </summary>
    private bool _IsGameStarted;


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

        // 중력 계산
        ApplyGravity();


        // 속도에 따라 캐릭터를 이동시킵니다.
        Move();
    }

    /// <summary>
    /// 캐릭터를 점프 시킵니다.
    /// </summary>
    public void Jump()
    {
        _YVelocity = m_JumpPower;
    }

    /// <summary>
    /// 속도에 따라 캐릭터를 이동시킵니다.
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.up * _YVelocity * Time.deltaTime;
    }

    /// <summary>
    /// 중력을 적용시킵니다.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_IsGameStarted) return;

        // Edit -> ProjectStrings 창의 Physics2D 에 설정된 중력 Y 값을 얻습니다.
        // 이 값은 엔진에 설정된 중력값이며, 필요에 따라 사용자가 변경할 수 있습니다.
        float engineGravity = Mathf.Abs(Physics2D.gravity.y) * 0.01f;

        // 하강 속도를 증가시킵니다.
        _YVelocity -= engineGravity;
    }
}
