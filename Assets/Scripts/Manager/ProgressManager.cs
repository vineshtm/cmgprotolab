using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the Save/Load of Game Progress
/// </summary>
public class ProgressManager : MonoBehaviour
{
    /// <summary>
    /// Updates the Progress
    /// Gets the Existing Progress Data
    /// Update with the Currrent Game Deatils and save it back to the Prefs
    /// </summary>
    /// <param name="CurrentGameData"></param>
    public void SaveCurrentGameData(GameData CurrentGameData)
    {
        ProgressData CurrentProgressData = LoadProgress(); //Get Existing Progres data

        if (CurrentProgressData == null)
            CurrentProgressData = new ProgressData();

        if (CurrentProgressData.GameDetailList == null)
            CurrentProgressData.GameDetailList = new List<GameData>();

        CurrentProgressData.GameDetailList.Add(CurrentGameData); //Add the current data to 

        CurrentProgressData.TotalGameSessions++; //increement total game session
        CurrentProgressData.HighestScore = GetHighScore(CurrentProgressData.GameDetailList); //Highest Score across all levels

        SaveProgressDataToPrefs(CurrentProgressData);
    }

    /// <summary>
    /// Load Game Progress Data
    /// </summary>
    /// <returns></returns>
    public ProgressData LoadProgress()
    {
        return GetPrefsProgressData();
    }

    /// <summary>
    /// Clear All Progress Data
    /// </summary>
    public void ClearAllProgressData()
    {
        ClearPrefsProgressData();
    }

    /// <summary>
    /// Get the Highest Score from all the List of Games
    /// Checks for the Scores of each game and Gets the highest of All.
    /// Highest Score can be based on each level because highest across levels/layouts does not make much sense.
    /// The fact that highest scorer will be finally, anyways with the biggest layouts.
    /// </summary>
    /// <param name="GameDataList">List of Games</param>
    /// <returns></returns>
    private int GetHighScore(List<GameData> GameDataList)
    {
        int HighScore = 0;

        for (int i = 0; i < GameDataList.Count; i++)
        {
            int score = GameDataList[i].Score;
            if (score > HighScore)
            {
                HighScore = score;
            }
        }

        return HighScore;
    }


    //PREFS MANAGER

    /// <summary>
    /// Prefs Key for the Progress Data JSON
    /// </summary>
    private const string PREF_PROGRESSDATA = "App_ProgressData";

    /// <summary>
    /// Save the Progress Data Details to the Prefs
    /// </summary>
    /// <param name="progress"></param>
    private void SaveProgressDataToPrefs(ProgressData progress)
    {
        string ProgressDataJson = null;

        try
        {
            ProgressDataJson = JsonUtility.ToJson(progress);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.Log("==SaveProgressDataToPrefs====Exception===" + ex.Message);
#endif
        }

        if (ProgressDataJson != null)
            PrefsUtil.SetGamePrefs(PREF_PROGRESSDATA, ProgressDataJson);
    }

    private ProgressData GetPrefsProgressData()
    {
        ProgressData Data = null;
        string ProgressDataJson = PrefsUtil.GetGamePrefs(PREF_PROGRESSDATA);
        try
        {
            Data = JsonUtility.FromJson<ProgressData>(ProgressDataJson);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.Log("==GetPrefsProgressData====EXCEPTION===" + ex.Message);
#endif
        }

        return Data;
    }

    /// <summary>
    /// Clear All Progress Data from the Prefs
    /// </summary>
    private void ClearPrefsProgressData()
    {
        PrefsUtil.ClearData(PREF_PROGRESSDATA);
    }
}
