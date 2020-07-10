using UnityEngine;
using UnityEditor;
using DG.Tweening;
using UnityEngine.UI;


/**
<summary>
</summary>
*/
[CustomEditor(typeof(Visualizer)), CanEditMultipleObjects]
public class VisualizerEditor : Editor
{
	private int m_indent;

	private bool m_togglePosUI;
	private bool showPositionShow;
	private bool showPositionHide;

	private bool toggleRotation;
	private bool showRotationShow;
	private bool showRotationHide;

	private bool toggleScale;
	private bool showScaleShow;
	private bool showScaleHide;

	private bool toggleTransparency;
	private bool showTransparencyShow;
	private bool showTransparencyHide;

	private bool toggleEtc;

	private SerializedProperty m_useCurrentAsShowAmount;
	private SerializedProperty m_autoShow;
	private SerializedProperty m_autoHide;
	private SerializedProperty m_debugOutput;
	private SerializedProperty m_disableWhenHiding;

	private Visualizer vis;
	private int v = 0;
	private bool use3D = true;
	private bool useMovement = false;


	void OnEnable()
	{
		m_useCurrentAsShowAmount = serializedObject.FindProperty("useCurrentAsShowAmount");
		m_autoShow = serializedObject.FindProperty("autoShow");
		m_autoHide = serializedObject.FindProperty("autoHide");
		m_debugOutput = serializedObject.FindProperty("debugOutput");
		m_disableWhenHiding = serializedObject.FindProperty("disableWhenHiding");
	}

	public override void OnInspectorGUI()
	{
		vis = target as Visualizer;

		SerializedObject so = serializedObject;
		so.Update();

		so.FindProperty("_image").objectReferenceValue = vis.GetComponent<Image>();
		if (vis.GetComponent<Image>() != null) so.FindProperty("use3D").boolValue = use3D = false;
		so.FindProperty("_spriteRenderer").objectReferenceValue = vis.GetComponent<SpriteRenderer>();

		m_indent = EditorGUI.indentLevel;

		GUIStyle st = new GUIStyle(EditorStyles.miniButton);
		st.fixedWidth = 40;
		GUILayout.BeginVertical(EditorStyles.helpBox);
		v = so.FindProperty("useMovement").boolValue ? 1 : 0;
		v = GUILayout.SelectionGrid(v, new string[2] { "None", use3D ? "Transform" : "RectTransform" }, 2, "toggle");

		so.FindProperty("useMovement").boolValue = useMovement = v > 0;

		// Debug.Log($"v: {v} type: {(use3D ? "3D" : "2D")} movement: {(useMovement ? "yes" : "no")}");

		// If 'None' was selected for position:
		if (useMovement)
		{
			if (use3D)
			{
				// so.FindProperty("use3D").boolValue = true;
				GUILayout.BeginHorizontal();
				so.FindProperty("showPosition").vector3Value = EditorGUILayout.Vector3Field("Show Position", so.FindProperty("showPosition").vector3Value);
				if (GUILayout.Button("Cur", st))
					so.FindProperty("showPosition").vector3Value = vis.transform.localPosition;
				if (GUILayout.Button("Set", st))
					vis.transform.localPosition = so.FindProperty("showPosition").vector3Value;
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				so.FindProperty("hidePosition").vector3Value = EditorGUILayout.Vector3Field("Hide Position", so.FindProperty("hidePosition").vector3Value);
				if (GUILayout.Button("Cur", st))
					so.FindProperty("hidePosition").vector3Value = vis.transform.localPosition;
				if (GUILayout.Button("Set", st))
					vis.transform.localPosition = so.FindProperty("hidePosition").vector3Value;
				GUILayout.EndHorizontal();
			}
			else
			{
				RectTransform rt = vis.transform as RectTransform;
				Debug.Log(rt);
				// so.FindProperty("use3D").boolValue = false;
				GUILayout.BeginHorizontal();
				so.FindProperty("showPosition2D").vector3Value = EditorGUILayout.Vector3Field("Show AnchorPosition", so.FindProperty("showPosition2D").vector3Value);
				if (GUILayout.Button("Cur", st))
					so.FindProperty("showPosition2D").vector3Value = rt.anchoredPosition3D;
				if (GUILayout.Button("Set", st))
					rt.anchoredPosition3D = so.FindProperty("showPosition2D").vector3Value;
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				so.FindProperty("hidePosition2D").vector3Value = EditorGUILayout.Vector3Field("Hide AnchorPosition", so.FindProperty("hidePosition2D").vector3Value);
				if (GUILayout.Button("Cur", st))
					so.FindProperty("hidePosition2D").vector3Value = rt.anchoredPosition3D;
				if (GUILayout.Button("Set", st))
					rt.anchoredPosition3D = so.FindProperty("hidePosition2D").vector3Value;
				GUILayout.EndHorizontal();
			}
			EditorGUI.indentLevel = m_indent;
			BuildShowHideParameters(
				header: $"Show-vars",
				extraInfo: $"dur:{so.FindProperty("showPositionDuration").floatValue} delay:{so.FindProperty("showPositionDelay").floatValue} ease:{vis.showPositionEase}",
				indent: 1,
				toggleBool: ref showPositionShow,
				durationString: "Show-duration",
				duration: ref vis.showPositionDuration,
				delayString: "Show-delay",
				delay: ref vis.showPositionDelay,
				easeString: "Show-ease",
				ease: ref vis.showPositionEase,
				completeAction: "showPositionCompleteAction"
			);
			BuildShowHideParameters(
				header: "Hide-vars",
				extraInfo: $"dur:{so.FindProperty("hidePositionDuration").floatValue} delay:{so.FindProperty("hidePositionDelay").floatValue} ease:{vis.hidePositionEase}",
				indent: 1,
				toggleBool: ref showPositionHide,
				durationString: "Hide-duration",
				duration: ref vis.hidePositionDuration,
				delayString: "Hide-delay",
				delay: ref vis.hidePositionDelay,
				easeString: "Hide-ease",
				ease: ref vis.hidePositionEase,
				completeAction: "hidePositionCompleteAction"
			);
		}
		GUILayout.EndVertical();





		GUILayout.BeginVertical(EditorStyles.helpBox);
		EditorGUI.indentLevel = m_indent;
		// if (ClickableHeader("Rotation", "", ref t.useRotation, EditorStyles.toggle))
		if (ClickableHeader("Rotation", "", ref vis.useRotation, EditorStyles.toggle))
		{
			GUILayout.BeginHorizontal();
			// t.showRotation = EditorGUILayout.Vector3Field("Show-rotation", t.showRotation);
			so.FindProperty("showRotation").vector3Value = EditorGUILayout.Vector3Field("Show-rotation", so.FindProperty("showRotation").vector3Value);
			if (GUILayout.Button("Current", GUILayout.Width(70))) so.FindProperty("showRotation").vector3Value = vis.transform.localRotation.eulerAngles;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			so.FindProperty("hideRotation").vector3Value = EditorGUILayout.Vector3Field("Hide-rotation", so.FindProperty("hideRotation").vector3Value);
			if (GUILayout.Button("Current", GUILayout.Width(70))) so.FindProperty("hideRotation").vector3Value = vis.transform.localRotation.eulerAngles;
			GUILayout.EndHorizontal();

			// Show/Hide stuff:
			BuildShowHideParameters(
				header: "Show Vars",
				extraInfo: $"dur:{so.FindProperty("showRotationDuration").floatValue} del:{so.FindProperty("showRotationDelay").floatValue} e:{so.FindProperty("showRotationEase").objectReferenceValue}",
				indent: 1,
				toggleBool: ref showRotationShow,
				durationString: "Show-duration",
				duration: ref vis.showRotationDuration,
				delayString: "Show-delay",
				delay: ref vis.showRotationDelay,
				easeString: "Show-ease",
				ease: ref vis.showRotationEase,
				completeAction: "showRotationCompleteAction");
			BuildShowHideParameters(
				header: "Hide Vars",
				extraInfo: $"dur:{so.FindProperty("hideRotationDuration").floatValue} del:{so.FindProperty("hideRotationDelay").floatValue} e:{so.FindProperty("hideRotationEase").objectReferenceValue}",
				indent: 1,
				toggleBool: ref showRotationHide,
				durationString: "Hide-duration",
				duration: ref vis.hideRotationDuration,
				delayString: "Hide-delay",
				delay: ref vis.hideRotationDelay,
				easeString: "Hide-ease",
				ease: ref vis.hideRotationEase,
				completeAction: "hideRotationCompleteAction");
		}
		GUILayout.EndVertical();

		//
		//   _______              __        
		//  |     __|.----.---.-.|  |.-----.
		//  |__     ||  __|  _  ||  ||  -__|
		//  |_______||____|___._||__||_____|
		//
		//
		GUILayout.BeginVertical(EditorStyles.helpBox);
		if (ClickableHeader("Scale", "", ref vis.useScale, EditorStyles.toggle))
		{
			GUILayout.BeginHorizontal();
			so.FindProperty("showScale").vector3Value = EditorGUILayout.Vector3Field("Show-scale", so.FindProperty("showScale").vector3Value);
			if (GUILayout.Button("Current", GUILayout.Width(70))) so.FindProperty("showScale").vector3Value = vis.transform.localScale;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			so.FindProperty("hideScale").vector3Value = EditorGUILayout.Vector3Field("Hide-scale", so.FindProperty("hideScale").vector3Value);
			if (GUILayout.Button("Current", GUILayout.Width(70))) so.FindProperty("hideScale").vector3Value = vis.transform.localScale;
			GUILayout.EndHorizontal();

			// Show/Hide stuff:
			BuildShowHideParameters(
				header: "Show Vars",
				extraInfo: $"dur:{so.FindProperty("showScaleDuration").floatValue} del:{so.FindProperty("showScaleDelay").floatValue} e:{so.FindProperty("showScaleEase").floatValue}",
				indent: 1,
				toggleBool: ref showScaleShow,
				durationString: "Show-duration",
				duration: ref vis.showScaleDuration,
				delayString: "Show-delay",
				delay: ref vis.showScaleDelay,
				easeString: "Show-ease",
				ease: ref vis.showScaleEase,
				completeAction: "showScaleCompleteAction");
			BuildShowHideParameters(
				header: "Hide Vars",
				extraInfo: $"dur:{so.FindProperty("hideScaleDuration").floatValue} del:{so.FindProperty("hideScaleDelay").floatValue} e:{so.FindProperty("hideScaleEase").floatValue}",
				indent: 1,
				toggleBool: ref showScaleHide,
				durationString: "Hide-duration",
				duration: ref vis.hideScaleDuration,
				delayString: "Hide-delay",
				delay: ref vis.hideScaleDelay,
				easeString: "Hide-ease",
				ease: ref vis.hideScaleEase,
				completeAction: "hideScaleCompleteAction");
		}
		GUILayout.EndVertical();

		//
		//   _______                                                                  
		//  |_     _|.----.---.-.-----.-----.-----.---.-.----.-----.-----.----.--.--.
		//    |   |  |   _|  _  |     |__ --|  _  |  _  |   _|  -__|     |  __|  |  |
		//    |___|  |__| |___._|__|__|_____|   __|___._|__| |_____|__|__|____|___  |
		//                                  |__|                              |_____|
		//
		//
		GUILayout.BeginVertical(EditorStyles.helpBox);
		if (ClickableHeader("Transparency (Renderer)", "", ref vis.useTransparency, EditorStyles.toggle))
		{
			// EditorGUI.indentLevel = indent + 1;
			so.FindProperty("showFadeAmount").floatValue = EditorGUILayout.FloatField("show-fadeAmount", so.FindProperty("showFadeAmount").floatValue);
			so.FindProperty("hideFadeAmount").floatValue = EditorGUILayout.FloatField("Hide-fadeAmount", so.FindProperty("hideFadeAmount").floatValue);

			// Show/Hide stuff:
			BuildShowHideParameters("Show Vars", $"dur:{so.FindProperty("showTransparencyDuration").floatValue} del:{so.FindProperty("showTransparencyDelay").floatValue} e:{vis.showTransparencyEase}", 1, ref showTransparencyShow, "Show-duration", ref vis.showTransparencyDuration, "Show-delay", ref vis.showTransparencyDelay, "Show-ease", ref vis.showTransparencyEase, "showTransparencyCompleteAction");
			BuildShowHideParameters("Hide Vars", $"dur:{so.FindProperty("hideTransparencyDuration").floatValue} del:{so.FindProperty("hideTransparencyDelay").floatValue} e:{vis.hideTransparencyEase}", 1, ref showTransparencyHide, "Hide-duration", ref vis.hideTransparencyDuration, "Hide-delay", ref vis.hideTransparencyDelay, "Hide-ease", ref vis.hideTransparencyEase, "hideTransparencyCompleteAction");
		}
		GUILayout.EndVertical();

		GUILayout.BeginVertical(EditorStyles.helpBox);
			EditorGUI.indentLevel = m_indent;
			EditorGUILayout.PropertyField(m_useCurrentAsShowAmount);
			EditorGUILayout.PropertyField(m_autoShow);
			EditorGUILayout.PropertyField(m_autoHide);
			EditorGUILayout.PropertyField(m_disableWhenHiding);
			EditorGUILayout.PropertyField(m_debugOutput);
			EditorGUI.indentLevel = m_indent;
		GUILayout.EndVertical();

		// Show/Hide buttons
		GUILayout.BeginHorizontal(EditorStyles.helpBox);
		if (GUILayout.Button("Show")) vis.Show();
		if (GUILayout.Button("Hide")) vis.Hide();
		GUILayout.EndHorizontal();

		so.ApplyModifiedProperties();
	}


	private void BuildShowHideParameters(string header, string extraInfo, int indent, ref bool toggleBool, string durationString, ref float duration, string delayString, ref float delay, string easeString, ref Ease ease, string completeAction)
	{
		int oldIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = oldIndent + indent;
		if (ClickableHeader(header, extraInfo, ref toggleBool, EditorStyles.foldout))
		{
			EditorGUI.indentLevel = oldIndent + indent + 1;
			duration = EditorGUILayout.FloatField(durationString, duration);
			delay = EditorGUILayout.FloatField(delayString, delay);
			ease = (Ease)EditorGUILayout.EnumPopup(easeString, ease);
			EditorGUILayout.PropertyField(serializedObject.FindProperty(completeAction));
		}
		EditorGUI.indentLevel = oldIndent;
	}

	private bool ClickableHeader(string label, string extraInfo, ref bool b, GUIStyle editorStyle, float clickHeight = 16f)
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

		Rect rect2 = new Rect(rect);
		rect2.x = 80;
		GUILayout.BeginHorizontal();
		if (editorStyle == EditorStyles.toggle) b = EditorGUI.Toggle(rect, label, b);
		else if (editorStyle == EditorStyles.foldout)
		{
			b = EditorGUI.Foldout(rect, b, label);
			rect2.width -= 80;
		}
		EditorGUI.LabelField(rect2, extraInfo, extraInfoStyle);
		GUILayout.EndHorizontal();

		EditorGUI.EndChangeCheck();
		return b;
	}
}