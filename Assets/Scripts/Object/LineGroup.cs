using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGroup : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    public LineObject[] m_LineObjects;

    [Header("��ũ�� �ӷ�")]
    public float m_ScrollSpeed = 5.0f;

    private void Update()
    {
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

}
