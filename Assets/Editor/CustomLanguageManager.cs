using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LanguageManager))]
public class CustomLanguageManager : Editor
{
    private SerializedProperty currentLanguageProp;
    private SerializedProperty multiLanguageTextsProp;
    private SerializedProperty multiLanguageSpritesProp;
    private SerializedProperty multiLanguageAudioClipsProp;
    private SerializedProperty multiLanguageObjectsProp;
    private SerializedProperty onLanguageChangeProp;

    private void OnEnable()
    {
        // ��ȡSerializedProperty
        currentLanguageProp = serializedObject.FindProperty("currentLanguage");
        multiLanguageTextsProp = serializedObject.FindProperty("multiLanguageTexts");
        multiLanguageSpritesProp = serializedObject.FindProperty("multiLanguageSprites");
        multiLanguageAudioClipsProp = serializedObject.FindProperty("multiLanguageAudioClips");
        multiLanguageObjectsProp = serializedObject.FindProperty("multiLanguageObjects");
        onLanguageChangeProp = serializedObject.FindProperty("onLanguageChange");
    }
    public override void OnInspectorGUI()
    {
        LanguageManager languageManager = (LanguageManager)target;

        // ����SerializedObject
        serializedObject.Update();

        //����
        EditorGUILayout.PropertyField(currentLanguageProp);

        if (languageManager.multiLanguageTexts.Count > 0)
        { EditorGUILayout.PropertyField(multiLanguageTextsProp); }
        if (languageManager.multiLanguageSprites.Count > 0)
        { EditorGUILayout.PropertyField(multiLanguageSpritesProp); }
        if (languageManager.multiLanguageAudioClips.Count > 0)
        { EditorGUILayout.PropertyField(multiLanguageAudioClipsProp); }
        if (languageManager.multiLanguageObjects.Count > 0)
        { EditorGUILayout.PropertyField(multiLanguageObjectsProp); }

        EditorGUILayout.PropertyField(onLanguageChangeProp);

        //���ܰ���
        if (GUILayout.Button("Update"))
        {
            languageManager.UpdateLanguage();
        }

        // Ӧ���޸ĵ�SerializedObject
        serializedObject.ApplyModifiedProperties();
    }
}
