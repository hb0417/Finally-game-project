using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ����Ƽ �ν����Ϳ��� �� �̸��� ������ �� �ֵ��� public ���� ����
    public string sceneName;

    // �� �̵��� ó���ϴ� �޼���
    public void LoadScene()
    {
        // ���� �Ͻ����� ����
        Time.timeScale = 1f;
        // ������ ������ �̵�
        SceneManager.LoadScene(sceneName);
    }
}
