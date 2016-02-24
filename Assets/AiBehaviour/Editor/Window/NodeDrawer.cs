﻿using UnityEngine;
using UnityEditor;
using AiBehaviour;

public class NodeDrawer {

    public delegate void NodeDrawerHandler(ANode node);
    public static NodeDrawerHandler OnRightClicked = delegate { };
    public static NodeDrawerHandler OnLeftClicked = delegate { };

    public static readonly Vector2 gSize = new Vector2(144f, 32f);

    private static int gNodeDrawerIdIncremental = 0;

    private readonly string _rootStyle = "flow node 5";
    private readonly string _rootStyleOn = "flow node 5 on";
    private readonly string _defaultStyle = "flow node 0";
    private readonly string _defaultStyleOn = "flow node 0 on";

    private ANode _node;
    private int _id;
    private Rect _rect;
    private bool _isRoot = false;

    public bool IsRoot {
        get { return _isRoot; }
        set { _isRoot = value; }
    }

    public void SetOffset(Vector2 offset) {
        _node.Position += offset;
    }

    public NodeDrawer(ANode node, bool isRoot = false) {
        _id = gNodeDrawerIdIncremental++;
        _node = node;
        _isRoot = isRoot;
        _rect = new Rect(0f, 0f, gSize.x, gSize.y);
    }

    public void DrawNode() {
        _rect.x = _node.Position.x;
        _rect.y = _node.Position.y;
        _rect = GUI.Window(_id, _rect, new GUI.WindowFunction(DrawNodeWindow), "", (Selection.activeObject != _node) ? (_isRoot ? _rootStyle : _defaultStyle) : (_isRoot ? _rootStyleOn : _defaultStyleOn));
        _node.Position.x = _rect.x;
        _node.Position.y = _rect.y;
    }

    private void DrawNodeWindow(int id) {
        Event e = Event.current;
        if (_node == null) {
            return;
        }
        GUI.Label(new Rect(0, gSize.y / 2f - 8, _rect.width, 16), new GUIContent(_node.GetType().Name, EditorGUIUtility.ObjectContent(_node, _node.GetType()).image));
        if (e.type == EventType.MouseUp && e.button == 1) {
            OnRightClicked(_node);
            e.Use();
        }
        if (e.type == EventType.MouseDown && e.button == 0) {
            OnLeftClicked(_node);
        }
        EditorUtility.SetDirty(_node);
        GUI.DragWindow();
    }
}
