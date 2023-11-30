using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        // �÷��̾� ��ġ�� ����

        // �÷��̾� ����
    }

    /// <summary>
    /// ���� ������Ʈ�� �ʱ�ȭ��ŵ�ϴ�.
    /// </summary>
    /// <param name="lineColor">������ų ������ �����մϴ�.</param>
    public void InitializeLineObject(Color lineColor, bool isPassable)
    {
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
