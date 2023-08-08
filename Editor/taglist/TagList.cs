using UnityEngine;
using System.Collections.Generic;

namespace com.ypmits.unitytools
{
	[CreateAssetMenu(fileName = "TagList", menuName = "Tools/Ypmits/Create TagList", order = 1)]
	public class TagList : ScriptableObject
	{
		[SerializeField] private List<string> m_tags = new List<string>();
		public List<string> tags => m_tags;
	}
}