using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 유니티 인스펙터에서 씬 이름을 설정할 수 있도록 public 변수 선언
    public string sceneName;

    // 씬 이동을 처리하는 메서드
    public void LoadScene()
    {
        // 게임 일시정지 해제
        Time.timeScale = 1f;
        // 지정된 씬으로 이동
        SceneManager.LoadScene(sceneName);
    }
}
