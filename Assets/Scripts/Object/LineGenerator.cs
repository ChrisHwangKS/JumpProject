using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� Ÿ���� ��Ÿ���� ���� ���� ����
/// </summary>
public enum ColorType : sbyte
{
    Red     = 0,
    Orange  = 1,
    Yellow  = 2,
    Green   = 3,
    Blue    = 4,
    Sky     = 5,
    Purple  = 6
}


public class LineGenerator : MonoBehaviour
{
    /// <summary>
    /// �� ����ŭ ������ �����صӴϴ�.
    /// </summary>
    [Header("���� ����")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

    [Header("���� ����")]
    public Color[] m_Colors;

    /// <summary>
    /// ���� ������ų ���� �������Դϴ�.
    /// </summary>
    [Header("���� ������")]
    public LineGroup m_LineGroupPrefab;


    private void Awake()
    {
        // ������ �����մϴ�.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            // ���� �׷��� �����մϴ�.
            GenerateLineGroup(i);
        }
    }

    /// <summary>
    /// ���� �׷��� �����մϴ�.
    /// </summary>
    /// <param name="index">���� �׷� �ε����� �����մϴ�.</param>
    /// <returns>������ ���� �׷��� ���d�մϴ�.</returns>
    private LineGroup GenerateLineGroup(int index)
    {
        // ���� �׷� ��ü�� ���� �����մϴ�.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // ������ ���� �׷��� �ʱ�ȭ�մϴ�.
        newLineGroup.InitializeLineGroup(index, m_Colors);

        // ������ ���� �׷� ��ü�� ��ȯ�մϴ�.
        return newLineGroup;
    }
}
