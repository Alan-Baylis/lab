using UnityEngine;
using UnityEditor;
using lab;

[CustomEditor(typeof(InverterNode))]
public class InverterNodeEditor : Editor {

    public override void OnInspectorGUI() {
        var parameter = (InverterNode)target;
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Connection:");
        var node = parameter.GetNode(0);
        if (node != null) {
            GUILayout.Label(string.Format("0. {0}", node.GetType().Name));
            if (GUILayout.Button("-", GUILayout.Width(35))) {
                parameter.RemoveNode(node);
                if (LabWindow.gWindow != null) {
                    LabWindow.gWindow.Repaint();
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
