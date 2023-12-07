using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �׷� ��� Ȯ���� ���� �븮�� ������ ��Ÿ���ϴ�.
/// </summary>
/// <param name="passedLineIndex">����� ���� �ε���</param>
/// <param name="nextColor">�÷��̾�� ������ų ������ �����մϴ�.</param>
/// <param name="isGameOver">���� ���� ���θ� �����մϴ�.</param>
public delegate void OnLineGroupPassedSignature(int passedLineIndex, ColorType nextColor, bool isGameOver);

public class LineGroup : MonoBehaviour
{
    /// <summary>
    /// ���� �׷� ������ ������ ��Ÿ���ϴ�.
    /// </summary>
    private const float LINE_GROUP_TERM = 0.5f;

    /// <summary>
    /// ù��° ���� ������Ʈ�� ��ġ�� ��Ÿ���ϴ�.
    /// </summary>
    private static float _FirstLineObjectPosition;

    [Header("���� ������Ʈ")]
    public LineObject[] m_LineObjects;

    [Header("��ũ�� �ӷ�")]
    public float m_ScrollSpeed = 5.0f;

    /// <summary>
    /// ���� �׷� �ε����� ��Ÿ���ϴ�.
    /// ���� ���� ��ġ�� �׷��� 0������ ���˴ϴ�.
    /// �ش� �ε����� ���� ���� �׷��� ��ġ�Ǵ� ��ġ�� �����ǵ��� �մϴ�.
    /// </summary>
    private int _LineGroupIndex;

    /// <summary>
    /// ���� �׷� �̵� �ӵ�
    /// </summary>
    private float _LineGroupMoveSpeed = 20.0f;

    /// <summary>
    /// �ش� ���� �׷쿡�� ĳ���Ϳ� �浹���� ��,
    /// ĳ���Ͱ� ����� �� �ִ� ���� Ÿ���� ��Ÿ���ϴ�.
    /// </summary>
    private ColorType _PassableColor;

    /// <summary>
    /// �÷��̾ �� ���� �׷��� ������� ���,
    /// �÷��̾�� ������ų ������ ��Ÿ���ϴ�.
    /// </summary>
    private ColorType _NextColor;

    /// <summary>
    /// �÷��̾� ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private Player _Player;

    /// <summary>
    /// ���� �׷� ��� �� �߻���ų �̺�Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private OnLineGroupPassedSignature _OnLineGroupPassed;

    private void Update()
    {
        //���� �׷� ��ġ �̵�
        MoveLineGroup();

        // ���� ������Ʈ ��ũ�Ѹ�
        ScrollingLine();
    }

    /// <summary>
    /// ���� ������Ʈ�� ��ũ�Ѹ� ��ŵ�ϴ�.
    /// </summary>
    private void ScrollingLine()
    {
        // �ӵ�
        Vector3 velocity = Vector3.right * m_ScrollSpeed;

        // ��� ���� ������Ʈ���� ��ġ�� ���������� m_ScrollSpeed ��ŭ�� �̵���ŵ�ϴ�.
        foreach (LineObject lineObject in m_LineObjects) 
        {
            // ���� ��ġ�� ����ϴ�.
            Vector2 linePosition = lineObject.transform.position;

            // ���������� �̵���ŵ�ϴ�.
            linePosition.x += m_ScrollSpeed * Time.deltaTime;

            // ���� ������Ʈ�� ��ġ X�� 8�� �ʰ��ϸ� �� �������� ��ġ
            if (linePosition.x > 8) linePosition.x -= 20.0f;


            // ��ġ�� �����մϴ�.
            lineObject.transform.position = linePosition;
        }

        // �ٸ� ���ΰ� ��ġ�� ����ȭ ��Ű�� ����
        // ù ��° ���� �׷��� ��쿡�� ù ��° ���� ������Ʈ�� ��ġ�� �����մϴ�.
        if(_LineGroupIndex == 0)
        {
            _FirstLineObjectPosition = m_LineObjects[0].transform.localPosition.x;
        }
    }

    /// <summary>
    /// ���� �׷��� �ε��� ���� ���� �̵���ŵ�ϴ�.
    /// </summary>
    private void MoveLineGroup()
    {
        // ��ǥ Y ��ġ�� ����մϴ�.
        float targetYPosition = (_LineGroupIndex * LINE_GROUP_TERM) + LINE_GROUP_TERM;

        // ��ǥ ��ġ�� ���մϴ�.
        Vector2 targetPosition = Vector2.down * targetYPosition;

        // ���� ��ġ�� ����ϴ�.
        Vector2 currentPosition = transform.position;

        // ��ǥ Y ��ġ�� �ε巴�� �̵���ŵ�ϴ�.
        transform.position = Vector2.Lerp(currentPosition, targetPosition, _LineGroupMoveSpeed * Time.deltaTime);
        // Vector2.Lerp(Vector2 a, Vector2 b, float t)
        // ���� ������ ���� �Լ��Դϴ�.
        // ���� ���� : �� ���� �������� �����Ͽ� �� ����������
        // t ��ġ(0.0f = t <= 1.0f) �� ���� ���ϴ� ����Դϴ�.


    }

    /// <summary>
    /// ���� �׷� ���� ���� ������Ʈ���� �ʱ�ȭ�մϴ�
    /// </summary>
    /// <param name="colors">��� ������ ������� �����մϴ�.</param>
    /// <param name="colorTypes">���� ���� Ÿ�Ե��� ������� �����մϴ�.</param>
    private void InitializeLineObject(Color[] colors, ColorType[] colorTypes)
    {
        // �ι�° ���� ������Ʈ�� ��ġ
        float secondLineObjectXPos = m_LineObjects[1].transform.localPosition.x;
        // ù��° ���� ������Ʈ�� ��ġ
        float firstLineObjectXPos = m_LineObjects[0].transform.localPosition.x;
        // ���� ������Ʈ �� ������ ��
        float lineObjectTerm = secondLineObjectXPos - firstLineObjectXPos;


        for(int i = 0; i < m_LineObjects.Length; ++i)
        {
            // i ��° LineObject �� ����ϴ�.
            LineObject lineObject = m_LineObjects[i];

            // ������ų ������ ����ϴ�.
            ColorType colorType = colorTypes[i];

            // ������ų ������ ����ϴ�.
            Color lineColor = colors[(int)colorType];

            // ���� ������Ʈ �ʱ�ȭ
            lineObject.InitializeLineObject(
                // ĳ���� ��ħ �޼��� ����
                OnCharacterOverlapped,
                // ������ų ������ �����մϴ�.
                lineColor :     lineColor,
                // ��� ���� ���θ� �����մϴ�.
                isPassable :    (_PassableColor == colorType));

            // ���� ������Ʈ ��ġ ����ȭ
            lineObject.transform.localPosition = Vector3.right * (_FirstLineObjectPosition + i * lineObjectTerm);
        }
    }

    /// <summary>
    /// ���� �׷��� �ʱ�ȭ �մϴ�.
    /// </summary>
    /// <param name="player">�÷��̾� ��ü�� �����մϴ�.</param>
    /// <param name="index">������ų �ε����� �����մϴ�.</param>
    /// <param name="colors">���� ���� �迭�� �����մϴ�.</param>
    /// <param name="colorTypes">���� ���� Ÿ�� �迭�� �����մϴ�.</param>
    /// <param name="passableColor">�÷��̾� ĳ���͸� �����ų ���� Ÿ���� �����մϴ�</param>
    /// <param name="nextColor">�÷��̾ ����� �� �÷��̾�� ������ų ������ �����մϴ�.</param>
    /// <param name="onLineGroupPassed">���� �׷� ����� �߻���ų �̺�Ʈ�� �����մϴ�.</param>
    public void InitializeLineGroup(
        Player player,
        int index, 
        Color[] colors, 
        ColorType[] colorTypes,
        ColorType passableColor,
        ColorType nextColor,
        OnLineGroupPassedSignature onLineGroupPassed)
    {
        _Player = player;

        _PassableColor = passableColor;

        _NextColor = nextColor;

        _OnLineGroupPassed = onLineGroupPassed;

        // ���� �ε����� �����մϴ�.
        SetLineGroupIndex(index);

        // ���� ������Ʈ���� ��� �ʱ�ȭ�մϴ�.
        InitializeLineObject(colors, colorTypes);


    }

    /// <summary>
    /// ���� �׷� �ε����� �����մϴ�.
    /// </summary>
    /// <param name="newIndex">������ų �ε����� �����մϴ�.</param>
    public void SetLineGroupIndex(int newIndex)
    {
        _LineGroupIndex = newIndex;
    }

    /// <summary>
    /// �÷��̾ �� ���ο� ������ �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    /// <param name="isPassable"></param>
    private void OnCharacterOverlapped(bool isPassable)
    {
        // ��� ������ ������Ʈ�� ���
        if(isPassable)
        {
            // ���� �׷��� ��ġ�� ����ϴ�.
            Vector2 groupPosition = transform.position;

            // ������ų Y ��ġ�� ����մϴ�.
            float newYPosition = groupPosition.y + 1.0f;

            // �÷��̾� ��ġ�� ����
            Vector2 playerPosition = _Player.transform.position;
            playerPosition.y = newYPosition;

            // ����� ��ġ�� �����ŵ�ϴ�.
            _Player.transform.position = playerPosition;

            // �÷��̾� ����
            _Player.Jump();
            
        }
        // ��� �Ұ����� ������Ʈ�� ���
        else
        {
            Time.timeScale = 0.0f;
        }

        // ���� ��� �̺�Ʈ�� �߻���ŵ�ϴ�.
        _OnLineGroupPassed?.Invoke(_LineGroupIndex, _NextColor, isPassable);

    }

}
