using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LocalizationDataType
{
    Text,
    TextMesh,
    TextMeshPro,
    TextMeshProUGUI,
    Dropdown,
    DropdownTextMeshPro,
    Image,
    SpriteRenderer,
    AudioSource
}

[Serializable]
public class MultiLanguageOption
{
    public MultiLanguageItem<string> text;
    public MultiLanguageItem<Sprite> sprite;

    public MultiLanguageOption(MultiLanguageItem<string> mutiLanguagetext, MultiLanguageItem<Sprite> mutiLanguagesprite)
    {
        this.text = mutiLanguagetext;
        this.sprite = mutiLanguagesprite;
    }
}

public class Localization : MonoBehaviour
{
    public LanguageManager languageManager;

    public LocalizationDataType dataType;

    //Text
    public Text text;
    private TextMesh textMesh;
    private TextMeshPro textMeshPro;
    private TextMeshProUGUI textMeshProUGUI;
    public MultiLanguageItem<string> multiLanguageText;

    //Text + Sprite
    private Dropdown dropdown;
    private TMP_Dropdown tmpDropdown;
    public MultiLanguageOption[] multiLanguageOptions;

    //Sprite
    private Image image;
    private SpriteRenderer spriteRenderer;
    public MultiLanguageItem<Sprite> multiLanguageSprite;

    //AduioClip
    private AudioSource audioSource;
    public MultiLanguageItem<AudioClip> multiLanguageAudioClip;

    private void Awake()
    {
        SyncTranslations();

        if (!FindLanguageManager())
        { Debug.LogError("No LanguageManagemnt"); }

        switch (dataType)
        {
            case LocalizationDataType.Text:
                text = GetComponent<Text>();
                languageManager.Register(multiLanguageText);
                break;
            case LocalizationDataType.TextMesh:
                textMesh = GetComponent<TextMesh>();
                languageManager.Register(multiLanguageText);
                break;
            case LocalizationDataType.TextMeshPro:
                textMeshPro = GetComponent<TextMeshPro>();
                languageManager.Register(multiLanguageText);
                break;
            case LocalizationDataType.TextMeshProUGUI:
                textMeshProUGUI = GetComponent<TextMeshProUGUI>();
                languageManager.Register(multiLanguageText);
                break;
            case LocalizationDataType.Dropdown:
                dropdown = GetComponent<Dropdown>();
                foreach (MultiLanguageOption option in multiLanguageOptions)
                {
                    languageManager.Register(option.text);
                    languageManager.Register(option.sprite);
                }
                break;
            case LocalizationDataType.DropdownTextMeshPro:
                tmpDropdown = GetComponent<TMP_Dropdown>();
                foreach (MultiLanguageOption option in multiLanguageOptions)
                {
                    languageManager.Register(option.text);
                    languageManager.Register(option.sprite);
                }
                break;
            case LocalizationDataType.Image:
                image = GetComponent<Image>();
                languageManager.Register(multiLanguageSprite);
                break;
            case LocalizationDataType.SpriteRenderer:
                spriteRenderer = GetComponent<SpriteRenderer>();
                languageManager.Register(multiLanguageSprite);
                break;
            case LocalizationDataType.AudioSource:
                audioSource = GetComponent<AudioSource>();
                languageManager.Register(multiLanguageAudioClip);
                break;
            default: break;
        }

        languageManager.onLanguageChange.AddListener(UpdateLocalization);
    }

    private void OnEnable()
    {
        UpdateLocalization();
    }

    private void OnDestroy()
    {
        RemoveLocalization();
    }

    public bool FindLanguageManager()
    {
        if (languageManager == null)
        {
            if (GameObject.Find("LanguageManager").GetComponent<LanguageManager>() != null)
            {
                languageManager = GameObject.Find("LanguageManager").GetComponent<LanguageManager>();
                return true;
            }
            else
            { return false; }
        }

        return true;
    }

    public void SyncTranslations()
    {
        multiLanguageText.SyncTranslation();
        multiLanguageSprite.SyncTranslation();
        multiLanguageAudioClip.SyncTranslation();

        foreach (var option in multiLanguageOptions)
        {
            option.text.SyncTranslation();
            option.sprite.SyncTranslation();
        }
    }

    private void UpdateLocalization()
    {
        switch (dataType)
        {
            case LocalizationDataType.Text:
                text.text = multiLanguageText.currentTranslation;
                break;
            case LocalizationDataType.TextMesh:
                textMesh.text = multiLanguageText.currentTranslation;
                break;
            case LocalizationDataType.TextMeshPro:
                textMeshPro.text = multiLanguageText.currentTranslation;
                break;
            case LocalizationDataType.TextMeshProUGUI:
                textMeshProUGUI.text = multiLanguageText.currentTranslation;
                break;
            case LocalizationDataType.Dropdown:
                for (int i = 0; i < dropdown.options.Count; i++)
                {
                    if (multiLanguageOptions[i] != null)
                    {
                        MultiLanguageOption option = multiLanguageOptions[i];
                        dropdown.options[i].text = option.text.currentTranslation;
                        dropdown.options[i].image = option.sprite.currentTranslation;
                    }
                }
                break;
            case LocalizationDataType.DropdownTextMeshPro:
                for (int i = 0; i < tmpDropdown.options.Count; i++)
                {
                    if (multiLanguageOptions[i] != null)
                    {
                        MultiLanguageOption option = multiLanguageOptions[i];
                        tmpDropdown.options[i].text = option.text.currentTranslation;
                        tmpDropdown.options[i].image = option.sprite.currentTranslation;
                    }
                }
                break;
            case LocalizationDataType.Image:
                image.sprite = multiLanguageSprite.currentTranslation;
                break;
            case LocalizationDataType.SpriteRenderer:
                spriteRenderer.sprite = multiLanguageSprite.currentTranslation;
                break;
            case LocalizationDataType.AudioSource:
                audioSource.clip = multiLanguageAudioClip.currentTranslation;
                break;
            default: break;

        }
    }

    private void RemoveLocalization()
    {
        if (languageManager == null) { return; }

        languageManager.Remove(multiLanguageText);
        languageManager.Remove(multiLanguageSprite);
        languageManager.Remove(multiLanguageAudioClip);

        foreach (MultiLanguageOption option in multiLanguageOptions)
        {
            languageManager.Remove(option.text);
            languageManager.Remove(option.sprite);
        }
    }

}
