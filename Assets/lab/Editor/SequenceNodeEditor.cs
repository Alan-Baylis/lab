using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using lab;
using System;

[CustomEditor(typeof(SequenceNode))]
public class SequenceNodeEditor : Editor {

    public static Action OnSequenceNodeChanged = delegate { };

    private ReorderableList _list;

    private void OnEnable() {
        _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_nodes"), true, true, false, false);
        _list.drawHeaderCallback += DrawHeader;
        _list.drawElementCallback += DrawElement;
        _list.onReorderCallback += Reorder;
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Connections:");
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        var element = (ANode)_list.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue;
        GUI.Label(new Rect(rect.x, rect.y, rect.width - 35, rect.height), string.Format("{0}. {1}", index, element.GetType().Name));
        if (GUI.Button(new Rect(rect.x + rect.width - 24f, rect.y, 24f, rect.height), "", "OL Minus")) {
            ((SequenceNode)target).RemoveNode(element);
            OnSequenceNodeChanged();
        }
    }

    private void Reorder(ReorderableList list) {
        OnSequenceNodeChanged();
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
