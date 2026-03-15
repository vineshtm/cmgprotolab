using UnityEngine;

/// <summary>
/// Simple Util/Wrapper for Player Prefs
/// </summary>
public class PrefsUtil
{
    /// <summary>
    /// Get Game Prefs with Key
    /// </summary>
    /// <param name="Key">Key string</param>
    /// <returns>null as default</returns>
    public static string GetGamePrefs(string Key)
    {
        return PlayerPrefs.GetString(Key, null);
    }

    /// <summary>
    /// Set Game Prafs with Key
    /// </summary>
    /// <param name="Key">Key string</param>
    /// <param name="Value">String value/JSON to Save</param>
    public static void SetGamePrefs(string Key, string Value)
    {
        PlayerPrefs.SetString(Key, Value);
    }

    /// <summary>
    /// Clear Prefs data with spefic key
    /// </summary>
    /// <param name="Key"></param>
    public static void ClearData(string Key)
    {
        PlayerPrefs.DeleteKey(Key);
    }

    /// <summary>
    /// Clear All Prefs Data
    /// </summary>
    public static void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
