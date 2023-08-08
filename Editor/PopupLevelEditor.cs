using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;

namespace Ypmits.Unitytools
{
	/**
	<summary>
	</summary>
	*/
	public class PopupInputLevelEditor : EditorWindow
	{
		private static UnityAction<string> _okHandler;
		private static UnityAction _cancelHandler;
		private static string _title;
		private string _label;


		public static void Open(string title, Rect pos, UnityAction<string> okHandler = null, UnityAction cancelHandler = null, float width = 450f, float height = 150f)
		{
			_title = title;
			_okHandler = okHandler;
			_cancelHandler = cancelHandler;

			PopupInputLevelEditor window = ScriptableObject.CreateInstance<PopupInputLevelEditor>();
			float x = (pos.x + (pos.width * .5f)) - (width * .5f);
			float y = (pos.y + (pos.height * .5f)) - (height * .5f);
			window.position = new Rect(x, y, width, height);
			window.titleContent = new GUIContent(title, "The title of the window");
			window.ShowUtility();
			window.Focus();
		}

		private void OnGUI()
		{
			UnityEngine.Event e = UnityEngine.Event.current;
			GUI.SetNextControlName("_label");
			_label = EditorGUILayout.TextField(new GUIContent("Name:", "tooltip"), _label);
			EditorGUI.FocusTextInControl("_label");
			GUILayout.Space(70);
			GUILayout.BeginHorizontal(EditorStyles.helpBox);
			if (GUILayout.Button("OK") || (e.isKey && e.keyCode == KeyCode.Return)) OKHandler();
			if (GUILayout.Button("Cancel") || (e.isKey && e.keyCode == KeyCode.Escape)) CloseWindow();
			GUILayout.EndHorizontal();
		}

		private void OKHandler()
		{
			_okHandler?.Invoke(_label);
			this.Close();
		}

		private void CloseWindow()
		{
			_cancelHandler?.Invoke();
			this.Close();
		}
	}
}