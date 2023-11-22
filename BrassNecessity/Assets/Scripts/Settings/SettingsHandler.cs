using UnityEngine;

public static class SettingsHandler
{
    private const int MAX_SETTING_VALUE = 10;
    private const int DEFAULT_VALUE = 5;

    public static int SensitivitySetting { get; set; }
    public static int BrightnessSetting { get; set; }
    public static int MusicVolumeSetting { get; set; }
    public static int EffectsVolumeSetting { get; set; }
    public static int SelectedCharacterId { get; private set; }
    private static int numberOfCharacters = 2;


    private static readonly string SENSITIVITY_KEY = "Sensitivity";
    private static readonly string BRIGHTNESS_KEY = "Brightness";
    private static readonly string MUSIC_VOLUME_KEY = "MusicVolume";
    private static readonly string EFFECTS_VOLUME_KEY = "EffectsVolume";
    private static readonly string HAS_READ_CONTROLS_KEY = "HasReadControls";

    public static void SaveSettings()
    {
        Debug.Log("Saving settings");
        saveSetting(SENSITIVITY_KEY, SensitivitySetting);
        saveSetting(BRIGHTNESS_KEY, BrightnessSetting);
        saveSetting(EFFECTS_VOLUME_KEY, EffectsVolumeSetting);
        saveSetting(MUSIC_VOLUME_KEY, MusicVolumeSetting);
    }

    private static void saveSetting(string settingKey, int settingValue)
    {
        PlayerPrefs.SetInt(settingKey, settingValue);
    }

    public static void LoadSettings()
    {
        SensitivitySetting = loadSettingFromPrefs(SENSITIVITY_KEY);
        BrightnessSetting = loadSettingFromPrefs(BRIGHTNESS_KEY);
        MusicVolumeSetting = loadSettingFromPrefs(MUSIC_VOLUME_KEY);
        EffectsVolumeSetting = loadSettingFromPrefs(EFFECTS_VOLUME_KEY);
    }

    private static int loadSettingFromPrefs(string settingKey)
    {
        int settingValue;
        if (PlayerPrefs.HasKey(settingKey))
        {
            settingValue = PlayerPrefs.GetInt(settingKey);
        }
        else
        {
            settingValue = DEFAULT_VALUE;
        }
        return settingValue;
    }

    public static bool GetHasReadControls()
    {
        bool hasRead = false;
        if (PlayerPrefs.HasKey(HAS_READ_CONTROLS_KEY))
        {
            int readValue = PlayerPrefs.GetInt(HAS_READ_CONTROLS_KEY);
            if (readValue == 1)
            {
                hasRead = true;
            }
        }
        return hasRead;
    }

    public static void SetHasReadControls(bool hasRead)
    {
        int value = 0;
        if (hasRead)
        {
            value = 1;
        }
        PlayerPrefs.SetInt(HAS_READ_CONTROLS_KEY, value);
    }

    public static void SetSelectCharacterId(int characterId)
    {
        if (characterId >= numberOfCharacters)
        {
            characterId = 0;
        }
        SelectedCharacterId = characterId;
    }

    public static float GetSensitivityFraction()
    {
        return getSettingFraction(SensitivitySetting);
    }

    public static float GetBrightnessFraction()
    {
        return getSettingFraction(BrightnessSetting);
    }

    public static float GetMusicVolumeFraction()
    {
        return getSettingFraction(MusicVolumeSetting);
    }

    public static float GetEffectVolumeFraction()
    {
        return getSettingFraction(EffectsVolumeSetting);
    }

    private static float getSettingFraction(int value)
    {
        return (float)value / (float)MAX_SETTING_VALUE;
    }
}
