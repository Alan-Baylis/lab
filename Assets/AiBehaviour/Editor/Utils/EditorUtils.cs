﻿using UnityEngine;
using AiBehaviour;
using System.Collections.Generic;

namespace UnityEditor {
    public static class EditorUtils {

        public static string[] ToArray<T>(this Dictionary<string, T>.KeyCollection collection) {
            string[] keys = new string[collection.Count];
            collection.CopyTo(keys, 0);
            return keys;
        }

        public static string[] TreesToNames(IList<AiTree> trees) {
            List<string> names = new List<string>();
            for (int i = 0; i < trees.Count; ++i) {
                names.Add(string.Format("Tree {0}", i));
            }
            return names.ToArray();
        }

        public static void DrawGrid(Rect position) {
            Profiler.BeginSample("DrawGrid");
            GL.PushMatrix();
            GL.Begin(GL.LINES);
            float num = 0f;
            float num2 = 0f;
            float num3 = num + position.width;
            float num4 = num2 + position.height;
            DrawGridLines(12f, new Color(1f, 1f, 1f, 0.35f), new Vector2(num, num2), new Vector2(num3, num4));
            DrawGridLines(120f, Color.white, new Vector2(num, num2), new Vector2(num3, num4));
            GL.End();
            GL.PopMatrix();
            Profiler.EndSample();
        }

        public static void DrawGridLines(float gridSize, Color color, Vector2 min, Vector2 max) {
            GL.Color(color);
            for (float num = min.x - min.x % gridSize; num < max.x; num += gridSize) {
                GL.Vertex(new Vector2(num, min.y));
                GL.Vertex(new Vector2(num, max.y));
            }
            for (float num2 = min.y - min.y % gridSize; num2 < max.y; num2 += gridSize) {
                GL.Vertex(new Vector2(min.x, num2));
                GL.Vertex(new Vector2(max.x, num2));
            }
        }

        public static void DrawNodeCurve(Rect start, Rect end) {
            Vector3 startPos = new Vector3(start.x + start.width / 2f, start.y + start.height / 2f, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2f, end.y + end.height / 2f, 0);
            Vector3 middlePos = (startPos + endPos) / 2f;
            DrawLine(startPos, middlePos, Color.white);
            DrawLine(middlePos, endPos, new Color(0.6f, 0.6f, 0.6f));
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color) {
            Color shadowCol = color;
            shadowCol.a = 0.35f;
            for (int i = 0; i < 3; ++i) {
                Handles.DrawBezier(start, end, start, end, shadowCol, null, (i + 1) * 2);
            }
            Handles.DrawBezier(start, end, start, end, color, null, 1);
        }

        public static void DrawLabel(Vector3 position, string text) {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            Handles.Label(position, text, style);
        }
    }
}
