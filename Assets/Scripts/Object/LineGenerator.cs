using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용될 색상 타입을 나타내기 위한 열겨 형식
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
    /// 이 값만큼 라인을 생성해둡니다.
    /// </summary>
    [Header("라인 개수")]
    [Range(1,15)]
    public int m_ColumnCount = 1;

    [Header("색상 정의")]
    public Color[] m_Colors;

    /// <summary>
    /// 복사 생성시킬 라인 프리팹입니다.
    /// </summary>
    [Header("라인 프리팹")]
    public LineGroup m_LineGroupPrefab;


    private void Awake()
    {
        // 생성된 라인의 정답 색상을 저장시킬 변수입니다.
        // 그 다음 라인에서 해당 색상을 포함시키기 위해 사용됨니다.
        // White 색상부터 시작되기 때문에 첫 라인에 White 색을 포함하도록 합니다.
        ColorType nextColor = ColorType.White;

        // 라인을 생성합니다.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            // 라인 그룹을 생성합니다.
            GenerateLineGroup(i, nextColor);
        }
    }

    /// <summary>
    /// 라인 그룹을 생성합니다.
    /// </summary>
    /// <param name="index">라인 그룹 인덱스를 전달합니다.</param>
    /// <param name="nextColor">포함시킬 색상을 전달합니다.</param>
    /// <returns>생성된 라인 그룹을 반홥합니다.</returns>
    private LineGroup GenerateLineGroup(int index, ColorType nextColor)
    {
        // 라인 그룹 객체를 복사 생성합니다.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // 생성된 라인 그룹을 초기화합니다.
        newLineGroup.InitializeLineGroup(
            index,
            m_Colors,
            GetSuffledColorTypeArray(nextColor));

        // 생성된 라인 그룹 객체를 반환합니다.
        return newLineGroup;
    }

    /// <summary>
    /// 라인 그룹에서 사용될 랜덤하게 섞인 색상 타입 배열을 반환합니다.
    /// </summary>
    /// <param name="inclusiveColor">배열에 포함시킬 색상을 전달합니다.</param>
    /// <returns>inclusiveColor 가 포함된 섞인 색상 배열을 반환합니다.</returns>
    private ColorType[] GetSuffledColorTypeArray(ColorType inclusiveColor)
    {
        // 셔플 로컬 함수
        void Swap(ref ColorType target1, ref ColorType target2) 
        {
            ColorType temp = target1;
            target1 = target2;
            target2 = temp;
        }

        ColorType[] colorTypeArray = System.Enum.GetValues(typeof(ColorType)) as ColorType[];
        /// System.Enum.GetValues(type)
        /// - type 에 해당하는 열겨형식 요소를 배열로 반환합니다.

        #region 모든 요소들을 섞습니다.
        // 대상 1을 선택시키기 위한 for문
        for(int i = 0; i <colorTypeArray.Length - 1; ++i)
        {
            // 대상 2의 인덱스를 설정합니다.
            int swapTargetIndex = Random.Range(i + 1, colorTypeArray.Length);

            // 대상 1과 대상 2를 스왑시킵니다.
            Swap(ref colorTypeArray[i], ref colorTypeArray[swapTargetIndex]);
        }
        #endregion

        #region 포함 시킬 색상 요소의 인덱스를 찾습니다.
        int inclusiveColorIndex = -1;
        for (int i = 0; i <colorTypeArray.Length; ++i)
        {
            // 만약 포함시킬 색상 요소를 찾았다면
            if (colorTypeArray[i] == inclusiveColor)
            {
                // 포함시킬 색상 요소 인덱스를 잠시 저장합니다.
                inclusiveColorIndex = i;
                break;
            }
        }
        #endregion

        #region 포함되어야 하는 색상을 반환시킬 배열 범위 내에 포함시킵니다.
        if(inclusiveColorIndex > 4)
        {
            // 배열의 포함시킬 값의 위치를 결정시킬 인덱스를 설정합니다.
            int randomIndex = Random.Range(0, 5);

            // 포함시킬 색상 요소와 randomIndex 위치에 있는 요소를 스왑합니다.
            Swap(ref colorTypeArray[inclusiveColorIndex], ref colorTypeArray[randomIndex]);
        }
        #endregion

        #region 반환 시킬 요소가 5개인 배열을 생성합니다.
        ColorType[] returnValue = new ColorType[5];

        // colorTypeArray 에서 요소들을 차례대로 returnValue 배열에 저장합니다.
        for (int i = 0; i<returnValue.Length; ++i)
        {
            returnValue[i] = colorTypeArray[i];
        }
        #endregion

        // 5개의 요소를 담는 배열을 반환합니다.
        return returnValue;
    }
}
