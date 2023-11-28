using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용될 색상 타입을 나타내기 위한 열겨 형식
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
    /// 이 값만큼 라인을 생성해둡니다.
    /// </summary>
    [Header("라인 개수")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

    [Header("색상 정의")]
    public Color[] m_Colors;

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
            // 라인 그룹을 생성합니다.
            GenerateLineGroup(i);
        }
    }

    /// <summary>
    /// 라인 그룹을 생성합니다.
    /// </summary>
    /// <param name="index">라인 그룹 인덱스를 전달합니다.</param>
    /// <returns>생성된 라인 그룹을 반홥합니다.</returns>
    private LineGroup GenerateLineGroup(int index)
    {
        // 라인 그룹 객체를 복사 생성합니다.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // 생성된 라인 그룹을 초기화합니다.
        newLineGroup.InitializeLineGroup(index, m_Colors);

        // 생성된 라인 그룹 객체를 반환합니다.
        return newLineGroup;
    }
}
