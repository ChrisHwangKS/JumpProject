using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾ �����ϴ� ĳ���Ϳ� ���� ������Ʈ�Դϴ�.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("���� ��")]
    public float m_JumpPower;

    /// <summary>
    /// ĳ���Ϳ� ����� Y �ӵ��Դϴ�.
    /// </summary>
    private float _YVelocity;

    /// <summary>
    /// ������ ���۵Ǿ����� ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsGameStarted;


    private void Update()
    {
        // ���콺 ���� Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            _IsGameStarted = true;
            // Input : �Է¿� ���� ����� �����ϴ� Ŭ����
            // GetMouseButton() : ���콺 ��ư�� �����ִٸ� True
            // GetMouseButtonDown() : ���콺 ��ư�� ���� �� True
            // GetMouseButtonUp() : ���콺 ��ư�� ���ȴ� �������� �� True

            // ���� ��ŵ�ϴ�.
            Jump();

        }

        // �߷� ���
        ApplyGravity();


        // �ӵ��� ���� ĳ���͸� �̵���ŵ�ϴ�.
        Move();
    }

    /// <summary>
    /// ĳ���͸� ���� ��ŵ�ϴ�.
    /// </summary>
    public void Jump()
    {
        _YVelocity = m_JumpPower;
    }

    /// <summary>
    /// �ӵ��� ���� ĳ���͸� �̵���ŵ�ϴ�.
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.up * _YVelocity * Time.deltaTime;
    }

    /// <summary>
    /// �߷��� �����ŵ�ϴ�.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_IsGameStarted) return;

        // Edit -> ProjectStrings â�� Physics2D �� ������ �߷� Y ���� ����ϴ�.
        // �� ���� ������ ������ �߷°��̸�, �ʿ信 ���� ����ڰ� ������ �� �ֽ��ϴ�.
        float engineGravity = Mathf.Abs(Physics2D.gravity.y) * 0.01f;

        // �ϰ� �ӵ��� ������ŵ�ϴ�.
        _YVelocity -= engineGravity;
    }
}
