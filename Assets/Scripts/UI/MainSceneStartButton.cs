using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ���� ���� ��ư�� �߰��� ������Ʈ�Դϴ�.
/// </summary>
public class MainSceneStartButton : MonoBehaviour
{
    /// <summary>
    /// StartButton Ŭ���� ȣ��� �޼���
    /// </summary>
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
