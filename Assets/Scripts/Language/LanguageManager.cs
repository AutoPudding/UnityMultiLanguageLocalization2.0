using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum Language
{
    Default,
    English_US,           // English_US
    English_UK,           // English_UK
    Français,             // French
    Deutsch,              // German
    Español,              // Spanish
    Italiano,             // Italian
    Nederlands,           // Dutch
    Polski,               // Polish
    Português,            // Portuguese
    Svenska,              // Swedish
    Dansk,                // Danish
    Suomi,                // Finnish
    Norsk,                // Norwegian
    Bahasa_Melayu,        // Malay
    Filipino,             // Filipino
    Khmer,                // Khmer
    Română,               // Romanian
    Ελληνικά,             // Greek
    Українська,           // Ukrainian
    Magyar,               // Hungarian
    Български,            // Bulgarian
    Српски,               // Serbian
    Hrvatski,             // Croatian
    Türkçe,               // Turkish
    Русский,              // Russian
    Қазақша,              // Kazakh
    Oʻzbekcha,            // Uzbek
    Bahasa_Indonesia,     // Indonesian
    Tiếng_Việt,           // Vietnamese
    简体中文,              // Simplified Chinese
    繁體中文,              // Traditional Chinese
    日本語,                // Japanese
    한국어,                // Korean
    हिन्दी,                  // Hindi
    ไทย,                  // Thai
    العربية,              // Arabic
    עברית,                // Hebrew
    فارسی,                // Persian (Farsi)
    မြန်မာစာ,               // Burmese
    Swahili,              // Swahili
    Afrikaans,            // Afrikaans
    Amharic,              // Amharic
    Hausa,                // Hausa
    Other
}

public class LanguageManager : MonoBehaviour
{
    public Language currentLanguage = Language.English_US;

    public List<MultiLanguageItem<string>> multiLanguageTexts = new();
    public List<MultiLanguageItem<Sprite>> multiLanguageSprites = new();
    public List<MultiLanguageItem<AudioClip>> multiLanguageAudioClips = new();
    public List<MultiLanguageItem<GameObject>> multiLanguageObjects = new();

    public UnityEvent onLanguageChange = new();


    private void Awake()
    {
        multiLanguageTexts = multiLanguageTexts.Where(item => item != null).ToList();
        multiLanguageSprites = multiLanguageSprites.Where(item => item != null).ToList();
        multiLanguageAudioClips = multiLanguageAudioClips.Where(item => item != null).ToList();
        multiLanguageObjects = multiLanguageObjects.Where(item => item != null).ToList();

        foreach (MultiLanguageItem<string> mutiLanguageText in multiLanguageTexts)
        { mutiLanguageText.languageManager = this; }
        foreach (MultiLanguageItem<Sprite> mutiLanguageSprite in multiLanguageSprites)
        { mutiLanguageSprite.languageManager = this; }
        foreach (MultiLanguageItem<AudioClip> mutiLanguageAudioClip in multiLanguageAudioClips)
        { mutiLanguageAudioClip.languageManager = this; }
        foreach (MultiLanguageItem<GameObject> mutiLanguageObject in multiLanguageObjects)
        { mutiLanguageObject.languageManager = this; }

        SyncTranslations();
    }

    private void Start()
    {
        UpdateLanguage();
    }

    public void SyncTranslations()
    {
        foreach (var item in multiLanguageTexts) { item.SyncTranslation(); }
        foreach (var item in multiLanguageSprites) { item.SyncTranslation(); }
        foreach (var item in multiLanguageAudioClips) { item.SyncTranslation(); }
        foreach (var item in multiLanguageObjects) { item.SyncTranslation(); }
    }


    // Switch
    public void SwitchLanguage(Language language)
    {
        currentLanguage = language;
        UpdateLanguage();
    }

    public void SwitchLanguage(int index) { SwitchLanguage((Language)index); }
    public void SwitchLanguage(string language) { SwitchLanguage((Language)Enum.Parse(typeof(Language), language)); }


    //Update
    public void UpdateLanguage()
    {
        multiLanguageTexts = multiLanguageTexts.Where(item => item != null).ToList();
        multiLanguageSprites = multiLanguageSprites.Where(item => item != null).ToList();
        multiLanguageAudioClips = multiLanguageAudioClips.Where(item => item != null).ToList();
        multiLanguageObjects = multiLanguageObjects.Where(item => item != null).ToList();

        foreach (MultiLanguageItem<string> mutiLanguageText in multiLanguageTexts)
        {
            if (mutiLanguageText == null) continue;
            mutiLanguageText.Update(currentLanguage);
        }
        foreach (MultiLanguageItem<Sprite> mutiLanguageSprite in multiLanguageSprites)
        {
            if (mutiLanguageSprite == null) continue;
            mutiLanguageSprite.Update(currentLanguage);
        }
        foreach (MultiLanguageItem<AudioClip> mutiLanguageAudioClip in multiLanguageAudioClips)
        {
            if (mutiLanguageAudioClip == null) continue;
            mutiLanguageAudioClip.Update(currentLanguage);
        }
        foreach (MultiLanguageItem<GameObject> mutiLanguageObject in multiLanguageObjects)
        {
            if (mutiLanguageObject == null) continue;
            mutiLanguageObject.Update(currentLanguage);
        }

        onLanguageChange.Invoke();

        Debug.Log("Update Language:" + currentLanguage.ToString());
    }


    //Register
    public void Register(MultiLanguageItem<string> mutiLanguageText)
    {
        if (mutiLanguageText == null) { return; }

        if (!multiLanguageTexts.Contains(mutiLanguageText))
        { multiLanguageTexts.Add(mutiLanguageText); }

        mutiLanguageText.languageManager = this;
        //mutiLanguageText.SyncTranslation();
        mutiLanguageText.Update();
    }
    public void Register(MultiLanguageItem<Sprite> mutiLanguageSprite)
    {
        if (mutiLanguageSprite == null) { return; }

        if (!multiLanguageSprites.Contains(mutiLanguageSprite))
        { multiLanguageSprites.Add(mutiLanguageSprite); }

        mutiLanguageSprite.languageManager = this;
        //mutiLanguageSprite.SyncTranslation();
        mutiLanguageSprite.Update();
    }
    public void Register(MultiLanguageItem<AudioClip> mutiLanguageAudioClip)
    {
        if (mutiLanguageAudioClip == null) { return; }

        if (!multiLanguageAudioClips.Contains(mutiLanguageAudioClip))
        { multiLanguageAudioClips.Add(mutiLanguageAudioClip); }

        mutiLanguageAudioClip.languageManager = this;
        //mutiLanguageAudioClip.SyncTranslation();
        mutiLanguageAudioClip.Update();
    }
    public void Register(MultiLanguageItem<GameObject> mutiLanguageObject)
    {
        if (mutiLanguageObject == null) { return; }

        if (!multiLanguageObjects.Contains(mutiLanguageObject))
        { multiLanguageObjects.Add(mutiLanguageObject); }

        mutiLanguageObject.languageManager = this;
        //mutiLanguageObject.SyncTranslation();
        mutiLanguageObject.Update();
    }

    //Remove
    public void Remove(MultiLanguageItem<string> mutiLanguageText)
    {
        if (mutiLanguageText == null
        || !multiLanguageTexts.Contains(mutiLanguageText))
        { return; }

        multiLanguageTexts.Remove(mutiLanguageText);
        mutiLanguageText.languageManager = null;
    }
    public void Remove(MultiLanguageItem<Sprite> mutiLanguageSprite)
    {
        if (mutiLanguageSprite == null
        || !multiLanguageSprites.Contains(mutiLanguageSprite))
        { return; }

        multiLanguageSprites.Remove(mutiLanguageSprite);
        mutiLanguageSprite.languageManager = null;
    }
    public void Remove(MultiLanguageItem<AudioClip> mutiLanguageAudioClip)
    {
        if (mutiLanguageAudioClip == null
        || !multiLanguageAudioClips.Contains(mutiLanguageAudioClip))
        { return; }

        multiLanguageAudioClips.Remove(mutiLanguageAudioClip);
        mutiLanguageAudioClip.languageManager = null;
    }
    public void Remove(MultiLanguageItem<GameObject> mutiLanguageObject)
    {
        if (mutiLanguageObject == null
        || !multiLanguageObjects.Contains(mutiLanguageObject))
        { return; }

        multiLanguageObjects.Remove(mutiLanguageObject);
        mutiLanguageObject.languageManager = null;
    }


}
