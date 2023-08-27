using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Events;

namespace Ypmits.Unitytools
{
	public static class EditorUtils
	{
		private static Dictionary<string, bool> s_prefs = new Dictionary<string, bool>();

		/**
        <summary>
        Draws a header with an texture and some text with shadow
        </summary>
        */
		public static void DrawHeader(string label = "{generic}", string info = "", float scale = .5f, Vector2 offset = new Vector2(), bool addSpaceAtEnd = false, bool drawBackground = true)
		{
			Texture tex = BuildTexture(100, 50, Color.white);// Resources.Load<Texture>("Editor/EditorHeader");
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

		/**
		<summary>
		Put something in the 'OnEnable()'- functon like:
		private ReorderableList _tagList;
		_tagList = EditorUtils.DrawReorderableTagList(serializedObject, "_colliderTags", "Tags (to let this explode)", "These are the tags that can trigger the explosion");
		And then call '_tagList.DoLayoutList()' in the 'OnInspectorGUI()' between the 'serializedObject.Update()' and 'serializedObject.ApplyModifiedProperties()';
		</summary>
		*/
		public static ReorderableList DrawReorderableTagList(SerializedObject serializedObject, string tagProperty, string title = "Tags:", string toolTip = "{{toolTip}}")
		{
			ReorderableList tagList = new ReorderableList(serializedObject, serializedObject.FindProperty(tagProperty), true, true, true, true);
			tagList.drawElementCallback = (Rect r, int index, bool isActive, bool isFocused) =>
			{
				SerializedProperty element = tagList.serializedProperty.GetArrayElementAtIndex(index);
				r.y += 2;
				element.stringValue = EditorGUI.TagField(r, element.stringValue);
			};
			tagList.drawHeaderCallback = (Rect r) => { EditorGUI.LabelField(r, new GUIContent(title, toolTip)); };
			return tagList;
		}

		/**
		<summary>
		Put something in the 'OnEnable()'- functon like:
		private ReorderableList _layerList;
		_layerList = EditorUtils.DrawReorderableTagList(serializedObject, "_layers", "Layers (to do stuff with)", "These are the layers");
		And then call '_layerList.DoLayoutList()' in the 'OnInspectorGUI()' between the 'serializedObject.Update()' and 'serializedObject.ApplyModifiedProperties()';
		</summary>
		*/
		public static ReorderableList DrawReorderableLayerList(SerializedObject serializedObject, string layerProperty, string title = "Layers:", string toolTip = "{{toolTip}}")
		{
			ReorderableList tagList = new ReorderableList(serializedObject, serializedObject.FindProperty(layerProperty), true, true, true, true);
			tagList.drawElementCallback = (Rect r, int index, bool isActive, bool isFocused) =>
			{
				SerializedProperty element = tagList.serializedProperty.GetArrayElementAtIndex(index);
				r.y += 2;
				element.intValue = EditorGUI.LayerField(r, element.intValue);
			};
			tagList.drawHeaderCallback = (Rect r) => { EditorGUI.LabelField(r, new GUIContent(title, toolTip)); };
			return tagList;
		}

		/**
		<summary>
		</summary>
		*/
		public static ReorderableList DrawReorderableList(SerializedObject serializedObject, string property, string title = "ObjectTitle:", string toolTip = "{{ObjectToolTip}}")
		{
			ReorderableList list = new ReorderableList(serializedObject, serializedObject.FindProperty(property), true, true, true, true);
			list.drawElementCallback = (Rect r, int index, bool isActive, bool isFocused) =>
			{
				r.height -= 5;
				EditorGUI.ObjectField(r, list.serializedProperty.GetArrayElementAtIndex(index));
			};
			list.drawHeaderCallback = (Rect r) => { EditorGUI.LabelField(r, new GUIContent(title, toolTip)); };
			return list;
		}

		// Returns an array of sorting layer names
		public static string[] GetSortingLayerNames()
		{
			Type internalEditorUtilityType = typeof(InternalEditorUtility);
			PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
			return (string[])sortingLayersProperty.GetValue(null, new object[0]);
		}

		// Returns an array of integers of unique sorting layer IDs
		public static int[] GetSortingLayerUniqueIDs()
		{
			Type internalEditorUtilityType = typeof(InternalEditorUtility);
			PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
			return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
		}

		// Returns a list of layer names
		public static List<string> GetLayerNames()
		{
			int numLayers = 31;
			List<string> layerNames = new List<string>();
			for (int i = 0; i <= numLayers; i++)
				layerNames.Add(LayerMask.LayerToName(i));
			return layerNames;
		}
		
		/**
		<summary>
		Draws a foldout and returns if it's folded out or not.
		The 'pre' (a unique EditorPrefs-name) will be used to save the data to the EditorPrefs.
		The 'str' is a string that will be visible in the header of the 'toggler'.
		The 'dict' is the dictionary that will be used to look up the GUIContent-data
		Make sure you'll call EditorUtils.PrefsTogglerFoldoutClose(); (or GUILayout.EndVertical();) afterwards!
		Example:
		if(PrefsTogglerFoldout2("showWeaponCollectable")) {
			// Draw Editor-stuff for weaponstuff
		}
		</summary>
		*/
		public static void PrefsTogglerFoldout2(string uniquePrefsName, GUIContent guicontent, UnityEvent action, bool useSurroundingHelpbox = true)
		{
			int i = EditorPrefs.GetInt(uniquePrefsName, 1);
			var returnBool = i == 1;
			if (s_prefs.ContainsKey(uniquePrefsName))
			{
				s_prefs[uniquePrefsName] = returnBool;
			}
			else s_prefs.Add(uniquePrefsName, returnBool);

			EditorGUI.indentLevel = 1;
			GUILayout.BeginVertical(useSurroundingHelpbox ? EditorStyles.helpBox : GUIStyle.none);
			returnBool = EditorGUILayout.Foldout(i == 1, guicontent);
			EditorPrefs.SetInt(uniquePrefsName, returnBool ? 1 : 0);
			EditorGUI.indentLevel = 1;
			if (returnBool) action.Invoke();
			GUILayout.EndVertical();
		}
	}
}











/**
	<summary>
	Draws a foldout and returns if it's folded out or not.
	The 'uniquePrefsName' will be used to save to the EditorPrefs.
	Make sure you'll call EditorUtils.PrefsTogglerFoldoutClose(); (or GUILayout.EndVertical();) afterwards!
	Example:
	if(PrefsToggler("showWeaponCollectable")) {
		// Draw Editor-stuff for weaponstuff
	}
	</summary>
	*/
/**
	public static bool PrefsTogglerFoldout(string uniqueEditorPrefsString, GUIContent guiContent, bool useSurroundingHelpbox = true)
	{
		int i = EditorPrefs.GetInt(uniqueEditorPrefsString, 1);
		var returnBool = i == 1;
		if (s_prefs.ContainsKey(uniqueEditorPrefsString)) s_prefs[uniqueEditorPrefsString] = returnBool;
		else s_prefs.Add(uniqueEditorPrefsString, returnBool);

		EditorGUI.indentLevel = 1;
		GUILayout.BeginVertical(useSurroundingHelpbox ? EditorStyles.helpBox : GUIStyle.none);
		returnBool = EditorGUILayout.Foldout(i == 1 ? true : false, guiContent);
		EditorPrefs.SetInt(uniqueEditorPrefsString, returnBool ? 1 : 0);
		EditorGUI.indentLevel = 1;
		return returnBool;
	}

	public static void PrefsTogglerFoldoutClose()
	{
		GUILayout.EndVertical();
	}


	public static bool DisplayDialog(string title, string message, string ok, string cancel, bool altCtrl = true, Event e = null)
	{
		bool ac = false;
		if (altCtrl && e != null) ac = (Event.current.alt && Event.current.control);
		return (ac || EditorUtility.DisplayDialog(title: "Warning!", message: message, ok: ok, cancel: cancel));
	}
	/**

*/