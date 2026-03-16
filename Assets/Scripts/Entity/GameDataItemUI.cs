using UnityEngine;
using TMPro;

/// <summary>
/// UI Data Setup for Game Data Prefab Entity
/// </summary>
public class GameDataItemUI : MonoBehaviour
{
    /// <summary>
    /// Sl No
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_SlNoText;

    /// <summary>
    /// Level data Text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_LevelText;

    /// <summary>
    /// Text with Time Data
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TimeText;

    /// <summary>
    /// Text with Score Data
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    /// <summary>
    /// Set Game Data Item Details
    /// </summary>
    /// <param name="No"></param>
    /// <param name="GameDataItem"></param>
    public void SetGameData(int No, GameData GameDataItem)
    {
        m_SlNoText.text = "" + No;
        m_LevelText.text = "" + ((DifficultyLevel)GameDataItem.DifficultyLevelIndex);
        m_TimeText.text = GameDataItem.Time;
        m_ScoreText.text = "" + GameDataItem.Score;
    }
}
