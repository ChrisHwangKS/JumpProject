using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� Ÿ���� ��Ÿ���� ���� ���� ����
/// </summary>
public enum ColorType : sbyte
{
    Red     = 0,
    Orange  = 1,
    Yellow  = 2,
    Green   = 3,
    Blue    = 4,
    Sky     = 5,
    White  = 6
}


public class LineGenerator : MonoBehaviour
{
    [Header("�÷��̾� ĳ����")]
    public Player m_Player;

    /// <summary>
    /// �� ����ŭ ������ �����صӴϴ�.
    /// </summary>
    [Header("���� ����")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

    [Header("���� ����")]
    public Color[] m_Colors;

    /// <summary>
    /// ���� ������ų ���� �������Դϴ�.
    /// </summary>
    [Header("���� ������")]
    public LineGroup m_LineGroupPrefab;

    /// <summary>
    /// ������ ���� ��ü���� ��Ÿ���ϴ�.
    /// </summary>
    private LineGroup[] _GeneratedLines;

    /// <summary>
    /// ������ ������ ���� ������ �����ų �����Դϴ�.
    /// �� ���� ���ο��� �ش� ������ ���Խ�Ű�� ���� ���ʴϴ�.
    /// White ������� ���۵Ǳ� ������ ù ���ο� White ���� �����ϵ��� �մϴ�.
    /// </summary>
    ColorType _NextPassableColor = ColorType.White;



    private void Awake()
    {
        // �迭 �޸� �Ҵ�
        _GeneratedLines = new LineGroup[m_ColumnCount];

        // ������ �����մϴ�.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            // ���� �׷��� �����մϴ�.
            _GeneratedLines[i] = GenerateLineGroup(i);
        }
    }

    /// <summary>
    /// ���� �׷��� �����մϴ�.
    /// </summary>
    /// <param name="index">���� �׷� �ε����� �����մϴ�.</param>
    /// <param name="nextColor">���Խ�ų ������ �����մϴ�.</param>
    /// <returns>������ ���� �׷��� ���d�մϴ�.</returns>
    private LineGroup GenerateLineGroup(int index)
    {
        // ���� �׷� ��ü�� ���� �����մϴ�.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // ������ų ���� �׷쿡 ���Խ�ų ������ �����صӴϴ�.
        ColorType inclusiveColor = _NextPassableColor;

        // ���� ������ �����ϰ� �����մϴ�.
        _NextPassableColor = GetRandomColorType();

        // ������ ���� �׷��� �ʱ�ȭ�մϴ�.
        newLineGroup.InitializeLineGroup(
            //�÷��̾� ��ü�� �����մϴ�.
            player :        m_Player,
            // �׷� �ε����� �����մϴ�.
            index :         index,
            // ���� �׷쿡�� ����� �� �ִ� ���� �迭�� �����մϴ�.
            colors :        m_Colors,
            // ���� ������Ʈ�鿡�� ������ų ������ ���� Ÿ�� �迭�� �����մϴ�.
            colorTypes :    GetSuffledColorTypeArray(inclusiveColor),
            // �� ���� �׷쿡�� �÷��̾ �����ų ���� Ÿ���� �����մϴ�. (���� ���� ����)
            passableColor : inclusiveColor,
            // ���� ������ ������� ��, �÷��̾�� ������ų ���� ������ �����մϴ�.
            // (���� ���ο� �ش� ������ ����)
            nextColor : _NextPassableColor,
            // �÷��̾ ���� �׷��� ����� ��� ȣ���� �޼��带 �����մϴ�.
            onLineGroupPassed : OnLineGroupPassed
            );

        // ������ ���� �׷� ��ü�� ��ȯ�մϴ�.
        return newLineGroup;
    }

    /// <summary>
    /// ������ ������ ��ȯ�ϴ� �޼���
    /// </summary>
    /// <returns>ColorType ������ ��� �� ������ ��Ҹ� ��ȯ�մϴ�. </returns>
    private ColorType GetRandomColorType()
    {
        // ���� Ÿ�� ���� ������ �迭�� ����ϴ�.
        ColorType[] colorTypeArray = System.Enum.GetValues(typeof(ColorType)) as ColorType[];

        // ������ ��Ҹ� ��ȯ
        return colorTypeArray[Random.Range(0,colorTypeArray.Length)];

    }

    /// <summary>
    /// ���� �׷쿡�� ���� �����ϰ� ���� ���� Ÿ�� �迭�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="inclusiveColor">�迭�� ���Խ�ų ������ �����մϴ�.</param>
    /// <returns>inclusiveColor �� ���Ե� ���� ���� �迭�� ��ȯ�մϴ�.</returns>
    private ColorType[] GetSuffledColorTypeArray(ColorType inclusiveColor)
    {
        // ���� ���� �Լ�
        void Swap(ref ColorType target1, ref ColorType target2) 
        {
            ColorType temp = target1;
            target1 = target2;
            target2 = temp;
        }

        ColorType[] colorTypeArray = System.Enum.GetValues(typeof(ColorType)) as ColorType[];
        /// System.Enum.GetValues(type)
        /// - type �� �ش��ϴ� �������� ��Ҹ� �迭�� ��ȯ�մϴ�.

        #region ��� ��ҵ��� �����ϴ�.
        // ��� 1�� ���ý�Ű�� ���� for��
        for(int i = 0; i <colorTypeArray.Length - 1; ++i)
        {
            // ��� 2�� �ε����� �����մϴ�.
            int swapTargetIndex = Random.Range(i + 1, colorTypeArray.Length);

            // ��� 1�� ��� 2�� ���ҽ�ŵ�ϴ�.
            Swap(ref colorTypeArray[i], ref colorTypeArray[swapTargetIndex]);
        }
        #endregion

        #region ���� ��ų ���� ����� �ε����� ã���ϴ�.
        int inclusiveColorIndex = -1;
        for (int i = 0; i <colorTypeArray.Length; ++i)
        {
            // ���� ���Խ�ų ���� ��Ҹ� ã�Ҵٸ�
            if (colorTypeArray[i] == inclusiveColor)
            {
                // ���Խ�ų ���� ��� �ε����� ��� �����մϴ�.
                inclusiveColorIndex = i;
                break;
            }
        }
        #endregion

        #region ���ԵǾ�� �ϴ� ������ ��ȯ��ų �迭 ���� ���� ���Խ�ŵ�ϴ�.
        if(inclusiveColorIndex > 4)
        {
            // �迭�� ���Խ�ų ���� ��ġ�� ������ų �ε����� �����մϴ�.
            int randomIndex = Random.Range(0, 5);

            // ���Խ�ų ���� ��ҿ� randomIndex ��ġ�� �ִ� ��Ҹ� �����մϴ�.
            Swap(ref colorTypeArray[inclusiveColorIndex], ref colorTypeArray[randomIndex]);
        }
        #endregion

        #region ��ȯ ��ų ��Ұ� 5���� �迭�� �����մϴ�.
        ColorType[] returnValue = new ColorType[5];

        // colorTypeArray ���� ��ҵ��� ���ʴ�� returnValue �迭�� �����մϴ�.
        for (int i = 0; i<returnValue.Length; ++i)
        {
            returnValue[i] = colorTypeArray[i];
        }
        #endregion

        // 5���� ��Ҹ� ��� �迭�� ��ȯ�մϴ�.
        return returnValue;
    }

    /// <summary>
    /// �÷��̾ ������ ����� ��� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    /// <param name="passedLineIndex">����� ���� �ε����� ���޵˴ϴ�.</param>
    /// <param name="nextColor">�÷��̾�� ���� ��ų ���� ������ ���޵˴ϴ�.</param>
    private void OnLineGroupPassed(int passedLineIndex, ColorType nextColor)
    {
        // ���� �׷� ����
        PopLineGroup(passedLineIndex);

        // ���� �׷� �߰�
        PushLineGroup();

        // �÷��̾��� ������ �����մϴ�.
        SetPlayerColor(nextColor);
    }

    /// <summary>
    /// ���� �׷��� �迭���� ���ϴ�.
    /// </summary>
    /// <param name="lineGroupIndex">�迭���� ���ܽ�ų ���� �׷� �ε����� �����մϴ�.</param>
    private void PopLineGroup(int lineGroupIndex)
    {
        // ����� ���� �׷� ��ü�� ����ϴ�.
        LineGroup passedLineGroup = _GeneratedLines[lineGroupIndex];

        // ����� ���� �׷��� �迭���� �����մϴ�.
        _GeneratedLines[lineGroupIndex] = null;

        // �� ������ Ž���� for ��
        // ù���� ��Һ��� ������ ����� ������ Ž���մϴ�.
        for(int i = 0; i < _GeneratedLines.Length - 1; ++i) 
        {
            // �� ������ �߰��� ���
            if (_GeneratedLines[i] == null) 
            {
                // ���� ����� ���� ��Ҹ� ã��, �� ������ ��ġ�� ��ȯ�մϴ�.
                for(int j = (i+1); j<_GeneratedLines.Length; ++j)
                {
                    // ������� ���� ���� �׷� ��ü�� �߰��� ���
                    if (_GeneratedLines[j] != null)
                    {
                        // ���� ��ġ�� ��ȯ�մϴ�.
                        LineGroup tempLineGroup = _GeneratedLines[i];
                        _GeneratedLines[i] = _GeneratedLines[j];
                        _GeneratedLines[j] = tempLineGroup;

                        // �� ������ ��ġ�� ��ȯ�� ���� �׷��� �ε����� �����մϴ�.
                        _GeneratedLines[i].SetLineGroupIndex(i);

                        // ������� ��ü�� �������Ƿ� ���� ��ȯ�� �����մϴ�.
                        break;
                    }
                }
            }

        }

        // �����Ų ���� �׷� ��ü�� �����մϴ�.
        Destroy(passedLineGroup.gameObject);
    }

    /// <summary>
    /// ���� �׷��� �߰��մϴ�.
    /// </summary>
    private void PushLineGroup()
    {
        // �迭�� �� ������ �ε����� ��Ÿ���� ���� ����
        int emptyIndex = GetEmptyLineGroupIndex();

        // �迭���� �� ������ ã�� ���ߴٸ� �߰� ���
        if (emptyIndex == -1) return;

        // ���� �׷��� �����մϴ�.
        GenerateLineGroup(emptyIndex);
    }

    /// <summary>
    /// �迭���� ���� �׷��� ������ �� �ִ� �� ��� �ε����� ã�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns>�� ����� �ε����� ��ȯ�մϴ�. ã�� ���� ��� -1�� ��ȯ�մϴ�.</returns>
    private int GetEmptyLineGroupIndex()
    {
        // �迭���� ������� ã���ϴ�.
        // ���� ù ��° ���� �׷��� ���ŵǰ�, PushLineGroup �޼��带 ȣ���ϰ� �˴ϴ�.
        // �� �� �迭�� ������� �ٸ� �׷��� �޲ٰ� �Ǹ�, ������ ��Ұ� �׻� ����ְ� �ǹǷ�
        // ������ ��Һ��� �� ������ Ž���ϵ��� �մϴ�.
        for (int i = (m_ColumnCount -1); (i>-1); --i)
        {
            // i ��° ��Ұ� ����ִٸ�
            if (_GeneratedLines[i] == null)
            {
                // �� ��� �ε����� ��ȯ
                return i;
            }
        }
        // ã�� ���� ��� ���������� �ε��� ��ȯ
        return -1;
    }

    /// <summary>
    /// �÷��̾��� ������ �����մϴ�.
    /// </summary>
    /// <param name="newColorType">�����ų ������ �����մϴ�.</param>
    private void SetPlayerColor(ColorType newColorType)
    {
        // ������ų ������ �迭���� ����ϴ�.
        Color newColor = m_Colors[(int)newColorType];

        // �÷��̾��� ������ �����մϴ�.
        m_Player.SetColor(newColor);
    }
}
