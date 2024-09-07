using UnityEditor;

[CustomEditor(typeof(Localization))]
public class CustomLocalization : Editor
{
    private SerializedProperty languageManagerProp;
    private SerializedProperty localizationDataTypeProp;
    private SerializedProperty multiLanguageTextProp;
    private SerializedProperty multiLanguageSpriteProp;
    private SerializedProperty multiLanguageOptionsProp;
    private SerializedProperty multiLanguageAudioClipProp;

    private void OnEnable()
    {
        // ��ȡSerializedProperty
        languageManagerProp = serializedObject.FindProperty("languageManager");
        localizationDataTypeProp = serializedObject.FindProperty("dataType");
        multiLanguageTextProp = serializedObject.FindProperty("multiLanguageText");
        multiLanguageSpriteProp = serializedObject.FindProperty("multiLanguageSprite");
        multiLanguageOptionsProp = serializedObject.FindProperty("multiLanguageOptions");
        multiLanguageAudioClipProp = serializedObject.FindProperty("multiLanguageAudioClip");
    }

    public override void OnInspectorGUI()
    {
        Localization localization = (Localization)target;

        // ����SerializedObject
        serializedObject.Update();

        // ����
        EditorGUILayout.PropertyField(languageManagerProp);
        EditorGUILayout.PropertyField(localizationDataTypeProp);

        switch (localization.dataType)
        {
            case LocalizationDataType.Text:
                //EditorGUILayout.PropertyField(textProp);
                EditorGUILayout.PropertyField(multiLanguageTextProp);
                break;
            case LocalizationDataType.TextMesh:
                EditorGUILayout.PropertyField(multiLanguageTextProp);
                break;
            case LocalizationDataType.TextMeshPro:
                EditorGUILayout.PropertyField(multiLanguageTextProp);
                break;
            case LocalizationDataType.TextMeshProUGUI:
                EditorGUILayout.PropertyField(multiLanguageTextProp);
                break;
            case LocalizationDataType.Dropdown:
                EditorGUILayout.PropertyField(multiLanguageOptionsProp);
                break;
            case LocalizationDataType.DropdownTextMeshPro:
                EditorGUILayout.PropertyField(multiLanguageOptionsProp);
                break;
            case LocalizationDataType.Image:
                EditorGUILayout.PropertyField(multiLanguageSpriteProp);
                break;
            case LocalizationDataType.SpriteRenderer:
                EditorGUILayout.PropertyField(multiLanguageSpriteProp);
                break;
            case LocalizationDataType.AudioSource:
                EditorGUILayout.PropertyField(multiLanguageAudioClipProp);
                break;
            default: break;
        }

        // Ӧ���޸ĵ�SerializedObject
        serializedObject.ApplyModifiedProperties();
    }
}
