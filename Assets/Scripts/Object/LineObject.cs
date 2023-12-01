using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ħ Ȯ���� ���� �븮�� ������ ��Ÿ���ϴ�
/// </summary>
/// <param name="isPassable">��ģ ���� ������Ʈ�� �÷��̾ ��� ��ų �� ������ �����մϴ�.</param>
public delegate void OverlapEventSignature(bool isPassable);

public class LineObject : MonoBehaviour
{
    /// <summary>
    /// �� ���� ������Ʈ �߰��� SpriteRenderer ������Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private SpriteRenderer _SpriteRenderer;

    /// <summary>
    /// �÷��̾� ��� ���� ���θ� ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsPassable;

    /// <summary>
    /// �÷��̾�� ������ ��� �߻���ų �̺�Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private OverlapEventSignature _OnPlayerOverlapped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        // �÷��̾� ��ħ �̺�Ʈ�� �߻���ŵ�ϴ�.
        _OnPlayerOverlapped?.Invoke(_IsPassable);
        //if(_OnPlayerOverlapped != null) _OnPlayerOverlapped(_IsPassable);
    }

    /// <summary>
    /// ���� ������Ʈ�� �ʱ�ȭ��ŵ�ϴ�.
    /// </summary>
    /// <param name="onPlayerOverlapped">�÷��̾�� ������ ��� �߻���ų �̺�Ʈ�� �����մϴ�.</param>
    /// <param name="lineColor">������ų ������ �����մϴ�.</param>
    public void InitializeLineObject(
        OverlapEventSignature onPlayerOverlapped
        , Color lineColor, bool isPassable)
    {
        // ��ħ �̺�Ʈ ����
        _OnPlayerOverlapped = onPlayerOverlapped;

        // SpriteRenderer ������Ʈ�� ����ϴ�.
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        // ���� ������ �����մϴ�.
        _SpriteRenderer.color = lineColor;

        // ��� ���� ���θ� �����մϴ�.
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
