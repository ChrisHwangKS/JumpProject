using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("�÷��̾�")]
    public Player m_Player;

    [Header("���� �ؽ�Ʈ")]
    public TMP_Text m_ScoreText;

    [Header("��ġ �� ���� �ؽ�Ʈ")]
    public TMP_Text m_TouchToPlay;

    [Header("���� ȭ������ �̵� ��ư")]
    public Button m_GotoMainButton;

    [Header("���� �ٽ� ���� ��ư")]
    public Button m_RestartButton;

    private void Awake()
    {
        // ���� ���� �̺�Ʈ�� ����մϴ�.
        m_Player.onGameStarted += OnGameStarted;

        // ���� ���� �̺�Ʈ�� ����մϴ�.
        m_Player.onScoreChanged += OnScoreChanged;

        // ���� ���� �̺�Ʈ�� ����մϴ�.
        m_Player.onGameFinished += OnGameOver;

        // ��ư �̺�Ʈ ���ε�
        m_GotoMainButton.onClick.AddListener(OnGotoMainButtonClicked);
        m_RestartButton.onClick.AddListener(OnRestartButtonClicked);

        // ��ư ��Ȱ��ȭ
        m_GotoMainButton.gameObject.SetActive(false);
        m_RestartButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// ������ ����Ǿ��� ��� ȣ��Ǵ� �޼����Դϴ�,
    /// </summary>
    /// <param name="score">������ ���޵˴ϴ�.</param>
    private void OnScoreChanged(int score)
    {
        m_ScoreText.text= score.ToString();
    }
    
    /// <summary>
    /// ���� ���� �� ȣ��Ǵ� �޼���
    /// </summary>
    private void OnGameStarted()
    {
        m_TouchToPlay.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ȭ������ ��ư Ŭ���� ȣ��˴ϴ�.
    /// </summary>
    private void OnGotoMainButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// ���� �ٽ� ���� ��ư Ŭ�� �� ȣ��˴ϴ�.
    /// </summary>
    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// ���� ���� �� ��ư�� Ȱ��ȭ �մϴ�.
    /// </summary>
    private void OnGameOver()
    {
        // ��ư Ȱ��ȭ
        m_GotoMainButton.gameObject.SetActive(true);
        m_RestartButton.gameObject.SetActive(true);

    }

}
