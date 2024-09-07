using UnityEngine;
using UnityEditor;

public class TranslationEditor
{
    [MenuItem("GameObject/Language/Translation")]
    public static void CreateTranslation()
    {
        GameObject translation = new GameObject();

        Translation translationScript = translation.AddComponent<Translation>();

        translation.name = "Translation";
    }
}
