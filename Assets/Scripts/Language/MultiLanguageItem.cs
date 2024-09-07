using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LanguageItem<T>
{
    public Language language;
    public T value;

    public LanguageItem(Language language, T value)
    {
        this.language = language;
        this.value = value;
    }
}


[Serializable]
public class MultiLanguageItem<T>
{
    [HideInInspector]
    public LanguageManager languageManager;

    public T currentTranslation;

    public List<LanguageItem<T>> translations = new();

    public Dictionary<Language, T> translationIn = new();

    public void Update()
    {
        if (languageManager == null) { return; }

        Update(languageManager.currentLanguage);
    }

    public void Update(Language language)
    {
        if (translationIn.ContainsKey(language))
        { currentTranslation = translationIn[language]; }

        else if (translationIn.ContainsKey(Language.Default))
        { currentTranslation = translationIn[Language.Default]; }
    }

    public void SyncTranslation()
    {
        foreach (LanguageItem<T> item in translations)
        { translationIn[item.language] = item.value; }
    }

    public T GetTranslationIn(Language language) { return translationIn[language]; }
    public T GetTranslationIn(string language) { return translationIn[(Language)Enum.Parse(typeof(Language), language)]; }

    public void AddTranslation(Language language, T value) { AddTranslation(new LanguageItem<T>(language, value)); }
    public void AddTranslation(LanguageItem<T> item)
    {
        translations.Add(item);
        translationIn[item.language] = item.value;
    }
    public void AddTranslations(LanguageItem<T>[] items)
    {
        foreach (LanguageItem<T> item in items)
        { translations.Add(item); }

        SyncTranslation();
    }
}

