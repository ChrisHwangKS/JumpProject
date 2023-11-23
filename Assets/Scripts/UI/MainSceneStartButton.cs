using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 메인 씬의 게임 시작 버튼에 추가될 컴포넌트입니다.
/// </summary>
public class MainSceneStartButton : MonoBehaviour
{
    /// <summary>
    /// StartButton 클릭시 호출될 메서드
    /// </summary>
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
