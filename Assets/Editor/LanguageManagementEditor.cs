using UnityEngine;
using UnityEditor;

public class LanguageManagementEditor : Editor
{
    [MenuItem("GameObject/Language/Manager")]
    public static void CreateLanguageManagement()
    {
        GameObject languageManager = new GameObject();

        LanguageManager languageManagementScript = languageManager.AddComponent<LanguageManager>();

        languageManager.name = "LanguageManager";
    }
}
