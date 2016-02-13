﻿using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(AiController))]
public class AiControllerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var aiController = (AiController)target;
        if (aiController.Blackboard == null) {
            EditorGUILayout.HelpBox("Not initialized", MessageType.Info);
        }
    }
}
