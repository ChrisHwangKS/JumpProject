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
    /// 플레이어 통과 가능 여부를 나타냅니다.
    /// </summary>
    private bool _IsPassable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        // 플레이어 위치를 조절

        // 플레이어 점프
    }

    /// <summary>
    /// 라인 오브젝트를 초기화시킵니다.
    /// </summary>
    /// <param name="lineColor">설정시킬 색상을 전달합니다.</param>
    public void InitializeLineObject(Color lineColor, bool isPassable)
    {
        // SpriteRenderer 컴포넌트를 얻습니다.
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        // 라인 색상을 설정합니다.
        _SpriteRenderer.color = lineColor;

        // 통과 가능 여부를 설정합니다.
        _IsPassable = isPassable;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(_IsPassable) 
        {
            Gizmos.color = Color.black;

            Gizmos.DrawWireCube(
                transform.position,
                new Vector3(4.0f, 0.4f, 0.0f));
        }
    }
#endif
}
