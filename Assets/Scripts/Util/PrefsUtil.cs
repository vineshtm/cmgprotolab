using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsUtil
{
    public static string GetGamePrefs(string Key)
    {
        return PlayerPrefs.GetString(Key, null);
    }

    public static void SetGamePrefs(string Key, string Value)
    {
        PlayerPrefs.SetString(Key, Value);
    }

    public static void ClearData(string Key)
    {
        PlayerPrefs.DeleteKey(Key);
    }

    public static void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
