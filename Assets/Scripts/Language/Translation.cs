using UnityEngine;

public enum TranslationDataType
{
    Text,
    Sprite,
    AudioClip,
    Object
}

public class Translation : MonoBehaviour
{
    public string title = string.Empty;

    public LanguageManager languageManager = null;

    public TranslationDataType translationType = TranslationDataType.Text;

    public string Text = string.Empty;
    public MultiLanguageItem<string> multiLanguageText = null;

    public Sprite Sprite = null;
    public MultiLanguageItem<Sprite> multiLanguageSprite = null;

    public AudioClip Clip = null;
    public MultiLanguageItem<AudioClip> multiLanguageAudioClip = null;

    public GameObject Object = null;
    public MultiLanguageItem<GameObject> multiLanguageObject = null;

    private void Awake()
    {
        if (!GetLanguageManager())
        { Debug.LogError("No LanguageManager"); }

        if (languageManager != null)
        {
            switch (translationType)
            {
                case TranslationDataType.Text:
                    languageManager.Register(multiLanguageText);
                    break;
                case TranslationDataType.Sprite:
                    languageManager.Register(multiLanguageSprite);
                    break;
                case TranslationDataType.AudioClip:
                    languageManager.Register(multiLanguageAudioClip);
                    break;
                case TranslationDataType.Object:
                    languageManager.Register(multiLanguageObject);
                    break;
                default: break;
            }

            languageManager.onLanguageChange.AddListener(UpdateTranslation);
        }
    }

    private void OnDestroy()
    {
        RemoveTranslation();
    }

    private bool GetLanguageManager()
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

    private void UpdateTranslation()
    {
        switch (translationType)
        {
            case TranslationDataType.Text:
                Text = multiLanguageText.currentTranslation;
                break;
            case TranslationDataType.Sprite:
                Sprite = multiLanguageSprite.currentTranslation;
                break;
            case TranslationDataType.AudioClip:
                Clip = multiLanguageAudioClip.currentTranslation;
                break;
            case TranslationDataType.Object:
                Object = multiLanguageObject.currentTranslation;
                break;
            default: break;
        }
    }

    private void RemoveTranslation()
    {
        if (languageManager == null) { return; }

        languageManager.Remove(multiLanguageText);
        languageManager.Remove(multiLanguageSprite);
        languageManager.Remove(multiLanguageAudioClip);
        languageManager.Remove(multiLanguageObject);
    }

}
