using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObject : MonoBehaviour
{
    /// <summary>
    /// 이 라인 오브젝트 추가된 SpriteRenderer 컴포넌트를 나타냅니다.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    /// <summary>
    /// 라인 오브젝트를 초기화시킵니다.
    /// </summary>
    /// <param name="lineColor">설정시킬 색상을 전달합니다.</param>
    public void InitializeLineObject(Color lineColor)
    {
        // SpriteRenderer 컴포넌트를 얻습니다.
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        // 라인 색상을 설정합니다.
        _SpriteRenderer.color = lineColor;
    }
}
