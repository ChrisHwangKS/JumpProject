using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGroup : MonoBehaviour
{
    [Header("라인 오브젝트")]
    public LineObject[] m_LineObjects;

    [Header("스크롤 속력")]
    public float m_ScrollSpeed = 5.0f;

    private void Update()
    {
        ScrollingLine();
    }

    /// <summary>
    /// 라인 오브젝트를 스크롤링 시킵니다.
    /// </summary>
    private void ScrollingLine()
    {
        // 속도
        Vector3 velocity = Vector3.right * m_ScrollSpeed;

        // 모든 라인 오브젝트들의 위치를 오른쪽으로 m_ScrollSpeed 만큼씩 이동시킵니다.
        foreach (LineObject lineObject in m_LineObjects) 
        {
            // 라인 위치를 얻습니다.
            Vector2 linePosition = lineObject.transform.position;

            // 오른쪽으로 이동시킵니다.
            linePosition.x += m_ScrollSpeed * Time.deltaTime;

            // 라인 오브젝트의 위치 X가 8을 초과하면 맨 왼쪽으로 배치
            if (linePosition.x > 8) linePosition.x -= 20.0f;


            // 위치를 적용합니다.
            lineObject.transform.position = linePosition;

            
        }
    }

}
