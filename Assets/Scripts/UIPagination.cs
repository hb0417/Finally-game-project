using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPagination : MonoBehaviour
{
    public GameObject[] pages; // 페이지들
    public Button nextButton; // 다음 버튼
    public Button prevButton; // 이전 버튼
    private int currentPageIndex = 0; // 현재 페이지 인덱스

    void Start()
    {
        ShowPage(currentPageIndex);

        // 버튼 이벤트 할당
        nextButton.onClick.AddListener(ShowNextPage);
        prevButton.onClick.AddListener(ShowPrevPage);

        // 초기 상태 설정
        UpdateButtonVisibility();
    }

    void ShowPage(int pageIndex)
    {
        // 모든 페이지 숨기기
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        // 해당 인덱스의 페이지 표시
        pages[pageIndex].SetActive(true);
    }

    void ShowNextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
            UpdateButtonVisibility();
        }
    }

    void ShowPrevPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
            UpdateButtonVisibility();
        }
    }

    void UpdateButtonVisibility()
    {
        // 이전 버튼 활성화/비활성화
        prevButton.gameObject.SetActive(currentPageIndex > 0);
        // 다음 버튼 활성화/비활성화
        nextButton.gameObject.SetActive(currentPageIndex < pages.Length - 1);
    }
}
