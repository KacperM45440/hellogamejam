using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _active = false;

    [SerializeField] private TMP_FontAsset[] fonts; 

    [SerializeField] private TextMeshProUGUI[] texts;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey");
        ChangeLocale(ID);
    }

    public void ChangeLocale(int localeID)
    {
        if (_active)
        {
            return;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].font = fonts[localeID];
        }

        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        PlayerPrefs.SetInt("LocaleKey", localeID);
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        _active = false;
    }
}