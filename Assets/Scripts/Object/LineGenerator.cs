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
    [Header("플레이어 캐릭터")]
    public Player m_Player;

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

    /// <summary>
    /// 생성된 라인 객체들을 나타냅니다.
    /// </summary>
    private LineGroup[] _GeneratedLines;

    /// <summary>
    /// 생성된 라인의 정답 색상을 저장시킬 변수입니다.
    /// 그 다음 라인에서 해당 색상을 포함시키기 위해 사용됨니다.
    /// White 색상부터 시작되기 때문에 첫 라인에 White 색을 포함하도록 합니다.
    /// </summary>
    ColorType _NextPassableColor = ColorType.White;



    private void Awake()
    {
        // 배열 메모리 할당
        _GeneratedLines = new LineGroup[m_ColumnCount];

        // 라인을 생성합니다.
        for(int i = 0; i <m_ColumnCount; ++i)
        {
            // 라인 그룹을 생성합니다.
            _GeneratedLines[i] = GenerateLineGroup(i);
        }
    }

    /// <summary>
    /// 라인 그룹을 생성합니다.
    /// </summary>
    /// <param name="index">라인 그룹 인덱스를 전달합니다.</param>
    /// <param name="nextColor">포함시킬 색상을 전달합니다.</param>
    /// <returns>생성된 라인 그룹을 반홥합니다.</returns>
    private LineGroup GenerateLineGroup(int index)
    {
        // 라인 그룹 객체를 복사 생성합니다.
        LineGroup newLineGroup = Instantiate(m_LineGroupPrefab);

        // 생성시킬 라인 그룹에 포함시킬 색상을 저장해둡니다.
        ColorType inclusiveColor = _NextPassableColor;

        // 다음 색상을 랜덤하게 설정합니다.
        _NextPassableColor = GetRandomColorType();

        // 생성된 라인 그룹을 초기화합니다.
        newLineGroup.InitializeLineGroup(
            //플레이어 객체를 전달합니다.
            player :        m_Player,
            // 그룹 인덱스를 전달합니다.
            index :         index,
            // 라인 그룹에서 사용할 수 있는 색상 배열을 전달합니다.
            colors :        m_Colors,
            // 라인 오브젝트들에게 설정시킬 랜덤한 색상 타입 배열을 전달합니다.
            colorTypes :    GetSuffledColorTypeArray(inclusiveColor),
            // 이 라인 그룹에서 플레이어를 통과시킬 색상 타입을 전달합니다. (정답 색상 전달)
            passableColor : inclusiveColor,
            // 정답 색상을 통과했을 떄, 플레이어에게 설정시킬 다음 색상을 전달합니다.
            // (다음 라인에 해당 색상을 포함)
            nextColor : _NextPassableColor,
            // 플레이어가 라인 그룹을 통과할 경우 호출할 메서드를 전달합니다.
            onLineGroupPassed : OnLineGroupPassed
            );

        // 생성된 라인 그룹 객체를 반환합니다.
        return newLineGroup;
    }

    /// <summary>
    /// 랜덤한 색상을 반환하는 메서드
    /// </summary>
    /// <returns>ColorType 열거형 요소 중 랜덤한 요소를 반환합니다. </returns>
    private ColorType GetRandomColorType()
    {
        // 색상 타입 열거 형식을 배열로 얻습니다.
        ColorType[] colorTypeArray = System.Enum.GetValues(typeof(ColorType)) as ColorType[];

        // 랜덤한 요소를 반환
        return colorTypeArray[Random.Range(0,colorTypeArray.Length)];

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

    /// <summary>
    /// 플레이어가 라인을 통과한 경우 호출되는 메서드입니다.
    /// </summary>
    /// <param name="passedLineIndex">통과한 라인 인덱스가 전달됩니다.</param>
    /// <param name="nextColor">플레이어에게 설정 시킬 다음 색상이 전달됩니다.</param>
    private void OnLineGroupPassed(int passedLineIndex, ColorType nextColor)
    {
        // 라인 그룹 제거
        PopLineGroup(passedLineIndex);

        // 라인 그룹 추가
        PushLineGroup();

        // 플레이어의 색상을 설정합니다.
        SetPlayerColor(nextColor);
    }

    /// <summary>
    /// 라인 그룹을 배열에서 뺍니다.
    /// </summary>
    /// <param name="lineGroupIndex">배열에서 제외시킬 라인 그룹 인덱스를 전달합니다.</param>
    private void PopLineGroup(int lineGroupIndex)
    {
        // 통과한 라인 그룹 객체를 얻습니다.
        LineGroup passedLineGroup = _GeneratedLines[lineGroupIndex];

        // 통과한 라인 그룹을 배열에서 제외합니다.
        _GeneratedLines[lineGroupIndex] = null;

        // 빈 공간을 탐색할 for 문
        // 첫번쨰 요소부터 마지막 요소의 전까지 탐색합니다.
        for(int i = 0; i < _GeneratedLines.Length - 1; ++i) 
        {
            // 빈 공간을 발견한 경우
            if (_GeneratedLines[i] == null) 
            {
                // 가장 가까운 다음 요소를 찾고, 빈 공간과 위치를 교환합니다.
                for(int j = (i+1); j<_GeneratedLines.Length; ++j)
                {
                    // 비어있지 않은 라인 그룹 객체를 발견한 경우
                    if (_GeneratedLines[j] != null)
                    {
                        // 서로 위치를 교환합니다.
                        LineGroup tempLineGroup = _GeneratedLines[i];
                        _GeneratedLines[i] = _GeneratedLines[j];
                        _GeneratedLines[j] = tempLineGroup;

                        // 빈 공간과 위치를 교환한 라인 그룹의 인덱스를 설정합니다.
                        _GeneratedLines[i].SetLineGroupIndex(i);

                        // 빈공간과 교체가 끝났으므로 다음 교환을 진행합니다.
                        break;
                    }
                }
            }

        }

        // 통과시킨 라인 그룹 객체를 제거합니다.
        Destroy(passedLineGroup.gameObject);
    }

    /// <summary>
    /// 라인 그룹을 추가합니다.
    /// </summary>
    private void PushLineGroup()
    {
        // 배열의 빈 공간의 인덱스를 나타내기 위한 변수
        int emptyIndex = GetEmptyLineGroupIndex();

        // 배열에서 빈 공간을 찾지 못했다면 추가 취소
        if (emptyIndex == -1) return;

        // 라인 그룹을 생성합니다.
        GenerateLineGroup(emptyIndex);
    }

    /// <summary>
    /// 배열에서 라인 그룹을 생성할 수 있는 빈 요소 인덱스를 찾아 반환합니다.
    /// </summary>
    /// <returns>빈 요소의 인덱스를 반환합니다. 찾지 못한 경우 -1을 반환합니다.</returns>
    private int GetEmptyLineGroupIndex()
    {
        // 배열에서 빈공간을 찾습니다.
        // 보통 첫 번째 라인 그룹이 제거되고, PushLineGroup 메서드를 호출하게 됩니다.
        // 이 때 배열의 빈공간을 다른 그룹이 메꾸게 되며, 마지막 요소가 항상 비어있게 되므로
        // 마지막 요소부터 빈 공간을 탐색하도록 합니다.
        for (int i = (m_ColumnCount -1); (i>-1); --i)
        {
            // i 번째 요소가 비어있다면
            if (_GeneratedLines[i] == null)
            {
                // 빈 요소 인덱스를 반환
                return i;
            }
        }
        // 찾지 못한 경우 비정상적인 인덱스 반환
        return -1;
    }

    /// <summary>
    /// 플레이어의 색상을 변경합니다.
    /// </summary>
    /// <param name="newColorType">변경시킬 색상을 전달합니다.</param>
    private void SetPlayerColor(ColorType newColorType)
    {
        // 설정시킬 색상을 배열에서 얻습니다.
        Color newColor = m_Colors[(int)newColorType];

        // 플레이어의 색상을 설정합니다.
        m_Player.SetColor(newColor);
    }
}
