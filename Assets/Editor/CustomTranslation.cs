using UnityEditor;

[CustomEditor(typeof(Translation))]
public class CustomTranslation : Editor
{
    private SerializedProperty titleProp;
    private SerializedProperty languageManagerProp;
    private SerializedProperty translationTypeProp;
    private SerializedProperty TextProp;
    private SerializedProperty mutiLanguageTextProp;
    private SerializedProperty SpriteProp;
    private SerializedProperty mutiLanguageSpriteProp;
    private SerializedProperty ClipProp;
    private SerializedProperty mutiLanguageAudioClipProp;
    private SerializedProperty ObjectProp;
    private SerializedProperty mutiLanguageObjectProp;

    private void OnEnable()
    {
        // 获取SerializedProperty
        titleProp = serializedObject.FindProperty("title");
        languageManagerProp = serializedObject.FindProperty("languageManager");
        translationTypeProp = serializedObject.FindProperty("translationType");
        TextProp = serializedObject.FindProperty("Text");
        mutiLanguageTextProp = serializedObject.FindProperty("multiLanguageText");
        SpriteProp = serializedObject.FindProperty("Sprite");
        mutiLanguageSpriteProp = serializedObject.FindProperty("multiLanguageSprite");
        ClipProp = serializedObject.FindProperty("Clip");
        mutiLanguageAudioClipProp = serializedObject.FindProperty("multiLanguageAudioClip");
        ObjectProp = serializedObject.FindProperty("Object");
        mutiLanguageObjectProp = serializedObject.FindProperty("multiLanguageObject");
    }

    public override void OnInspectorGUI()
    {
        Translation translation = (Translation)target;

        // 更新SerializedObject
        serializedObject.Update();

        //参数
        EditorGUILayout.PropertyField(titleProp);
        EditorGUILayout.PropertyField(languageManagerProp);
        EditorGUILayout.PropertyField(translationTypeProp);

        switch (translation.translationType)
        {
            case TranslationDataType.Text:
                EditorGUILayout.PropertyField(TextProp);
                EditorGUILayout.PropertyField(mutiLanguageTextProp);
                break;
            case TranslationDataType.Sprite:
                EditorGUILayout.PropertyField(SpriteProp);
                EditorGUILayout.PropertyField(mutiLanguageSpriteProp);
                break;
            case TranslationDataType.AudioClip:
                EditorGUILayout.PropertyField(ClipProp);
                EditorGUILayout.PropertyField(mutiLanguageAudioClipProp);
                break;
            case TranslationDataType.Object:
                EditorGUILayout.PropertyField(ObjectProp);
                EditorGUILayout.PropertyField(mutiLanguageObjectProp);
                break;
            default: break;
        }

        // 应用修改到SerializedObject
        serializedObject.ApplyModifiedProperties();
    }
}
