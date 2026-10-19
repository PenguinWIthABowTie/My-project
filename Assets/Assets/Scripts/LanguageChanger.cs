using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChanger : MonoBehaviour
{
    public void SetEnglish()
    {
        SetLanguage("en");
    }

    public void SetSpanish()
    {
        SetLanguage("es");
    }

    public void SetUkrainan()
    {
        SetLanguage("uk");
    }

    private void SetLanguage(string localeCode)
    {
        
        var locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);

        Debug.Log(locale);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
        else
        {
            Debug.Log("Locale not found:" + localeCode);
        }
    }


}
