using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGroup : MonoBehaviour
{
    /// <summary>
    /// ���� �׷� ������ ������ ��Ÿ���ϴ�.
    /// </summary>
    private const float LINE_GROUP_TERM = 0.5f;

    [Header("���� ������Ʈ")]
    public LineObject[] m_LineObjects;

    [Header("��ũ�� �ӷ�")]
    public float m_ScrollSpeed = 5.0f;

    /// <summary>
    /// ���� �׷� �ε����� ��Ÿ���ϴ�.
    /// ���� ���� ��ġ�� �׷��� 0������ ���˴ϴ�.
    /// �ش� �ε����� ���� ���� �׷��� ��ġ�Ǵ� ��ġ�� �����ǵ��� �մϴ�.
    /// </summary>
    private int _LineGroupIndex;

    /// <summary>
    /// ���� �׷� �̵� �ӵ�
    /// </summary>
    private float _LineGroupMoveSpeed = 20.0f;

    private void Update()
    {
        //���� �׷� ��ġ �̵�
        MoveLineGroup();

        // ���� ������Ʈ ��ũ�Ѹ�
        ScrollingLine();
    }

    /// <summary>
    /// ���� ������Ʈ�� ��ũ�Ѹ� ��ŵ�ϴ�.
    /// </summary>
    private void ScrollingLine()
    {
        // �ӵ�
        Vector3 velocity = Vector3.right * m_ScrollSpeed;

        // ��� ���� ������Ʈ���� ��ġ�� ���������� m_ScrollSpeed ��ŭ�� �̵���ŵ�ϴ�.
        foreach (LineObject lineObject in m_LineObjects) 
        {
            // ���� ��ġ�� ����ϴ�.
            Vector2 linePosition = lineObject.transform.position;

            // ���������� �̵���ŵ�ϴ�.
            linePosition.x += m_ScrollSpeed * Time.deltaTime;

            // ���� ������Ʈ�� ��ġ X�� 8�� �ʰ��ϸ� �� �������� ��ġ
            if (linePosition.x > 8) linePosition.x -= 20.0f;


            // ��ġ�� �����մϴ�.
            lineObject.transform.position = linePosition;

            
        }
    }

    /// <summary>
    /// ���� �׷��� �ε��� ���� ���� �̵���ŵ�ϴ�.
    /// </summary>
    private void MoveLineGroup()
    {
        // ��ǥ Y ��ġ�� ����մϴ�.
        float targetYPosition = (_LineGroupIndex * LINE_GROUP_TERM) + LINE_GROUP_TERM;

        // ��ǥ ��ġ�� ���մϴ�.
        Vector2 targetPosition = Vector2.down * targetYPosition;

        // ���� ��ġ�� ����ϴ�.
        Vector2 currentPosition = transform.position;

        // ��ǥ Y ��ġ�� �ε巴�� �̵���ŵ�ϴ�.
        transform.position = Vector2.Lerp(currentPosition, targetPosition, _LineGroupMoveSpeed * Time.deltaTime);
        // Vector2.Lerp(Vector2 a, Vector2 b, float t)
        // ���� ������ ���� �Լ��Դϴ�.
        // ���� ���� : �� ���� �������� �����Ͽ� �� ����������
        // t ��ġ(0.0f = t <= 1.0f) �� ���� ���ϴ� ����Դϴ�.


    }

    /// <summary>
    /// ���� �׷� ���� ���� ������Ʈ���� �ʱ�ȭ�մϴ�
    /// </summary>
    /// <param name="colors">��� ������ ������� �����մϴ�.</param>
    private void InitializeLineObject(Color[] colors)
    {
        for(int i = 0; i < m_LineObjects.Length; ++i)
        {
            // i ��° LineObject �� ����ϴ�.
            LineObject lineObject = m_LineObjects[i];

            // ������ų ������ ����ϴ�.
            Color lineColor = colors[Random.Range(0, colors.Length)];

            // ���� ������Ʈ �ʱ�ȭ
            lineObject.InitializeLineObject(lineColor);
        }
    }

    /// <summary>
    /// ���� �׷��� �ʱ�ȭ �մϴ�.
    /// </summary>
    /// <param name="index">������ų �ε����� �����մϴ�.</param>
    /// <param name="colors">���� ���� �迭�� �����մϴ�.</param>
    public void InitializeLineGroup(int index, Color[] colors)
    {
        // ���� �ε����� �����մϴ�.
        SetLineGroupIndex(index);

        // ���� ������Ʈ���� ��� �ʱ�ȭ�մϴ�.
        InitializeLineObject(colors);
    }

    /// <summary>
    /// ���� �׷� �ε����� �����մϴ�.
    /// </summary>
    /// <param name="newIndex">������ų �ε����� �����մϴ�.</param>
    public void SetLineGroupIndex(int newIndex)
    {
        _LineGroupIndex = newIndex;
    }



}
