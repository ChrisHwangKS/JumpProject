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


    private void Awake()
    {
        // ������ ������ ���� ������ �����ų �����Դϴ�.
        // �� ���� ���ο��� �ش� ������ ���Խ�Ű�� ���� ���ʴϴ�.
        // White ������� ���۵Ǳ� ������ ù ���ο� White ���� �����ϵ��� �մϴ�.
        ColorType nextColor = ColorType.White;

        // ������ �����մϴ�.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            // ���� �׷��� �����մϴ�.
            GenerateLineGroup(i, nextColor);
        }
    }

    /// <summary>
    /// ���� �׷��� �����մϴ�.
    /// </summary>
    /// <param name="index">���� �׷� �ε����� �����մϴ�.</param>
    /// <param name="nextColor">���Խ�ų ������ �����մϴ�.</param>
    /// <returns>������ ���� �׷��� ���d�մϴ�.</returns>
    private LineGroup GenerateLineGroup(int index, ColorType nextColor)
    {
        // ���� �׷� ��ü�� ���� �����մϴ�.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // ������ ���� �׷��� �ʱ�ȭ�մϴ�.
        newLineGroup.InitializeLineGroup(
            index,
            m_Colors,
            GetSuffledColorTypeArray(nextColor));

        // ������ ���� �׷� ��ü�� ��ȯ�մϴ�.
        return newLineGroup;
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
}
