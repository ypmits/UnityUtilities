using UnityEngine;
using UnityEditor;
using System.IO;


namespace Ypmits.Unitytools.Editor
{
	public static class DirectoryUtils
	{
		public static void AddAssetToCurrentDirectory(Object asset, string name, bool autoSelect)
		{
			AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}.asset", GetSelectedPathOrFallback(), name));
			AssetDatabase.SaveAssets();
			if (autoSelect)
			{
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
			}
		}

		/**
		<summary>
		Retrieves selected folder on Project view.
		</summary>
		<returns></returns>
		*/
		public static string GetSelectedPathOrFallback()
		{
			string path = "Assets";
			foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
			{
				path = AssetDatabase.GetAssetPath(obj);
				if (!string.IsNullOrEmpty(path) && File.Exists(path))
				{
					path = Path.GetDirectoryName(path);
					break;
				}
			}
			return path;
		}
	}
}