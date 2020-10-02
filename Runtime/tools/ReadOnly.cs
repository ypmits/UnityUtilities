using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ReadOnlyAttribute : PropertyAttribute {}
 
#if UNITY_EDITOR
/**
<summary>
This is a helpful functionality that makes a property 'ReadOnly' in the editor, so you can't accidently or purposely change it.
</summary>
*/
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif