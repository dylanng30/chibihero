using UnityEngine;
using UnityEditor;
using System.IO;

public class AudioSetupHelper : EditorWindow
{
    [MenuItem("Tools/Setup Audio System")]
    public static void SetupAudioSystem()
    {
        // Tạo AudioSystem GameObject nếu chưa có
        AudioSystem audioSystem = FindObjectOfType<AudioSystem>();
        
        if (audioSystem == null)
        {
            GameObject audioSystemObj = new GameObject("AudioSystem");
            audioSystem = audioSystemObj.AddComponent<AudioSystem>();
            Debug.Log("Created AudioSystem GameObject in scene.");
        }
        
        // Tự động setup AudioSources và load audio clips
        audioSystem.CreateAudioSourcesManually();
        
        // Tự động tìm và gán audio clips từ Assets/Sounds
        AutoAssignAllAudioClips(audioSystem);
        
        Debug.Log("Audio System Setup Complete! All audio files found and assigned from Assets/Sounds folder.");
    }
    
    private static void AutoAssignAllAudioClips(AudioSystem audioSystem)
    {
        SerializedObject serializedObject = new SerializedObject(audioSystem);
        
        // Tìm và gán các audio clips
        AssignAudioClip(serializedObject, "walkingSound", "Walking");
        AssignAudioClip(serializedObject, "jumpSound", "JUMP");
        AssignAudioClip(serializedObject, "attackSound1", "attack1");
        AssignAudioClip(serializedObject, "attackSound2", "attack2");
        AssignAudioClip(serializedObject, "attackSound3", "attack3");
        AssignAudioClip(serializedObject, "backgroundMusic", "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private static void AssignAudioClip(SerializedObject serializedObject, string propertyName, string clipName)
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
