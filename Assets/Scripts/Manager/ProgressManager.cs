using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public void SaveCurrentGameData(GameData CurrentGameData)
    {
        ProgressData CurrentProgressData = LoadProgress();

        if (CurrentProgressData == null)
            CurrentProgressData = new ProgressData();

        if (CurrentProgressData.GameDetailList == null)
            CurrentProgressData.GameDetailList = new List<GameData>();

        CurrentProgressData.GameDetailList.Add(CurrentGameData);

        CurrentProgressData.TotalGameSessions++;
        CurrentProgressData.HighestScore = GetHighScore(CurrentProgressData.GameDetailList);

        SaveProgressDataToPrefs(CurrentProgressData);

#if UNITY_EDITOR
        LogProgressDataOnEditorConsole();
#endif

    }

    public ProgressData LoadProgress()
    {
        return GetPrefsProgressData();
    }

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

#if UNITY_EDITOR

    private void LogProgressDataOnEditorConsole()
    {
        ProgressData CurrentProgressData = LoadProgress();

        if (CurrentProgressData != null)
        {
            List<GameData> GameDataList = CurrentProgressData.GameDetailList;

            string GameDataUnitystring = "";

            for (int i = 0; i < GameDataList.Count; i++)
            {
                GameDataUnitystring += "-" + i + ".===Score:"
                + GameDataList[i]?.Score + "   ==Attempts:"
                + GameDataList[i]?.Attempts + "====\n";
            }

            Debug.Log("==========CURRENT PROGRESS==============" + "\n"
                + "Total Game Sessions : " + CurrentProgressData.TotalGameSessions + "===="
                + "Highest Score : " + CurrentProgressData.HighestScore + "====\n"
                + GameDataUnitystring + "\n" +
                "========================================");
        }
        else
        {
            Debug.Log("======NO PROGRESSDATA===========");
        }
    }

#endif


    //PREFS MANABER

    private const string PREF_PROGRESSDATA = "App_ProgressData";

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
}
