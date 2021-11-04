using UnityEditor;

[CustomEditor(typeof(LevelSO)), CanEditMultipleObjects]
public class CustomEditorLevelSO : Editor
{
    public SerializedProperty
        levelType,
        levelID,
        levelPrefab,
        totalBothSideObject,
        levelAnswers,
        matchAnswer;

    private void OnEnable()
    {
        levelType = serializedObject.FindProperty("levelType");
        levelID = serializedObject.FindProperty("levelID");
        levelPrefab = serializedObject.FindProperty("levelPrefab");
        totalBothSideObject = serializedObject.FindProperty("totalBothSidesObject");
        levelAnswers = serializedObject.FindProperty("levelAnswers");
        matchAnswer = serializedObject.FindProperty("matchAnswer");
    }


    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(levelType);
        EditorGUILayout.PropertyField(levelID);

        LevelSO.LevelType _levelType = (LevelSO.LevelType)levelType.enumValueIndex;

        switch (_levelType)
        {
            case LevelSO.LevelType.Menghitung:
                EditorGUILayout.PropertyField(levelPrefab);
                EditorGUILayout.PropertyField(levelAnswers);
                EditorGUILayout.PropertyField(matchAnswer);
                break;

            case LevelSO.LevelType.Pengurangan:
                EditorGUILayout.PropertyField(levelPrefab);
                EditorGUILayout.PropertyField(totalBothSideObject);
                EditorGUILayout.PropertyField(levelAnswers);
                EditorGUILayout.PropertyField(matchAnswer);
                break;

            case LevelSO.LevelType.Penjumlahan:
                EditorGUILayout.PropertyField(levelPrefab);
                EditorGUILayout.PropertyField(totalBothSideObject);
                EditorGUILayout.PropertyField(levelAnswers);
                EditorGUILayout.PropertyField(matchAnswer);
                break;

            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}
