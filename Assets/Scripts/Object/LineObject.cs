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
    /// ���� ������Ʈ�� �ʱ�ȭ��ŵ�ϴ�.
    /// </summary>
    /// <param name="lineColor">������ų ������ �����մϴ�.</param>
    public void InitializeLineObject(Color lineColor)
    {
        // SpriteRenderer ������Ʈ�� ����ϴ�.
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        // ���� ������ �����մϴ�.
        _SpriteRenderer.color = lineColor;
    }
}
