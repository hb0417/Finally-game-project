using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



// �ؽ�Ʈ �����̴� ��ũ��Ʈ �Դϴ�.
public class BlinkingText : MonoBehaviour
{
    public TMP_Text blinkingText;
    public float blinkDuration = 1.0f; // �����̴� �� �ð� (��)

    private void Start()
    {
        if (blinkingText == null)
        {
            blinkingText = GetComponent<TMP_Text>();
        }
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // �ؽ�Ʈ�� ������鼭 ������� �ִϸ��̼�
            yield return StartCoroutine(FadeTextToZeroAlpha(blinkDuration / 2));
            // �ؽ�Ʈ�� �������鼭 ��Ÿ���� �ִϸ��̼�
            yield return StartCoroutine(FadeTextToFullAlpha(blinkDuration / 2));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t)
    {
        blinkingText.color = new Color(blinkingText.color.r, blinkingText.color.g, blinkingText.color.b, 0);
        while (blinkingText.color.a < 1.0f)
        {
            blinkingText.color = new Color(blinkingText.color.r, blinkingText.color.g, blinkingText.color.b, blinkingText.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        blinkingText.color = new Color(blinkingText.color.r, blinkingText.color.g, blinkingText.color.b, 1);
        while (blinkingText.color.a > 0.0f)
        {
            blinkingText.color = new Color(blinkingText.color.r, blinkingText.color.g, blinkingText.color.b, blinkingText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
