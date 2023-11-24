using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    /// <summary>
    /// 이 값만큼 라인을 생성해둡니다.
    /// </summary>
    [Header("라인 개수")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

    /// <summary>
    /// 복사 생성시킬 라인 프리팹입니다.
    /// </summary>
    [Header("라인 프리팹")]
    public LineGroup m_LineGroupPrefab;


    private void Awake()
    {
        // 라인을 생성합니다.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);
            newLineGroup.transform.position += i * 0.5f * Vector3.down;
        }
    }
}
