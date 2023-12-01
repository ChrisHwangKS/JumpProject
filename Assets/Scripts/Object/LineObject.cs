using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 겹침 확인을 위한 대리자 형식을 나타냅니다
/// </summary>
/// <param name="isPassable">겹친 라인 오브젝트가 플레이어를 통과 시킬 수 있음을 전달합니다.</param>
public delegate void OverlapEventSignature(bool isPassable);

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

    /// <summary>
    /// 플레이어와 겹쳤을 경우 발생시킬 이벤트를 나타냅니다.
    /// </summary>
    private OverlapEventSignature _OnPlayerOverlapped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        // 플레이어 겹침 이벤트를 발생시킵니다.
        _OnPlayerOverlapped?.Invoke(_IsPassable);
        //if(_OnPlayerOverlapped != null) _OnPlayerOverlapped(_IsPassable);
    }

    /// <summary>
    /// 라인 오브젝트를 초기화시킵니다.
    /// </summary>
    /// <param name="onPlayerOverlapped">플레이어와 겹쳤을 경우 발생시킬 이벤트를 전달합니다.</param>
    /// <param name="lineColor">설정시킬 색상을 전달합니다.</param>
    public void InitializeLineObject(
        OverlapEventSignature onPlayerOverlapped
        , Color lineColor, bool isPassable)
    {
        // 겹침 이벤트 설정
        _OnPlayerOverlapped = onPlayerOverlapped;

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
