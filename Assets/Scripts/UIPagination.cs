using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPagination : MonoBehaviour
{
    public GameObject[] pages; // ��������
    public Button nextButton; // ���� ��ư
    public Button prevButton; // ���� ��ư
    private int currentPageIndex = 0; // ���� ������ �ε���

    void Start()
    {
        ShowPage(currentPageIndex);

        // ��ư �̺�Ʈ �Ҵ�
        nextButton.onClick.AddListener(ShowNextPage);
        prevButton.onClick.AddListener(ShowPrevPage);

        // �ʱ� ���� ����
        UpdateButtonVisibility();
    }

    void ShowPage(int pageIndex)
    {
        // ��� ������ �����
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        // �ش� �ε����� ������ ǥ��
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
        // ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        prevButton.gameObject.SetActive(currentPageIndex > 0);
        // ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        nextButton.gameObject.SetActive(currentPageIndex < pages.Length - 1);
    }
}
