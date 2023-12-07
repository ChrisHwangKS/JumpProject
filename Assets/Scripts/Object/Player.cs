using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// ������ ���� �Ǿ��� �� �߻���ų �̺�Ʈ�� ���� �븮�� ����
/// </summary>
public delegate void OnGameStartedSignature();

/// <summary>
/// ������ ����Ǿ��� ��� �߻���ų �̺�Ʈ�� ���� �븮�� ����
/// </summary>
/// <param name="currentScore">���� ������ ���޵˴ϴ�.</param>
public delegate void OnScoreChangedSignature(int currentScore);

/// <summary>
/// ������ ����Ǿ��� �� �߻���ų �̺�Ʈ�� ���� �븮�� ����
/// </summary>
public delegate void OnGameFinishedSignature();

/// <summary>
/// �÷��̾ �����ϴ� ĳ���Ϳ� ���� ������Ʈ�Դϴ�.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("ī�޶�")]
    public Camera m_Camera;

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
    /// ī�޶� �ʱ� ����׷��� Size �� �����ص� �����Դϴ�.
    /// </summary>
    private float _InitialCameraSize;

    /// <summary>
    /// SpriteRenderer ������Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    /// <summary>
    /// ������ ��Ÿ���ϴ�.
    /// </summary>
    private int _Score;

    /// <summary>
    /// ������ ����Ǿ��� ��� �߻��ϴ� �̺�Ʈ�Դϴ�.
    /// </summary>
    public event OnScoreChangedSignature onScoreChanged;

    /// <summary>
    /// ���� ������ �˸��� �̺�Ʈ�Դϴ�.
    /// </summary>
    public event OnGameStartedSignature onGameStarted;

    /// <summary>
    /// ���� ���� �˸��� �̺�Ʈ�Դϴ�.
    /// </summary>
    public event OnGameFinishedSignature onGameFinished;

    private void Awake()
    {
        // ī�޶� �ʱ� ������ ����
        _InitialCameraSize = m_Camera.orthographicSize;

        // GetComponentInChildren<T>();: �� ������Ʈ���� �ڽ� ������Ʈ�� ��� Ȯ���ϸ�
        // ������ ������Ʈ�� ã�� ��ȯ�ϴ� �޼���
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        

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
        // ī�޶� �� �ƿ�
        ZoomOutCamera();

        // ���콺 ���� Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            if(!_IsGameStarted)
            {
                _IsGameStarted = true;
                onGameStarted?.Invoke();
            }

            // Input : �Է¿� ���� ����� �����ϴ� Ŭ����
            // GetMouseButton() : ���콺 ��ư�� �����ִٸ� True
            // GetMouseButtonDown() : ���콺 ��ư�� ���� �� True
            // GetMouseButtonUp() : ���콺 ��ư�� ���ȴ� �������� �� True

            // ���� ��ŵ�ϴ�.
            Jump();

        }
    }

    /// <summary>
    /// ī�޶� �� �ƿ� �޼���
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

    /// <summary>
    /// ������ ������� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void OnLinePassed()
    {
        // ���� �߰�
        ++_Score;

        // ī�޶� ����׷��� ������ ����
        m_Camera.orthographicSize = 2.0f;

        onScoreChanged?.Invoke(_Score);
    }

    /// <summary>
    /// ���� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void OnGameOver()
    {
        // ���� ���� �̺�Ʈ �߻�
        onGameFinished?.Invoke();
    }
}
