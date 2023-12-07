using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("플레이어")]
    public Player m_Player;

    [Header("점수 텍스트")]
    public TMP_Text m_ScoreText;

    [Header("터치 시 시작 텍스트")]
    public TMP_Text m_TouchToPlay;

    [Header("메인 화면으로 이동 버튼")]
    public Button m_GotoMainButton;

    [Header("게임 다시 시작 버튼")]
    public Button m_RestartButton;

    private void Awake()
    {
        // 게임 시작 이벤트를 등록합니다.
        m_Player.onGameStarted += OnGameStarted;

        // 점수 변경 이벤트를 등록합니다.
        m_Player.onScoreChanged += OnScoreChanged;

        // 게임 오버 이벤트를 등록합니다.
        m_Player.onGameFinished += OnGameOver;

        // 버튼 이벤트 바인딩
        m_GotoMainButton.onClick.AddListener(OnGotoMainButtonClicked);
        m_RestartButton.onClick.AddListener(OnRestartButtonClicked);

        // 버튼 비활성화
        m_GotoMainButton.gameObject.SetActive(false);
        m_RestartButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 점수가 변경되었을 경우 호출되는 메서드입니다,
    /// </summary>
    /// <param name="score">점수가 전달됩니다.</param>
    private void OnScoreChanged(int score)
    {
        m_ScoreText.text= score.ToString();
    }
    
    /// <summary>
    /// 게임 시작 시 호출되는 메서드
    /// </summary>
    private void OnGameStarted()
    {
        m_TouchToPlay.gameObject.SetActive(false);
    }

    /// <summary>
    /// 메인 화면으로 버튼 클릭시 호출됩니다.
    /// </summary>
    private void OnGotoMainButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// 게임 다시 시작 버튼 클릭 시 호출됩니다.
    /// </summary>
    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// 게임 오버 시 버튼을 활성화 합니다.
    /// </summary>
    private void OnGameOver()
    {
        // 버튼 활성화
        m_GotoMainButton.gameObject.SetActive(true);
        m_RestartButton.gameObject.SetActive(true);

    }

}
