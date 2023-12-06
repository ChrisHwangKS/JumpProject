using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


/// <summary>
/// �÷��̾ �����ϴ� ĳ���Ϳ� ���� ������Ʈ�Դϴ�.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("���� ��")]
    public float m_JumpPower;

    [Header("�߷¿� ������ �¼�")]
    public float m_GravityMultiplier;

    /// <summary>
    /// ĳ���Ϳ� ����� Y �ӵ��Դϴ�.
    /// </summary>
    private float _YVelocity;

    /// <summary>
    /// ������ ���۵Ǿ����� ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsGameStarted;

    /// <summary>
    /// SpriteRenderer ������Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    private void Awake()
    {
        // GetComponentInChildren<T>();: �� ������Ʈ���� �ڽ� ������Ʈ�� ��� Ȯ���ϸ�
        // ������ ������Ʈ�� ã�� ��ȯ�ϴ� �޼���
        _SpriteRenderer= GetComponentInChildren<SpriteRenderer>();
        

        Time.fixedDeltaTime = 1 / 60.0f;
    }

    private void FixedUpdate()
    {
        // �߷� ���
        ApplyGravity();


        // �ӵ��� ���� ĳ���͸� �̵���ŵ�ϴ�.
        Move();
    }


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
    }


    /// <summary>
    /// �ӵ��� ���� ĳ���͸� �̵���ŵ�ϴ�.
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.up * _YVelocity;
    }

    /// <summary>
    /// �߷��� �����ŵ�ϴ�.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_IsGameStarted) return;

        // Edit -> ProjectStrings â�� Physics2D �� ������ �߷� Y ���� ����ϴ�.
        // �� ���� ������ ������ �߷°��̸�, �ʿ信 ���� ����ڰ� ������ �� �ֽ��ϴ�.
        float engineGravity = Mathf.Abs(Physics2D.gravity.y) * m_GravityMultiplier;

        // �ϰ� �ӵ��� ������ŵ�ϴ�.
        _YVelocity -= engineGravity;
    }

    /// <summary>
    /// ĳ���͸� ���� ��ŵ�ϴ�.
    /// </summary>
    public void Jump()
    {
        _YVelocity = m_JumpPower;
    }

    /// <summary>
    /// ������ �����մϴ�
    /// </summary>
    /// <param name="newColor">������ų ������ �����մϴ�.</param>
    public void SetColor(Color newColor)
    {
        // ǥ�õǴ� ������ �����մϴ�.
        _SpriteRenderer.color = newColor;
    }
}
