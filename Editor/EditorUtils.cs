using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;


public class EditorUtils
{
    /**
    <summary>
    Draws a header with an texture and some text with shadow
    </summary>
    */
    public static void DrawHeader(string label = "{generic}", string info = "", float scale = .5f, Vector2 offset = new Vector2(), bool addSpaceAtEnd = false, bool drawBackground = true)
    {
        Texture tex = Resources.Load<Texture>("Editor/EditorHeader");
        int fontSize = (int)((tex.height * .5f) * scale);
        offset += new Vector2(10f * scale, 6f);
        GUIStyle headerStyleShadow = new GUIStyle(EditorStyles.largeLabel), headerStyle = new GUIStyle(EditorStyles.largeLabel);
        headerStyleShadow.normal.textColor = new Color(0, 0, 0, .4f);
        headerStyleShadow.fontSize = headerStyle.fontSize = fontSize;
        headerStyle.normal.textColor = Color.white;

        // Draw header:
        EditorGUILayout.Space();
        var rect = GUILayoutUtility.GetRect(0f, 0f);
        Rect labelRect = new Rect(rect), labelShadowRect = new Rect(rect), labelShadowRect2 = new Rect(rect);
        labelRect.x += offset.x;
        labelRect.y += offset.y;

        float o = 1f;
        labelShadowRect.x = labelShadowRect2.x = labelRect.x + o;
        labelShadowRect.y = labelShadowRect2.y = labelRect.y + o;

        labelRect.width = labelShadowRect.width = labelShadowRect2.width = rect.width = (tex.width * scale);
        labelRect.height = labelShadowRect.height = labelShadowRect2.height = rect.height = (tex.height * scale);

        GUILayout.Space(rect.height);
        if (drawBackground) GUI.DrawTexture(rect, tex);
        GUI.Label(labelShadowRect, label, headerStyleShadow);
        GUI.Label(labelShadowRect2, label, headerStyleShadow);
        GUI.Label(labelRect, label, headerStyle);
        if (addSpaceAtEnd) EditorGUILayout.Space();

        if (!info.Equals("")) { EditorGUILayout.Space(); EditorGUILayout.HelpBox(info, MessageType.Info); }
    }

    /**
    <summary>
    Use this function to not only make the 'toggle' or 'foldout-arrow' clickable but the entire horizontal bar, including the label.
    like:
    <code>if (EditorUtils.ClickableHeader("Click me", originalBoolean, ref localBoolean, EditorStyles.toggle)) {}</code>
    </summary>
    */
    public static bool ClickableHeader(string label, string extraInfo, ref bool b, GUIStyle editorStyle, float clickHeight = 16f)
    {
        EditorGUI.BeginChangeCheck();
        Rect r = EditorGUILayout.GetControlRect(true, clickHeight, editorStyle);
        Rect rect = GUILayoutUtility.GetLastRect();
        GUIStyle extraInfoStyle;
        if (extraInfo != "")
        {
            extraInfoStyle = new GUIStyle(EditorStyles.helpBox);
            extraInfoStyle.stretchWidth = true;
        }
        else
        {
            extraInfoStyle = new GUIStyle();
        }
        extraInfoStyle.fontSize = 8;
        if (Event.current.type == EventType.MouseUp && rect.Contains(Event.current.mousePosition))
        {
            b = !b;
            GUI.changed = true;
            Event.current.Use();
        }
        if (editorStyle == EditorStyles.toggle)
        {
            GUILayout.BeginHorizontal();
            b = EditorGUI.Toggle(rect, label, b);
            Rect rect2 = new Rect(rect);
            rect2.x = 80;
            EditorGUI.LabelField(rect2, extraInfo, extraInfoStyle);
            GUILayout.EndHorizontal();
        }
        if (editorStyle == EditorStyles.foldout)
        {
            GUILayout.BeginHorizontal();
            b = EditorGUI.Foldout(rect, b, label);
            Rect rect2 = new Rect(rect);
            rect2.x = 80;
            rect2.width -= 80;
            EditorGUI.LabelField(rect2, extraInfo, extraInfoStyle);
            GUILayout.EndHorizontal();
        }
        EditorGUI.EndChangeCheck();
        return b;
    }

    /**
    <summary>
    Builds a texture
    </summary>
    */
    public static Texture2D BuildTexture(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    /**
    <summary>
    Draws a divider
    </summary>
    */
    public static void Divider(float padding = 10f)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(1));
        r.y += padding / 2;
        Rect r2 = EditorGUILayout.GetControlRect(GUILayout.Height(1));
        r2.y = r.y + 1;
        EditorGUI.DrawRect(r, new Color(0f, 0f, 0f, .4f));
        EditorGUI.DrawRect(r2, new Color(1f, 1f, 1f, .2f));
        EditorGUILayout.GetControlRect(GUILayout.Height(3));
    }
}