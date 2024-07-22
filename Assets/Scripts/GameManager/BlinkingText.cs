using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



// 텍스트 깜박이는 스크립트 입니다.
public class BlinkingText : MonoBehaviour
{
    public TMP_Text blinkingText;
    public float blinkDuration = 1.0f; // 깜박이는 총 시간 (초)

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
            // 텍스트가 흐려지면서 사라지는 애니메이션
            yield return StartCoroutine(FadeTextToZeroAlpha(blinkDuration / 2));
            // 텍스트가 진해지면서 나타나는 애니메이션
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
