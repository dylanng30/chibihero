using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioSystem))]
public class AudioSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AudioSystem audioSystem = (AudioSystem)target;
        
        // Vẽ Inspector mặc định
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Quick Setup", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Create Audio Sources"))
        {
            audioSystem.CreateAudioSourcesManually();
            EditorUtility.SetDirty(audioSystem);
        }
          if (GUILayout.Button("Load Audio Clips"))
        {
            audioSystem.SetupAudioClipsManually();
            EditorUtility.SetDirty(audioSystem);
        }
        
        if (GUILayout.Button("Auto Find Audio"))
        {
            AutoAssignAudioClips(audioSystem);
            EditorUtility.SetDirty(audioSystem);
        }
        
        EditorGUILayout.EndHorizontal();
          if (GUILayout.Button("Complete Setup"))
        {
            audioSystem.CreateAudioSourcesManually();
            audioSystem.SetupAudioClipsManually();
            AutoAssignAudioClips(audioSystem);
            EditorUtility.SetDirty(audioSystem);
            Debug.Log("AudioSystem setup complete!");
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Testing & Debug", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Test All Sounds"))
        {
            audioSystem.TestAllSounds();
        }
        
        if (GUILayout.Button("Debug Status"))
        {
            audioSystem.DebugAudioStatus();
        }
        
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "1. Click 'Create Audio Sources' to automatically create child AudioSource objects\n" +
            "2. Click 'Auto Find Audio' to automatically find and assign audio clips from Assets/Sounds\n" +
            "3. Click 'Load Audio Clips' to load using the LoadAudioClips method\n" +
            "4. Or use 'Complete Setup' to do everything at once\n" +
            "5. Use 'Test All Sounds' to check if audio is working\n" +
            "6. Use 'Debug Status' to see audio system info\n" +
            "7. You can also manually drag audio files to the slots above", 
            MessageType.Info);
    }
    
    private void AutoAssignAudioClips(AudioSystem audioSystem)
    {
        SerializedObject serializedObject = new SerializedObject(audioSystem);
        
        // Tìm và gán Walking sound
        AssignAudioClip(serializedObject, "walkingSound", "Walking");
        
        // Tìm và gán Jump sound  
        AssignAudioClip(serializedObject, "jumpSound", "JUMP");
        
        // Tìm và gán Attack sounds
        AssignAudioClip(serializedObject, "attackSound1", "attack1");
        AssignAudioClip(serializedObject, "attackSound2", "attack2");
        AssignAudioClip(serializedObject, "attackSound3", "attack3");
        
        // Tìm và gán Background music
        AssignAudioClip(serializedObject, "backgroundMusic", "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
        
        serializedObject.ApplyModifiedProperties();
        Debug.Log("Audio clips auto-assigned from Assets/Sounds folder!");
    }
    
    private void AssignAudioClip(SerializedObject serializedObject, string propertyName, string clipName)
    {
        string[] guids = AssetDatabase.FindAssets($"{clipName} t:AudioClip");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            
            SerializedProperty property = serializedObject.FindProperty(propertyName);
            property.objectReferenceValue = clip;
            
            Debug.Log($"Found and assigned: {clipName} at {path}");
        }
        else
        {
            Debug.LogWarning($"Could not find audio clip: {clipName}");
        }
    }
}
