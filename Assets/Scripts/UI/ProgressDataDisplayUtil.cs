using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Util Class to Generate Game Data Item List
/// </summary>
public class ProgressDataDisplayUtil : MonoBehaviour
{
    /// <summary>
    /// Game Data Item Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_GameDataPrefab;

    /// <summary>
    /// Table Container Gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_GameDataTableContainer;

    /// <summary>
    /// List Holder Tranform
    /// </summary>
    [SerializeField]
    private RectTransform m_ListContainerTransform;

    /// <summary>
    /// Total Sessions text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TotalGameSessionsText;

    /// <summary>
    /// Highscore text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_HighscoreText;

    /// <summary>
    /// Generate ListView for the Game Data Item List
    /// </summary>
    /// <param name="TotalGameSessions"></param>
    /// <returns></returns>
    public List<GameObject> GenerateListView(int TotalGameSessions)
    {
        ClearGridLayout(); //Clear Existing List Table

        List<GameObject> GameDataItemList = new List<GameObject>();

        for (int i = 0; i < TotalGameSessions; i++)
        {
            GameObject card = Instantiate(m_GameDataPrefab, m_ListContainerTransform);

            GameDataItemList.Add(card);
        }

        m_GameDataTableContainer.SetActive(true); //Enable List View

        return GameDataItemList;
    }

    /// <summary>
    /// Set Total Game Session Text
    /// </summary>
    /// <param name="TotalGameSessions"></param>
    public void SetTotalGameSessionText(int TotalGameSessions)
    {
        m_TotalGameSessionsText.gameObject.SetActive(true);
        m_TotalGameSessionsText.alignment = TextAlignmentOptions.Left;
        m_TotalGameSessionsText.text = "Total Game Sessions : " + TotalGameSessions;
    }

    /// <summary>
    /// Set Highscore Text
    /// </summary>
    /// <param name="Highscore"></param>
    public void SetHighscoreText(int Highscore)
    {
        m_HighscoreText.gameObject.SetActive(true);
        m_HighscoreText.text = "Highest Score : " + Highscore;
    }

    /// <summary>
    /// Set Empty Data UI
    /// </summary>
    public void SetNoData()
    {
        m_HighscoreText.gameObject.SetActive(false);
        m_TotalGameSessionsText.text = "NO DATA AVAILABLE";
        m_TotalGameSessionsText.alignment = TextAlignmentOptions.Center;

        m_GameDataTableContainer.SetActive(false);
    }

    /// <summary>
    /// CLear List View
    /// </summary>
    private void ClearGridLayout()
    {
        for (int i = m_ListContainerTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_ListContainerTransform.GetChild(i).gameObject);
        }
    }
}
