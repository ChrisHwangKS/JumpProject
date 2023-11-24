using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    /// <summary>
    /// �� ����ŭ ������ �����صӴϴ�.
    /// </summary>
    [Header("���� ����")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

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
            LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);
            newLineGroup.transform.position += i * 0.5f * Vector3.down;
        }
    }
}
