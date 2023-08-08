using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


namespace com.ypmits.unitytools
{
	/**
	<summary>
	</summary>
	*/
	[CustomEditor(typeof(TagList))]
	public class TagListEditor : Editor
	{
		private ReorderableList m_tagList;

		void OnEnable()
		{
			if (target == null) return;
			m_tagList = EditorUtils.DrawReorderableTagList(serializedObject, "m_tags", "Managed tags", "These are the tags that are managed by 'GameObjectManager'");
		}
		public override void OnInspectorGUI()
		{
			SerializedObject so = serializedObject;
			so.Update();

			m_tagList.DoLayoutList();

			so.ApplyModifiedProperties();
		}
	}
}