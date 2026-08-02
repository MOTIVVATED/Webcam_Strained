using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuideRankDialogueSet", menuName = "Guide/Rank Dialogue Set")]
public class GuideRankDialogueSet : ScriptableObject
{
	[System.Serializable]
	public class RankEntry
	{
		public int rankIndex;
		public List<string> lines;
	}

	public List<RankEntry> entries;
}
