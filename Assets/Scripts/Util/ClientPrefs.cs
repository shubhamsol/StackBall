using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClientPrefs 
{
    const string _levelProgress     = "LevelProgress";
    const string _lives             = "Lives";
    const string _maxLevelReached   = "MaxLevelReached";

    public static int defaultLives          = 2;

    public static void SetLevelProgress(int level)
    {
        PlayerPrefs.SetInt(_levelProgress, level);
        if(PlayerPrefs.HasKey(_maxLevelReached))
        {
            if(PlayerPrefs.GetInt(_maxLevelReached)<level)
            {
                PlayerPrefs.SetInt(_maxLevelReached, level);
            }
        }
        else
            PlayerPrefs.SetInt(_maxLevelReached, level);
    }
    public static int GetMaxLevelReached()
    {
        if (PlayerPrefs.HasKey(_maxLevelReached))
            return PlayerPrefs.GetInt(_maxLevelReached);
        return 0;
    }
    public static int GetLevelProgress()
    {
        if(PlayerPrefs.HasKey(_levelProgress))
            return PlayerPrefs.GetInt(_levelProgress);
        return 0;   
    }
    public static int GetLives()
    {
        if (PlayerPrefs.HasKey(_lives))
            return PlayerPrefs.GetInt(_lives);
        return defaultLives;
    }
    public static void SetLives(int lives)
    {
        PlayerPrefs.SetInt(_lives, lives);
    }
}
