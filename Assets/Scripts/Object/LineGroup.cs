using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 라인 그룹 통과 확인을 위한 대리자 형식을 나타냅니다.
/// </summary>
/// <param name="passedLineIndex">통과한 라인 인덱스</param>
/// <param name="nextColor">플레이어에게 설정시킬 색상을 전달합니다.</param>
/// <param name="isGameOver">게임 오버 여부를 전달합니다.</param>
public delegate void OnLineGroupPassedSignature(int passedLineIndex, ColorType nextColor, bool isGameOver);

public class LineGroup : MonoBehaviour
{
    /// <summary>
    /// 라인 그룹 사이의 간격을 나타냅니다.
    /// </summary>
    private const float LINE_GROUP_TERM = 0.5f;

    /// <summary>
    /// 첫번째 라인 오브젝트의 위치를 나타냅니다.
    /// </summary>
    private static float _FirstLineObjectPosition;

    [Header("라인 오브젝트")]
    public LineObject[] m_LineObjects;

    [Header("스크롤 속력")]
    public float m_ScrollSpeed = 5.0f;

    /// <summary>
    /// 라인 그룹 인덱스를 나타냅니다.
    /// 가장 위에 위치한 그룹이 0번으로 사용됩니다.
    /// 해당 인덱스에 따라 라인 그룹이 배치되는 위치가 결정되도록 합니다.
    /// </summary>
    private int _LineGroupIndex;

    /// <summary>
    /// 라인 그룹 이동 속도
    /// </summary>
    private float _LineGroupMoveSpeed = 20.0f;

    /// <summary>
    /// 해당 라인 그룹에서 캐릭터와 충돌했을 때,
    /// 캐릭터가 통과할 수 있는 색상 타입을 나타냅니다.
    /// </summary>
    private ColorType _PassableColor;

    /// <summary>
    /// 플레이어가 이 라인 그룹을 통과했을 경우,
    /// 플레이어에게 설정시킬 색상을 나타냅니다.
    /// </summary>
    private ColorType _NextColor;

    /// <summary>
    /// 플레이어 객체를 나타냅니다.
    /// </summary>
    private Player _Player;

    /// <summary>
    /// 라인 그룹 통과 시 발생시킬 이벤트를 나타냅니다.
    /// </summary>
    private OnLineGroupPassedSignature _OnLineGroupPassed;

    private void Update()
    {
        //라인 그룹 위치 이동
        MoveLineGroup();

        // 라인 오브젝트 스크롤링
        ScrollingLine();
    }

    /// <summary>
    /// 라인 오브젝트를 스크롤링 시킵니다.
    /// </summary>
    private void ScrollingLine()
    {
        // 속도
        Vector3 velocity = Vector3.right * m_ScrollSpeed;

        // 모든 라인 오브젝트들의 위치를 오른쪽으로 m_ScrollSpeed 만큼씩 이동시킵니다.
        foreach (LineObject lineObject in m_LineObjects) 
        {
            // 라인 위치를 얻습니다.
            Vector2 linePosition = lineObject.transform.position;

            // 오른쪽으로 이동시킵니다.
            linePosition.x += m_ScrollSpeed * Time.deltaTime;

            // 라인 오브젝트의 위치 X가 8을 초과하면 맨 왼쪽으로 배치
            if (linePosition.x > 8) linePosition.x -= 20.0f;


            // 위치를 적용합니다.
            lineObject.transform.position = linePosition;
        }

        // 다른 라인과 위치를 동기화 시키기 위해
        // 첫 번째 라인 그룹인 경우에만 첫 번째 라인 오브젝트의 위치를 저장합니다.
        if(_LineGroupIndex == 0)
        {
            _FirstLineObjectPosition = m_LineObjects[0].transform.localPosition.x;
        }
    }

    /// <summary>
    /// 라인 그룹을 인덱스 값에 따라 이동시킵니다.
    /// </summary>
    private void MoveLineGroup()
    {
        // 목표 Y 위치를 계산합니다.
        float targetYPosition = (_LineGroupIndex * LINE_GROUP_TERM) + LINE_GROUP_TERM;

        // 목표 위치를 구합니다.
        Vector2 targetPosition = Vector2.down * targetYPosition;

        // 현재 위치를 얻습니다.
        Vector2 currentPosition = transform.position;

        // 목표 Y 위치로 부드럽게 이동시킵니다.
        transform.position = Vector2.Lerp(currentPosition, targetPosition, _LineGroupMoveSpeed * Time.deltaTime);
        // Vector2.Lerp(Vector2 a, Vector2 b, float t)
        // 선형 보간을 위한 함수입니다.
        // 선형 보간 : 두 점을 선형으로 연결하여 두 지점사이의
        // t 위치(0.0f = t <= 1.0f) 의 값을 구하는 방법입니다.


    }

    /// <summary>
    /// 라인 그룹 내의 라인 오브젝트들을 초기화합니다
    /// </summary>
    /// <param name="colors">사용 가능한 색상들을 전달합니다.</param>
    /// <param name="colorTypes">사용될 색상 타입들을 순서대로 전달합니다.</param>
    private void InitializeLineObject(Color[] colors, ColorType[] colorTypes)
    {
        // 두번째 라인 오브젝트의 위치
        float secondLineObjectXPos = m_LineObjects[1].transform.localPosition.x;
        // 첫번째 라인 오브젝트의 위치
        float firstLineObjectXPos = m_LineObjects[0].transform.localPosition.x;
        // 라인 오브젝트 둘 사이의 텀
        float lineObjectTerm = secondLineObjectXPos - firstLineObjectXPos;


        for(int i = 0; i < m_LineObjects.Length; ++i)
        {
            // i 번째 LineObject 를 얻습니다.
            LineObject lineObject = m_LineObjects[i];

            // 설정시킬 색상값을 얻습니다.
            ColorType colorType = colorTypes[i];

            // 설정시킬 색상값을 얻습니다.
            Color lineColor = colors[(int)colorType];

            // 라인 오브젝트 초기화
            lineObject.InitializeLineObject(
                // 캐릭터 겹침 메서드 전달
                OnCharacterOverlapped,
                // 설정시킬 색상을 전달합니다.
                lineColor :     lineColor,
                // 통과 가능 여부를 전달합니다.
                isPassable :    (_PassableColor == colorType));

            // 라인 오브젝트 위치 동기화
            lineObject.transform.localPosition = Vector3.right * (_FirstLineObjectPosition + i * lineObjectTerm);
        }
    }

    /// <summary>
    /// 라인 그룹을 초기화 합니다.
    /// </summary>
    /// <param name="player">플레이어 객체를 전달합니다.</param>
    /// <param name="index">설정시킬 인덱스를 전달합니다.</param>
    /// <param name="colors">사용될 색상 배열을 전달합니다.</param>
    /// <param name="colorTypes">사용될 색상 타입 배열을 전달합니다.</param>
    /// <param name="passableColor">플레이어 캐릭터를 통과시킬 색상 타입을 전달합니다</param>
    /// <param name="nextColor">플레이어가 통과된 후 플레이어에게 설정시킬 색상을 전달합니다.</param>
    /// <param name="onLineGroupPassed">라인 그룹 통과시 발생시킬 이벤트를 전달합니다.</param>
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

        // 라인 인덱스를 설정합니다.
        SetLineGroupIndex(index);

        // 라인 오브젝트들을 모두 초기화합니다.
        InitializeLineObject(colors, colorTypes);


    }

    /// <summary>
    /// 라인 그룹 인덱스를 설정합니다.
    /// </summary>
    /// <param name="newIndex">설정시킬 인덱스를 전달합니다.</param>
    public void SetLineGroupIndex(int newIndex)
    {
        _LineGroupIndex = newIndex;
    }

    /// <summary>
    /// 플레이어가 이 라인에 겹쳤을 때 호출되는 메서드입니다.
    /// </summary>
    /// <param name="isPassable"></param>
    private void OnCharacterOverlapped(bool isPassable)
    {
        // 통과 가능한 오브젝트인 경우
        if(isPassable)
        {
            // 라인 그룹의 위치를 얻습니다.
            Vector2 groupPosition = transform.position;

            // 설정시킬 Y 위치를 계산합니다.
            float newYPosition = groupPosition.y + 1.0f;

            // 플레이어 위치를 조절
            Vector2 playerPosition = _Player.transform.position;
            playerPosition.y = newYPosition;

            // 계산한 위치를 적용시킵니다.
            _Player.transform.position = playerPosition;

            // 플레이어 점프
            _Player.Jump();
            
        }
        // 통과 불가능한 오브젝트인 경우
        else
        {
            Time.timeScale = 0.0f;
        }

        // 라인 통과 이벤트를 발생시킵니다.
        _OnLineGroupPassed?.Invoke(_LineGroupIndex, _NextColor, isPassable);

    }

}
